using K.PEF.Core.Common.Models;
using K.PEF.Core.Common.Models.Types;
using Serilog;
using ci = K.PEF.Core.Common.Interfaces;

namespace K.PEF.Core.Shell.Infrastructures
{
    public class LogWrapper : ci.ILogger
    {
        private readonly ILogger _adapter = null;

        public LogWrapper(ILogger adapter) => _adapter = adapter;

        public void Log(LogEntry entry)
        {
            switch (entry.LogType)
            {
                case LogType.Debug:
                    _adapter.Debug(entry.Exception, entry.MessageOrTemplate, entry.TemplateArgs);
                    break;
                case LogType.Information:
                    _adapter.Information(entry.Exception, entry.MessageOrTemplate, entry.TemplateArgs);
                    break;
                case LogType.Warning:
                    _adapter.Warning(entry.Exception, entry.MessageOrTemplate, entry.TemplateArgs);
                    break;
                case LogType.Error:
                    _adapter.Error(entry.Exception, entry.MessageOrTemplate, entry.TemplateArgs);
                    break;
                case LogType.Fatal:
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
