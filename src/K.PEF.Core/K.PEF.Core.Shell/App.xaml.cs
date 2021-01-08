using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using System.Windows;

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
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

        /****************************************************************************************************
         * Prism.PrismApplicationBase
         * {
         *      protected abstract System.Windows.Window CreateShell()
         * }
         * **************************************************************************************************/
        protected override Window CreateShell()
        {
            var shell = Container.Resolve<Views.ShellWindow>();
            shell.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            return shell;
        }

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
            base.OnExit(e);
        }
    }
}
