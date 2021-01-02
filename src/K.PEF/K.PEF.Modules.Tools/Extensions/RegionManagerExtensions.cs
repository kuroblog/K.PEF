using K.PEF.Common.Parameters;
using K.PEF.Modules.Tools.Views;
using Prism.Regions;
using System;

namespace K.PEF.Modules.Tools.Extensions
{
    public static class RegionManagerExtensions
    {
        public static void NavigateToHome(this IRegionManager regionManager, Action<NavigationResult> callbackHandler) =>
            regionManager?.RequestNavigate(RegionNames.Home, typeof(HomeView).FullName, callbackHandler);

        public static void NavigateToExceptionTest(this IRegionManager regionManager, Action<NavigationResult> callbackHandler) =>
            regionManager?.RequestNavigate(RegionNames.Home, typeof(ExceptionTestView).FullName, callbackHandler);

        public static void NavigateToDialogWindowTest(this IRegionManager regionManager, Action<NavigationResult> callbackHandler) =>
            regionManager?.RequestNavigate(RegionNames.Home, typeof(DialogWindowTestView).FullName, callbackHandler);
    }
}
