using K.PEF.Core.Common.Extensions;
using K.PEF.Core.Common.Interfaces;
using K.PEF.Core.Modules.PrismExtensions;
using Prism.Commands;
using Prism.Regions;
using System;
using System.Threading;

namespace K.PEF.Core.Modules.TestTool.ViewModels
{
    public partial class TestLogViewModel : PrismNavigationAwareViewModel
    {
        private readonly ILogger<TestLogViewModel> _logger = null;
        private readonly IMainDispatcher _mainDispatcher = null;

        public TestLogViewModel(
            IRegionManager regionManager,
            IMainDispatcher mainDispatcher,
            ILogger<TestLogViewModel> logger) : base(regionManager)
        {
            if (regionManager == null)
            {
                throw new ArgumentNullException(nameof(regionManager));
            }

            if (mainDispatcher == null)
            {
                throw new ArgumentNullException(nameof(mainDispatcher));
            }

            if (logger == null)
            {
                throw new ArgumentNullException(nameof(logger));
            }

            _mainDispatcher = mainDispatcher;
            _logger = logger;

            _logger.Debug("{class}.{method} invoke.", nameof(TestLogViewModel), nameof(TestLogViewModel));
        }

        public DelegateCommand<object> LoadedCommand => new DelegateCommand<object>(args =>
        {
            _logger.Debug("{class}.{method} invoke.", nameof(TestLogViewModel), nameof(LoadedCommand));
        });

        private void ThrowTestException(string errorMessage = "Something has gone wrong.") => throw new InvalidOperationException(errorMessage);

        public DelegateCommand CreateNewLogCommand => new DelegateCommand(() =>
        {
            _logger.Debug("{class}.{method} invoke.", nameof(TestLogViewModel), nameof(CreateNewLogCommand));

            // sample 1
            //ThrowTestException();

            // sample 2
            //_mainDispatcher.Invoke(() => ThrowTestException());

            // sample 3
            new Thread(() => ThrowTestException()) { IsBackground = true }.Start();
        });
    }
}
