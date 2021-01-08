using K.PEF.Core.Modules.PrismExtensions;
using Prism.Commands;
using Prism.Regions;
using K.PEF.Core.Common.Settings;

namespace K.PEF.Core.Modules.TestTool.ViewModels
{
    public partial class MainViewModel : PrismNavigationAwareViewModel
    {
        public MainViewModel(IRegionManager regionManager) : base(regionManager) { }

        public DelegateCommand<object> LoadedCommand => new DelegateCommand<object>(args =>
        {
            _regionManager.RequestNavigate(PrismRegionNames.HomeRegion, typeof(Views.LoginView).FullName, callback => { });
        });
    }
}
