namespace K.PEF.Logger.Infrastructures
{
    public interface ILogger
    {
        void Trace<TArg>(TArg arg);

        void Debug<TArg>(TArg arg);

        void Info<TArg>(TArg arg);

        void Error<TArg>(TArg arg);

        void Fatal<TArg>(TArg arg);
    }
}
