using K.PEF.Core.Common.Extensions;
using K.PEF.Core.Common.Interfaces;
using K.PEF.Core.Common.Settings;
using K.PEF.Core.Shell.Infrastructures;
using K.PEF.Core.Shell.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Formatting.Compact;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using static K.PEF.Core.Common.Extensions.ILoggerExtensions;
using ci = K.PEF.Core.Common.Interfaces;

namespace K.PEF.Core.Shell
{
    //public partial class App : Application
    // 使用 Prism.Unity.PrismApplication 替换 System.Windows.Application
    public partial class App : PrismApplication
    {
        /****************************************************************************************************
         * Prism.PrismApplicationBase
         * {
         *      protected abstract void RegisterTypes(Prism.Ioc.IContainerRegistry containerRegistry);
         * }
         * **************************************************************************************************/
        protected override void RegisterTypes(IContainerRegistry containerRegistry) => RegisterTypesByContainerWithServiceCollection(containerRegistry);

        /****************************************************************************************************
         * Prism.PrismApplicationBase
         * {
         *      protected abstract System.Windows.Window CreateShell()
         * }
         * **************************************************************************************************/
        protected override Window CreateShell() => CreateShellWindow();

        protected override IModuleCatalog CreateModuleCatalog()
        {
            //base.CreateModuleCatalog();
            // 设置要加载的 Modules 目录
            // 当前目录
            //var modulePath = @".\";
            // 指定目录
            var modulePath = @".\Modules";

            return new DirectoryModuleCatalog { ModulePath = modulePath };
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                ShutdownMode = ShutdownMode.OnMainWindowClose;

                //DispatcherUnhandledException += (s, o) => { };
                DispatcherUnhandledException += DispatcherUnhandledExceptionHandler;

                //AppDomain.CurrentDomain.UnhandledException += (s, o) => { };
                //AppDomain.CurrentDomain.UnhandledException += AppDomainCurrentDomainUnhandledExceptionHandler;
                AppDomain.CurrentDomain.UnhandledException += AppDomainCurrentDomainUnhandledExceptionHandlerV2;

                //TaskScheduler.UnobservedTaskException += (s, o) => { };
                TaskScheduler.UnobservedTaskException += TaskSchedulerUnobservedTaskExceptionHandler;

                //StartupUri = new Uri($"{nameof(MainWindow)}.xaml", UriKind.Relative);

                base.OnStartup(e);
            }
            catch (Exception ex)
            {
                CatchExceptionHandler(ex.AsJsonString(), this);
            }
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Log.CloseAndFlush();

            base.OnExit(e);
        }

        private Window _shellWindow = null;

        private Window CreateShellWindow()
        {
            var startupSetting = Container.Resolve<IOptions<StartupSetting>>()?.Value;
            if (startupSetting == null)
            {
                throw new ArgumentException("null", nameof(startupSetting));
            }

            _shellWindow = Container.Resolve<Views.ShellWindow>();
            _shellWindow.Width = startupSetting.ScreenWidth;
            _shellWindow.Height = startupSetting.ScreenHeight;
            _shellWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            return _shellWindow;
        }

        private void RegisterTypesByContainerWithServiceCollection(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterServices(serviceCollection =>
            {
                var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                var configure = new ConfigurationBuilder()
                //.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                // or
                .SetBasePath(Directory.GetCurrentDirectory())
                // set appsettings from
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                // when asp.net core
                .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
                .Build();

                if (Log.Logger == null || Log.Logger.GetType().Name == "SilentLogger")
                {
                    Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configure)
                    .Enrich.With(new TestArgEnrich())
                    .Enrich.WithProperty("DebuggerAttached", Debugger.IsAttached)
                    .CreateLogger();
                }

                // serilog sample 1
                //Log.Logger.Information("{Test} is a test.", "12333");

                // serilog sample 2
                //using var t = Serilog.Context.LogContext.PushProperty("ScopeKey", 999);
                //Log.Logger.Information("111");
                //Log.Logger.Information("222");
                //Log.Logger.Information("333");
                //t.Dispose();

                serviceCollection
            .AddSingleton<IConfiguration>(configure)
            .Configure<StartupSetting>(configure.GetSection(nameof(StartupSetting)));

                serviceCollection.AddLogging(builder =>
                {
                    builder
                .ClearProviders()
                .AddSerilog(Log.Logger, dispose: true);
                });

                serviceCollection
                .AddSingleton(Log.Logger)
                .AddSingleton<ci.ILogger, LogWrapper>()
                .AddSingleton(typeof(ci.ILogger<>), typeof(LogWrapper<>));
            });

