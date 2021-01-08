using K.PEF.Core.Modules.PrismExtensions;
using K.PEF.Core.Modules.TestTool.Models;
using K.PEF.Core.Modules.TestTool.Settings;
using Prism.Commands;
using Prism.Regions;
using System.Collections.ObjectModel;

namespace K.PEF.Core.Modules.TestTool.ViewModels
{
    public partial class MenuViewModel : PrismNavigationAwareViewModel
    {
        public MenuViewModel(IRegionManager regionManager) : base(regionManager)
        {
            void doRequestNavigate(string key) => _regionManager.RequestNavigate(RegionNames.BodyRegion, key, callback => { });

            void doShutdown(string key) { }

            var controlMenu = new MenuModel(typeof(Views.TestControlView).FullName, "Test Controls", doRequestNavigate);
            var styleMenu = new MenuModel(typeof(Views.TestStyleView).FullName, "Test Styles", doRequestNavigate);
            var exitMenu = new MenuModel("Exit", "Exit", doShutdown);
            var uiTestMenu = new MenuModel("UiTest", "UI Test") { SubMenus = new ObservableCollection<MenuModel> { controlMenu, styleMenu, exitMenu } };
            Menus.Add(uiTestMenu);

            var dataMenu = new MenuModel(typeof(Views.TestDataView).FullName, "Mock Data", doRequestNavigate);
            var dataTestMenu = new MenuModel("DataTest", "Data Test") { SubMenus = new ObservableCollection<MenuModel> { dataMenu } };

            var logMenu = new MenuModel(typeof(Views.TestLogView).FullName, "Mock New Logs", doRequestNavigate);
            var logTestMenu = new MenuModel("LogTest", "Log Test") { SubMenus = new ObservableCollection<MenuModel> { logMenu } };

            var mockTestMenu = new MenuModel("MockTest", "Mock Data Test") { SubMenus = new ObservableCollection<MenuModel> { dataTestMenu, logTestMenu } };
            Menus.Add(mockTestMenu);

            var viewsMenu = new MenuModel("Views", "Views");
            Menus.Add(viewsMenu);

            var viewHelpMenu = new MenuModel("ViewHelp", "View Help");
            var aboutMenu = new MenuModel("About", "About");
            var helpMenu = new MenuModel("Help", "Help") { SubMenus = new ObservableCollection<MenuModel> { viewHelpMenu, aboutMenu } };
            Menus.Add(helpMenu);
        }

        public DelegateCommand<object> LoadedCommand => new DelegateCommand<object>(args => { });

        private ObservableCollection<MenuModel> _menus = new ObservableCollection<MenuModel>();

        public ObservableCollection<MenuModel> Menus
        {
            get => _menus;
            set => SetProperty(ref _menus, value);
        }
    }
}
