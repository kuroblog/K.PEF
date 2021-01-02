using System;

namespace K.PEF.Logger
{
    public class NLogWrapper
    {
        private NLogWrapper() { }

        private static readonly Lazy<NLogWrapper> instance = new Lazy<NLogWrapper>(() => new NLogWrapper { });

        public static NLogWrapper Instance => instance?.Value;

        private NLog.Logger logger => NLog.LogManager.GetCurrentClassLogger();
        //private NLog.Logger logger => NLog.LogManager.GetLogger("MyLogger");

        public void Trace<TArg>(TArg arg) => logger.Trace(arg);

        public void Debug<TArg>(TArg arg) => logger.Debug(arg);

        public void Info<TArg>(TArg arg) => logger.Info(arg);

        public void Error<TArg>(TArg arg) => logger.Error(arg);

        public void Fatal<TArg>(TArg arg) => logger.Fatal(arg);
    }
}
