﻿using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace K.PEF.Core.Shell.ViewModels
{
    public partial class ShellWindowViewModel : BindableBase, INavigationAware
    {
        /****************************************************************************************************
         * Prism.Regions.INavigationAware
         * {
         *      bool IsNavigationTarget(Prism.Regions.NavigationContext navigationContext);
         * }
         * **************************************************************************************************/
        public bool IsNavigationTarget(NavigationContext navigationContext) => true;

        /****************************************************************************************************
         * Prism.Regions.INavigationAware
         * {
         *      void OnNavigatedFrom(Prism.Regions.NavigationContext navigationContext);
         * }
         * **************************************************************************************************/
        public void OnNavigatedFrom(NavigationContext navigationContext) { }

        /****************************************************************************************************
         * Prism.Regions.INavigationAware
         * {
         *      void OnNavigatedTo(Prism.Regions.NavigationContext navigationContext);
         * }
         * **************************************************************************************************/
        public void OnNavigatedTo(NavigationContext navigationContext) { }

        public ShellWindowViewModel() { }

        public DelegateCommand<object> LoadedCommand => new DelegateCommand<object>(args => { });
    }
}
