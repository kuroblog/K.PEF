using K.PEF.Core.Common.Interfaces;
using K.PEF.Core.Common.Models;
using K.PEF.Core.Common.Models.Types;
using System;

namespace K.PEF.Core.Common.Extensions
{
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
