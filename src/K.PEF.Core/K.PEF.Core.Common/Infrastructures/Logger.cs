using System;

namespace K.PEF.Core.Common.Infrastructures
{
    public enum LogType
    {
        Debug,
        Information,
        Warning,
        Error,
        Fatal
    }

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

    public interface ILogger
    {
        void Log(LogEntry entry);

        void PushProperty(string name, object value, bool destructureObjects = false);
    }

    public interface ILogger<T> : ILogger where T : class { }

    public static class ILoggerExtensions
    {
        public static void Debug(this ILogger logger, string messageOrTemplate, params object[] templateArgs) =>
            logger.Log(new LogEntry(LogType.Debug, messageOrTemplate, templateArgs: templateArgs));

        public static void Information(this ILogger logger, string messageOrTemplate, params object[] templateArgs) =>
            logger.Log(new LogEntry(LogType.Information, messageOrTemplate, templateArgs: templateArgs));

        public static void Warning(this ILogger logger, string messageOrTemplate, params object[] templateArgs) =>
            logger.Log(new LogEntry(LogType.Warning, messageOrTemplate, templateArgs: templateArgs));

        public static void Error(this ILogger logger, Exception exception, string messageOrTemplate = null, params object[] templateArgs) =>
            logger.Log(new LogEntry(LogType.Error, string.IsNullOrEmpty(messageOrTemplate) ? exception.Message : messageOrTemplate, exception, templateArgs: templateArgs));

        public static void Fatal(this ILogger logger, Exception exception, string messageOrTemplate = null, params object[] templateArgs) =>
            logger.Log(new LogEntry(LogType.Fatal, string.IsNullOrEmpty(messageOrTemplate) ? exception.Message : messageOrTemplate, exception, templateArgs: templateArgs));
    }
}
