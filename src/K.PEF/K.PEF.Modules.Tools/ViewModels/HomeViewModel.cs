using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace K.PEF.Modules.Tools.ViewModels
{
    public partial class HomeViewModel : BindableBase, INavigationAware
    {
        private readonly ToolsConfigManager configManager;

        public HomeViewModel(
            ToolsConfigManager configManager)
        {
            this.configManager = configManager;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
        }

        public DelegateCommand<object> LoadedCommand => new DelegateCommand<object>(args => { });
    }
}
