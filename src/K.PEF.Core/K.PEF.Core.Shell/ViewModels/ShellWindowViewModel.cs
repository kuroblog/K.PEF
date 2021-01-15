using K.PEF.Core.Common.Infrastructures;
using K.PEF.Core.Common.Settings;
using Microsoft.Extensions.Options;
using Prism.Commands;
using Prism.Mvvm;
using System;

namespace K.PEF.Core.Shell.ViewModels
{
    public partial class ShellWindowViewModel : BindableBase
    {
        private readonly StartupSetting _startupSetting = null;

        public string Title => _startupSetting.AppName;

        private double _width;

        public double Width
        {
            get => _width;
            set => SetProperty(ref _width, value);
        }

        private double _height;

        public double Height
        {
            get => _height;
            set => SetProperty(ref _height, value);
        }

        private readonly ILogger<ShellWindowViewModel> _logger;

        public ShellWindowViewModel(IOptions<StartupSetting> startupSettingOptions, ILogger<ShellWindowViewModel> logger)
        {
            if (startupSettingOptions == null)
            {
                throw new ArgumentNullException(nameof(startupSettingOptions));
            }

            if (startupSettingOptions.Value == null)
            {
                throw new ArgumentException("null", nameof(StartupSetting));
            }

            _startupSetting = startupSettingOptions.Value;
            _width = _startupSetting.ScreenWidth;
            _height = _startupSetting.ScreenHeight;

            //Trace.WriteLine("12333333");

            _logger = logger;
            //_logger.Information("this is a {test_message}", "hello world.");
        }

        public DelegateCommand<object> LoadedCommand => new DelegateCommand<object>(args => { });
    }
}
