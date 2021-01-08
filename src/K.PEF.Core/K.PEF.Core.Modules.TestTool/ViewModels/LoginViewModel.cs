using K.PEF.Core.Common.Settings;
using K.PEF.Core.Modules.PrismExtensions;
using Prism.Commands;
using Prism.Regions;

namespace K.PEF.Core.Modules.TestTool.ViewModels
{
    public partial class LoginViewModel : PrismNavigationAwareViewModel
    {
        public LoginViewModel(IRegionManager regionManager) : base(regionManager) { }

        public DelegateCommand<object> LoadedCommand => new DelegateCommand<object>(args => { });

        public DelegateCommand MockUserLoginCommand => new DelegateCommand(() =>
        {
            var navParams = new NavigationParameters();

            _regionManager.RequestNavigate(PrismRegionNames.HomeRegion, typeof(Views.HomeView).FullName, callback => { }, navParams);
        });
    }
}
