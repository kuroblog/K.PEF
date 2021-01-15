using System;
using System.Windows.Threading;

namespace K.PEF.Core.Common.Interfaces
{
    public interface IMainDispatcher
    {
        void Invoke(Action action, DispatcherPriority priority = DispatcherPriority.Normal);

        void BeginInvoke(Action action, DispatcherPriority priority = DispatcherPriority.Normal);
    }
}
