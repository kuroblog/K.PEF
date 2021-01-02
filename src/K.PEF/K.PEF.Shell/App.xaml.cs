
namespace K.PEF.Shell
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;

    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            ShutdownMode = ShutdownMode.OnMainWindowClose;

            DispatcherUnhandledException += (s, o) => { };

            TaskScheduler.UnobservedTaskException += (s, o) => { };

            AppDomain.CurrentDomain.UnhandledException += (s, o) => { };

            StartupUri = new Uri($"{nameof(MainWindow)}.xaml", UriKind.Relative);

            base.OnStartup(e);
        }
    }
}
