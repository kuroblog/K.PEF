using K.PEF.Core.Modules.PrismExtensions;
using Prism.Commands;
using Prism.Regions;

namespace K.PEF.Core.Modules.TestTool.ViewModels
{
    public partial class TestStyleViewModel : PrismNavigationAwareViewModel
    {
        public TestStyleViewModel(IRegionManager regionManager) : base(regionManager) { }

        public DelegateCommand<object> LoadedCommand => new DelegateCommand<object>(args => { });
    }
}
