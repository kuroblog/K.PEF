using K.PEF.Core.Common.Models.Types;
using System;

namespace K.PEF.Core.Common.Models
{
    public class LogEntry
    {
        public LogType LogType { get; private set; }

        public string MessageOrTemplate { get; private set; }

        public Exception Exception { get; private set; }

        public object[] TemplateArgs { get; private set; }

        public LogEntry(LogType logType, string messageOrTemplate, Exception exception = null, params object[] templateArgs)
        {
            if (messageOrTemplate == null)
            {
                throw new ArgumentNullException(nameof(messageOrTemplate));
            }

            if (messageOrTemplate == string.Empty)
            {
                throw new ArgumentException("empty", nameof(messageOrTemplate));
            }

            LogType = logType;
            MessageOrTemplate = messageOrTemplate;
            Exception = exception;
            TemplateArgs = templateArgs;
        }
    }
}
