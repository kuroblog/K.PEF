using K.PEF.Common;
using K.PEF.Common.Extensions;
using K.PEF.Logger;
using K.PEF.Logger.Infrastructures;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace K.PEF.Shell
{
    //public partial class App : Application
    public partial class App : PrismApplication
    {
        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new DirectoryModuleCatalog { ModulePath = @".\Modules" };

            //return base.CreateModuleCatalog();
        }

        protected override Window CreateShell()
        {
            var shellWindow = Container.Resolve<Shell>();
            shellWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            var shellConfigManager = Container.Resolve<ShellConfigManager>();
            shellWindow.Width = shellConfigManager.Width;
            shellWindow.Height = shellConfigManager.Height;

            return shellWindow;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ShellConfigManager>();
            containerRegistry.RegisterSingleton<ILogger, NLogger>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                ShutdownMode = ShutdownMode.OnMainWindowClose;

                //DispatcherUnhandledException += (s, o) => { };
                DispatcherUnhandledException += DispatcherUnhandledExceptionHandler;

                //AppDomain.CurrentDomain.UnhandledException += (s, o) => { };
                AppDomain.CurrentDomain.UnhandledException += AppDomainCurrentDomainUnhandledExceptionHandler;

                //TaskScheduler.UnobservedTaskException += (s, o) => { };
                TaskScheduler.UnobservedTaskException += TaskSchedulerUnobservedTaskExceptionHandler;

                //StartupUri = new Uri($"{nameof(MainWindow)}.xaml", UriKind.Relative);

                base.OnStartup(e);
            }
            catch (Exception ex)
            {
                CatchExceptionHandler(ex.AsJsonFormatString(), this);
            }
        }

        private void DispatcherUnhandledExceptionHandler(object sender, DispatcherUnhandledExceptionEventArgs e) =>
            CatchExceptionHandler(e.Exception, sender, () => e.Handled = true);

        private void TaskSchedulerUnobservedTaskExceptionHandler(object sender, UnobservedTaskExceptionEventArgs e) =>
            CatchExceptionHandler(e.Exception, sender, () => e.SetObserved());

        private void AppDomainCurrentDomainUnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.IsTerminating)
            {
                logger.Fatal(e.ExceptionObject);

                var fatalFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"ERROR.{DateTime.Now.AsFullTimeFormatString()}.{Guid.NewGuid().AsFormatN()}.log");
                File.WriteAllText(fatalFile, e.ExceptionObject.AsJsonFormatString());

                MessageBox.Show(e.ExceptionObject.AsJsonFormatString(), "System Error");

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

        private void CatchExceptionHandler<TError>(TError error, object sender, Action callbackHandler = null)
        {
            try
            {
                logger.Error(error);

                callbackHandler?.Invoke();
            }
            catch (Exception ex)
            {
                Trace.TraceError(ex.Message);

                logger.Fatal(ex);

                //MessageBox.Show(ex.Message, "error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //private void WriteToErrorLog<TError>(TError error, object sender)
        //{
        //    var errorJson = error.AsJsonFormatString();
        //    var target = sender == null ? "null" : sender.GetType().FullName;
        //    var errorFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"ERROR.{DateTime.Now.AsFullTimeFormatString()}.{target}.{Guid.NewGuid().AsFormatN()}.log");
        //    File.WriteAllText(errorFile, errorJson);
        //}

        private ILogger logger => Container.Resolve<ILogger>();
    }
}
