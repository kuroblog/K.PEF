using K.PEF.Core.Common.Interfaces;
using Prism.Ioc;
using Prism.Unity;
using System;
using System.Windows;
using System.Windows.Threading;

namespace K.PEF.Core.Shell.Infrastructures
{
    public class MainDispatcher : IMainDispatcher
    {
        //private static readonly Lazy<MainDispatcher> instance = new Lazy<MainDispatcher>(() => new MainDispatcher { });

        //public static MainDispatcher Instance => instance?.Value;

        private readonly PrismApplication _prismApplication = Application.Current as PrismApplication;

        public IContainerProvider ContainerProvider => _prismApplication.Container;

        public void Invoke(Action action, DispatcherPriority priority = DispatcherPriority.Normal) =>
            _prismApplication.Dispatcher.Invoke(priority, action);

        public void BeginInvoke(Action action, DispatcherPriority priority = DispatcherPriority.Normal) =>
            _prismApplication.Dispatcher.BeginInvoke(priority, action);
    }
}
