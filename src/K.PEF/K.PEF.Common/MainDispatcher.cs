using Prism.Ioc;
using Prism.Unity;
using System;
using System.Windows;
using System.Windows.Threading;

namespace K.PEF.Common
{
    public class MainDispatcher
    {
        private MainDispatcher() { }

        private static readonly Lazy<MainDispatcher> instance = new Lazy<MainDispatcher>(() => new MainDispatcher { });

        public static MainDispatcher Instance => instance?.Value;

        private readonly PrismApplication prismApplication = Application.Current as PrismApplication;

        public IContainerProvider ContainerProvider => prismApplication.Container;

        public void Invoke(Action action, DispatcherPriority priority = DispatcherPriority.Normal) =>
            prismApplication.Dispatcher.BeginInvoke(priority, action);
    }
}
