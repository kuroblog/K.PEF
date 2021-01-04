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

            ExecuteDelegateCommand = new DelegateCommand(Execute, CanExecute);

            DelegateCommandObservesProperty = new DelegateCommand(Execute, CanExecute).ObservesProperty(() => IsEnabled);

            DelegateCommandObservesCanExecute = new DelegateCommand(Execute).ObservesCanExecute(() => IsEnabled);

            ExecuteGenericDelegateCommand = new DelegateCommand<string>(ExecuteGeneric).ObservesCanExecute(() => IsEnabled);
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
        private bool _isEnabled;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                SetProperty(ref _isEnabled, value);
                ExecuteDelegateCommand.RaiseCanExecuteChanged();
            }
        }

        private string _updateText;
        public string UpdateText
        {
            get { return _updateText; }
            set { SetProperty(ref _updateText, value); }
        }

        public DelegateCommand ExecuteDelegateCommand { get; private set; }

        public DelegateCommand<string> ExecuteGenericDelegateCommand { get; private set; }

        public DelegateCommand DelegateCommandObservesProperty { get; private set; }

        public DelegateCommand DelegateCommandObservesCanExecute { get; private set; }

        private void Execute()
        {
            IsEnabled = false;

            UpdateText = $"Updated: {DateTime.Now}";

            Task.Delay(TimeSpan.FromSeconds(3)).Wait();

            IsEnabled = true;
        }

        private void ExecuteGeneric(string parameter)
        {
            UpdateText = parameter;
        }

        private bool CanExecute()
        {
            return IsEnabled;
        }
    }
}
