using K.PEF.Common.Parameters;
using K.PEF.Logger.Infrastructures;
using K.PEF.Modules.Main.ViewModels;
using K.PEF.Modules.Main.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Mvvm;
using Prism.Regions;
using System.Reflection;

namespace K.PEF.Modules.Main
{
    public class MainModule : IModule
    {
        private readonly ILogger logger;

        public MainModule(ILogger logger)
        {
            logger.Info(MethodBase.GetCurrentMethod().Name);

            this.logger = logger;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            logger.Info(MethodBase.GetCurrentMethod().Name);

            var regionManager = containerProvider.Resolve<IRegionManager>();

            regionManager.RegisterViewWithRegion(RegionNames.Main, typeof(MainView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            logger.Info(MethodBase.GetCurrentMethod().Name);

            containerRegistry.RegisterSingleton<MainConfigManager>();

            ViewModelLocationProvider.Register<MainView, MainViewModel>();
        }
    }
}
