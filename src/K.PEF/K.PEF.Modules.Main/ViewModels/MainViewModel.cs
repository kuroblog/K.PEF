using Prism.Commands;
using Prism.Mvvm;

namespace K.PEF.Modules.Main.ViewModels
{
    public partial class MainViewModel : BindableBase
    {
        private readonly MainConfigManager configManager;

        public MainViewModel(
            MainConfigManager configManager)
        {
            this.configManager = configManager;
        }

        public DelegateCommand<object> LoadedCommand => new DelegateCommand<object>(args => { });
    }
}
