using Prism.Commands;
using Prism.Mvvm;

namespace K.PEF.Core.Shell.ViewModels
{
    public partial class ShellWindowViewModel : BindableBase
    {
        public ShellWindowViewModel()
        {
        }

        public DelegateCommand<object> LoadedCommand => new DelegateCommand<object>(args =>
        {
        });
    }
}
