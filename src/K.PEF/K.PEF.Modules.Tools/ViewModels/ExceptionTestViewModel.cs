using K.PEF.Common;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace K.PEF.Modules.Tools.ViewModels
{
    public partial class ExceptionTestViewModel : BindableBase, INavigationAware
    {
        private readonly ToolsConfigManager configManager;

        public ExceptionTestViewModel(
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
        private void ThrowTestException(string errorMessage = "Something has gone wrong.") => throw new InvalidOperationException(errorMessage);

        public DelegateCommand Ex1Command => new DelegateCommand(() => ThrowTestException());

        public DelegateCommand Ex2Command => new DelegateCommand(() =>
        {
            MainDispatcher.Instance.Invoke(() => ThrowTestException());
        });

        public DelegateCommand Ex3Command => new DelegateCommand(() =>
        {
            new Thread(() => ThrowTestException())
            {
                IsBackground = true
            }.Start();
        });

        public DelegateCommand Ex4Command => new DelegateCommand(() =>
        {
            Task.Run(() => ThrowTestException());
        });

        public DelegateCommand Ex5Command => new DelegateCommand(async () =>
        {
            await Task.Run(() => ThrowTestException());
        });
    }
}
