using K.PEF.Core.Common.Settings;
using K.PEF.Core.Modules.TestTool.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;

namespace K.PEF.Core.Modules.Main
{
    public class TestToolModule : IModule
    {
        public TestToolModule() { }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();

            regionManager.RegisterViewWithRegion(PrismRegionNames.ContentRegion, typeof(MainView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //containerRegistry.RegisterForNavigation<LoginView>(typeof(LoginView).FullName);
            new List<Type> {
                typeof(LoginView),
                typeof(HomeView),
                typeof(TestControlView),
                typeof(TestLogView),
                typeof(TestStyleView),
                typeof(TestDataView),
                typeof(FootView),
                typeof(MenuView)
            }.ForEach(type => containerRegistry.RegisterForNavigation(type, type.FullName));
        }
    }
}
