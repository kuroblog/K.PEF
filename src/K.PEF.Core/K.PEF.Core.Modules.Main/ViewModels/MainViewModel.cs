using Prism.Commands;
using Prism.Mvvm;

namespace K.PEF.Core.Modules.Main.ViewModels
{
   public partial class MainViewModel : BindableBase
    {
        public MainViewModel()
        {
        }

        public DelegateCommand<object> LoadedCommand => new DelegateCommand<object>(args =>
        {
        });
    }
}
