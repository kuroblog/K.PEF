using Serilog;
using ci = K.PEF.Core.Common.Infrastructures;

namespace K.PEF.Core.Shell.Infrastructures
{
    public class LogWrapper : ci.ILogger
    {
        private readonly ILogger _adapter = null;

        public LogWrapper(ILogger adapter) => _adapter = adapter;

        public void Log(ci.LogEntry entry)
        {
            switch (entry.LogType)
            {
                case ci.LogType.Debug:
                    _adapter.Debug(entry.Exception, entry.MessageOrTemplate, entry.TemplateArgs);
                    break;
                case ci.LogType.Information:
                    _adapter.Information(entry.Exception, entry.MessageOrTemplate, entry.TemplateArgs);
                    break;
                case ci.LogType.Warning:
                    _adapter.Warning(entry.Exception, entry.MessageOrTemplate, entry.TemplateArgs);
                    break;
                case ci.LogType.Error:
                    _adapter.Error(entry.Exception, entry.MessageOrTemplate, entry.TemplateArgs);
                    break;
                case ci.LogType.Fatal:
                    _adapter.Fatal(entry.Exception, entry.MessageOrTemplate, entry.TemplateArgs);
                    break;
                default:
                    break;
            }
        }

        public void PushProperty(string name, object value, bool destructureObjects = false) =>
            Serilog.Context.LogContext.PushProperty(name, value, destructureObjects);
    }

    public class LogWrapper<T> : LogWrapper, ci.ILogger<T> where T : class
    {
        public LogWrapper(ILogger logger) : base(logger.ForContext<T>()) { }
    }
}
