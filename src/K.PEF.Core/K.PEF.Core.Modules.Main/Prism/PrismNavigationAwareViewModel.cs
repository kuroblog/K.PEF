using Prism.Mvvm;
using Prism.Regions;
using System;

namespace K.PEF.Core.Modules.PrismExtensions
{
    public abstract class PrismNavigationAwareViewModel : BindableBase, INavigationAware
    {
        /****************************************************************************************************
         * Prism.Regions.INavigationAware
         * {
         *      bool IsNavigationTarget(Prism.Regions.NavigationContext navigationContext);
         * }
         * **************************************************************************************************/
        public virtual bool IsNavigationTarget(NavigationContext navigationContext) => true;

        /****************************************************************************************************
         * Prism.Regions.INavigationAware
         * {
         *      void OnNavigatedFrom(Prism.Regions.NavigationContext navigationContext);
         * }
         * **************************************************************************************************/
        public virtual void OnNavigatedFrom(NavigationContext navigationContext) { }

        /****************************************************************************************************
         * Prism.Regions.INavigationAware
         * {
         *      void OnNavigatedTo(Prism.Regions.NavigationContext navigationContext);
         * }
         * **************************************************************************************************/
        public virtual void OnNavigatedTo(NavigationContext navigationContext) { }

        protected readonly IRegionManager _regionManager = null;

        public PrismNavigationAwareViewModel(IRegionManager regionManager)
        {
            if (regionManager == null)
            {
                throw new ArgumentNullException(nameof(regionManager));
            }

            _regionManager = regionManager;
        }
    }
}
