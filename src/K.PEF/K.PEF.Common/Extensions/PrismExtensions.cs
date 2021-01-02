using Prism.Ioc;
using System.Windows.Controls;

namespace K.PEF.Common.Extensions
{
    public static class PrismExtensions
    {
        public static void RegisterForNavigationByViewType<TUserControl>(this IContainerRegistry containerRegistry) where TUserControl : UserControl =>
            containerRegistry?.RegisterForNavigation<TUserControl>(typeof(TUserControl).FullName);
    }
}
