using System;

namespace K.PEF.Core.Common.Extensions
{
    public static class ValidationExtensions
    {
        public static void ThrowExceptionWhenArgIsNullOrEmpty<TArg>(this TArg arg)
        {
            if (arg == null)
            {
                throw new ArgumentNullException(typeof(TArg).Name);
            }

            if (arg is String && arg.ToString() == string.Empty)
            {
                throw new ArgumentException("Empty", typeof(TArg).Name);
            }
        }
    }
}
