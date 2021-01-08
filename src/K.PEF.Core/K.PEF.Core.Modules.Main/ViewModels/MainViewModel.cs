using K.PEF.Core.Modules.PrismExtensions;
using Prism.Commands;
using Prism.Regions;

namespace K.PEF.Core.Modules.Main.ViewModels
{
    //public partial class MainViewModel : BindableBase, INavigationAware
    public partial class MainViewModel : PrismNavigationAwareViewModel
    {
        public MainViewModel(IRegionManager regionManager) : base(regionManager) { }

        public DelegateCommand<object> LoadedCommand => new DelegateCommand<object>(args => { });
    }
}