            containerRegistry.RegisterSingleton<IMainDispatcher, MainDispatcher>();
        }

        private void DispatcherUnhandledExceptionHandler(object sender, DispatcherUnhandledExceptionEventArgs e) =>
            CatchExceptionHandler(e.Exception, sender, () => e.Handled = true);

        private void TaskSchedulerUnobservedTaskExceptionHandler(object sender, UnobservedTaskExceptionEventArgs e) =>
            CatchExceptionHandler(e.Exception, sender, () => e.SetObserved());

        private void AppDomainCurrentDomainUnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.IsTerminating)
            {
                // TODO: Exception 转化 Json 会出错
                var errorJson = e.ExceptionObject.AsFormattedJsonString();

                _logger.Fatal(
                    new Exception(e.ExceptionObject.ToString()),
                    "{sender} : {UnhandledException}",
                    sender == null ? "empty" : sender.GetType().FullName,
                    errorJson);

                WriteToFatalLog(errorJson);

                MessageBox.Show(_shellWindow, errorJson, "System Error");

                //MainDispatcher.Instance.Invoke(() => MessageBox.Show(
                //    $"系统发生异常:{Environment.NewLine}  {e.ExceptionObject.ToString()}{Environment.NewLine}  {e.ExceptionObject.AsJsonFormatString()}",
                //    "系统错误",
                //    MessageBoxButton.OK));
            }
            else
            {
                CatchExceptionHandler(e.ExceptionObject, sender);
            }
        }

        private void AppDomainCurrentDomainUnhandledExceptionHandlerV2(object sender, UnhandledExceptionEventArgs e) =>
            CatchExceptionHandler(
                e.ExceptionObject,
                sender,
                () =>
                {
                    if (e.IsTerminating)
                    {
                        Container.Resolve<IMainDispatcher>()?.Invoke(
                            () => MessageBox.Show(_shellWindow, e.ExceptionObject.ToString(), "System Error"));
                    }
                },
                e.IsTerminating);

        private void WriteToFatalLog(string fatalLog)
        {
            var fatalLogFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"ERROR.{DateTime.Now.ToString("yyyyMMddHHmmssfffffff")}.{Guid.NewGuid().ToString("N")}.log");
            File.WriteAllText(fatalLogFile, fatalLog);
        }

        private void CatchExceptionHandler<TError>(TError error, object sender, Action callbackHandler = null, bool IsTerminating = false)
        {
            try
            {
                if (error == null)
                {
                    throw new ArgumentNullException(nameof(error));
                }

                if (error is not null and Exception e)
                {
                    if (IsTerminating)
                    {
                        _logger.Fatal(e);
                    }
                    else
                    {
                        _logger.Error(e);
                    }

                    ExceptionDispatchInfo.Capture(e);
                }
                else
                {
                    // TODO: Exception 转化 Json 会出错
                    _logger.Fatal(
                        new Exception(error.ToString()),
                        "{sender} : {UnhandledException}",
                        sender == null ? "empty" : sender.GetType().FullName,
                        error.AsJsonString());
                }

                callbackHandler?.Invoke();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);

                _logger.Fatal(ex);

                ExceptionDispatchInfo.Capture(ex);

                //MessageBox.Show(ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private ci.ILogger _logger
        {
            get
            {
                var logger = Container.Resolve<ci.ILogger>();
                if (logger == null)
                {
                    var temporaryLogger = new LoggerConfiguration()
                        .MinimumLevel.Verbose()
                        .Enrich.FromLogContext()
                        .Enrich.WithThreadId()
                        .Enrich.WithMachineName()
                        .Enrich.WithExceptionDetails()
                        .WriteTo.Console()
                        .WriteTo.Trace()
                        .WriteTo.File(
                            restrictedToMinimumLevel: LogEventLevel.Verbose,
                            formatter: new CompactJsonFormatter(),
                            path: "./logs/err_.json",
                            rollingInterval: RollingInterval.Day,
                            rollOnFileSizeLimit: true)
                        .CreateLogger();

                    logger = new LogWrapper(temporaryLogger);
                }

                return logger;
            }
        }
    }
}
