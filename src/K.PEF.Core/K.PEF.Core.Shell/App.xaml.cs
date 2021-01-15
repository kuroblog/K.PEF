using K.PEF.Core.Common.Settings;
using K.PEF.Core.Shell.Infrastructures;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using Serilog;
using System;
using System.IO;
using System.Windows;
using ci = K.PEF.Core.Common.Infrastructures;

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
            ShutdownMode = ShutdownMode.OnMainWindowClose;

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            Log.CloseAndFlush();

            base.OnExit(e);
        }

        private Window CreateShellWindow()
        {
            var startupSetting = Container.Resolve<IOptions<StartupSetting>>()?.Value;
            if (startupSetting == null)
            {
                throw new ArgumentException("null", nameof(startupSetting));
            }

            var shell = Container.Resolve<Views.ShellWindow>();
            shell.Width = startupSetting.ScreenWidth;
            shell.Height = startupSetting.ScreenHeight;
            shell.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            return shell;
        }

        private void RegisterTypesByContainerWithServiceCollection(IContainerRegistry containerRegistry) =>
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
                        .CreateLogger();
                }

                serviceCollection
                    .AddSingleton<IConfiguration>(configure)
                    .Configure<StartupSetting>(configure.GetSection(nameof(StartupSetting)));

                serviceCollection.AddLogging(builder =>
                {
                    builder
                        .ClearProviders()
                        //.AddConsole()
                        .AddSerilog(Log.Logger, dispose: true);
                });

                serviceCollection
                    .AddSingleton(Log.Logger)
                    .AddSingleton<ci.ILogger, LogWrapper>()
                    .AddSingleton(typeof(ci.ILogger<>), typeof(LogWrapper<>));
            });
    }
}
