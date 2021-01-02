using System;

namespace K.PEF.Common.Extensions
{
    public static class GuidExtensions
    {
        public static string AsFormatN(this Guid guid, string format = "N", bool isUpper = false)
        {
            var guidString = guid.ToString(format);
            return isUpper ? guidString.ToUpper() : guidString.ToLower();
        }
    }
}
