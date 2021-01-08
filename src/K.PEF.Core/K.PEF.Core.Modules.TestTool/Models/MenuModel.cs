using K.PEF.Core.Common.Extensions;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;

namespace K.PEF.Core.Modules.TestTool.Models
{
    public class MenuModel : BindableBase
    {
        private string _key = string.Empty;

        public string Key
        {
            get => _key;
            set => SetProperty(ref _key, value);
        }

        private string _name = string.Empty;

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private ObservableCollection<MenuModel> _subMenus = new ObservableCollection<MenuModel>();

        public ObservableCollection<MenuModel> SubMenus
        {
            get => _subMenus;
            set => SetProperty(ref _subMenus, value);
        }

        public DelegateCommand<string> DoCommand { get; private set; }

        public MenuModel(string key, string name, Action<string> menuActionHandler = null)
        {
            key.ThrowExceptionWhenArgIsNullOrEmpty();
            name.ThrowExceptionWhenArgIsNullOrEmpty();
            //menuActionHandler.ThrowExceptionWhenArgIsNullOrEmpty();

            Key = key;
            Name = name;

            if (menuActionHandler != null)
            {
                DoCommand = new DelegateCommand<string>(menuActionHandler);
            }
        }

        //public MenuModel()
        //{
        //    DoCommand = new DelegateCommand<string>(OnMenuActivated);
        //}

        //public DelegateCommand<string> DoCommand { get; private set; } //= new DelegateCommand<string>(key => { OnMenuActivated(key); });

        //public delegate void MenuActivatedEventHandler(string menuKey);

        //public event MenuActivatedEventHandler MenuActivated;

        //public void OnMenuActivated(string menuKey) => MenuActivated?.Invoke(menuKey);
    }
}
