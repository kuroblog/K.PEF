using K.PEF.Logger.Infrastructures;

namespace K.PEF.Logger
{
    public class NLogger : ILogger
    {
        public void Trace<TArg>(TArg arg) => NLogWrapper.Instance.Trace(arg);

        public void Debug<TArg>(TArg arg) => NLogWrapper.Instance.Debug(arg);

        public void Info<TArg>(TArg arg) => NLogWrapper.Instance.Info(arg);

        public void Error<TArg>(TArg arg) => NLogWrapper.Instance.Error(arg);

        public void Fatal<TArg>(TArg arg) => NLogWrapper.Instance.Fatal(arg);
    }
}
