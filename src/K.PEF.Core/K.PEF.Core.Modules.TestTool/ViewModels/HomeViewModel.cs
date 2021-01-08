using K.PEF.Core.Modules.PrismExtensions;
using K.PEF.Core.Modules.TestTool.Settings;
using Prism.Commands;
using Prism.Regions;

namespace K.PEF.Core.Modules.TestTool.ViewModels
{
    public partial class HomeViewModel : PrismNavigationAwareViewModel
    {
        public HomeViewModel(IRegionManager regionManager) : base(regionManager) { }

        public DelegateCommand<object> LoadedCommand => new DelegateCommand<object>(args =>
        {
            _regionManager.RequestNavigate(RegionNames.FootRegion, typeof(Views.FootView).FullName, callback => { });

            _regionManager.RequestNavigate(RegionNames.HeadRegion, typeof(Views.MenuView).FullName, callback => { });
        });
    }
}
