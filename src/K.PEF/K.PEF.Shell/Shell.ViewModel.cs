using Prism.Commands;
using Prism.Mvvm;

namespace K.PEF.Shell
{
    public partial class ShellViewModel : BindableBase
    {
        private readonly ShellConfigManager configManager;

        public ShellViewModel(
            ShellConfigManager configManager)
        {
            this.configManager = configManager;
        }

        public DelegateCommand<object> LoadedCommand => new DelegateCommand<object>(args => { });

        public int Width => configManager.Width;

        public int Height => configManager.Height;

        private string title = "K - Prism Extend Framework";

        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }
    }
}