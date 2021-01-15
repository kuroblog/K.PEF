using K.PEF.Core.Common.Models;

namespace K.PEF.Core.Common.Interfaces
{
    public interface ILogger
    {
        void Log(LogEntry entry);

        void PushProperty(string name, object value, bool destructureObjects = false);
    }

    public interface ILogger<T> : ILogger where T : class { }
}
