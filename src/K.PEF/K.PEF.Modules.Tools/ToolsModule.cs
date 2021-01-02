using K.PEF.Common.Extensions;
using K.PEF.Common.Parameters;
using K.PEF.Logger.Infrastructures;
using K.PEF.Modules.Tools.ViewModels;
using K.PEF.Modules.Tools.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using System.Reflection;

namespace K.PEF.Modules.Tools
{
    public class ToolsModule : IModule
    {
        private readonly ILogger logger;

        public ToolsModule(ILogger logger)
        {
            logger.Info(MethodBase.GetCurrentMethod().Name);

            this.logger = logger;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            logger.Info(MethodBase.GetCurrentMethod().Name);

            var regionManager = containerProvider.Resolve<IRegionManager>();

            regionManager.RegisterViewWithRegion(RegionNames.Content, typeof(MainView));

            //regionManager.RegisterViewWithRegion(RegionNames.Home, typeof(HomeView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            logger.Info(MethodBase.GetCurrentMethod().Name);

            containerRegistry.RegisterSingleton<ToolsConfigManager>();

            ViewModelLocationProvider.Register<MainView, MainViewModel>();

            ViewModelLocationProvider.Register<HomeView, HomeViewModel>();
            //containerRegistry.RegisterForNavigation<HomeView>(typeof(HomeView).FullName);
            containerRegistry.RegisterForNavigationByViewType<HomeView>();

            ViewModelLocationProvider.Register<ExceptionTestView, ExceptionTestViewModel>();
            containerRegistry.RegisterForNavigationByViewType<ExceptionTestView>();

            ViewModelLocationProvider.Register<DialogWindowTestView, DialogWindowTestViewModel>();
            containerRegistry.RegisterForNavigationByViewType<DialogWindowTestView>();
        }
    }
}
