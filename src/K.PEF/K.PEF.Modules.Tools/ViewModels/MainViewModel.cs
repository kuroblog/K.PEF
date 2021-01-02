using K.PEF.Modules.Tools.Extensions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace K.PEF.Modules.Tools.ViewModels
{
    public partial class MainViewModel : BindableBase, INavigationAware
    {
        private readonly ToolsConfigManager configManager;
        private readonly IRegionManager regionManager;

        public MainViewModel(
            IRegionManager regionManager,
            ToolsConfigManager configManager)
        {
            this.configManager = configManager;
            this.regionManager = regionManager;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        public DelegateCommand<object> LoadedCommand => new DelegateCommand<object>(args =>
        {
            //regionManager.NavigateToHome(a => ViewName = "Home");
            regionManager.NavigateToExceptionTest(a => ViewName = "Exception Test");
        });

        public DelegateCommand HomeCommand => new DelegateCommand(() => regionManager.NavigateToHome(a => ViewName = "Home"));

        public DelegateCommand ExceptionTestCommand => new DelegateCommand(() => regionManager.NavigateToExceptionTest(a => ViewName = "Exception Test"));

        public DelegateCommand DialogWindowTestCommand => new DelegateCommand(() => regionManager.NavigateToDialogWindowTest(a => ViewName = "Dialog Window Test"));

        public string Version => configManager.Version;

        private string viewName;

        public string ViewName
        {
            get => viewName;
            set => SetProperty(ref viewName, value);
        }
    }
}

