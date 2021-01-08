using Prism.Ioc;
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
            var shell = Container.Resolve<MainWindow>();
            shell.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            return shell;
        }
    }
}
