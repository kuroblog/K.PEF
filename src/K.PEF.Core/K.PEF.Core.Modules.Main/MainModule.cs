using K.PEF.Core.Common.Settings;
using K.PEF.Core.Modules.Main.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace K.PEF.Core.Modules.Main
{
    public class MainModule : IModule
    {
        public MainModule() { }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();

            regionManager.RegisterViewWithRegion(PrismRegionNames.MainRegion, typeof(MainView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry) { }
    }
}
