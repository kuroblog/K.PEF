using Newtonsoft.Json;

namespace K.PEF.Common.Extensions
{
    public static class JsonExtensions
    {
        public static string AsJsonString<TArg>(this TArg arg, bool isFormat = false) =>
            JsonConvert.SerializeObject(arg, isFormat ? Formatting.Indented : Formatting.None);

        public static string AsJsonFormatString<TArg>(this TArg arg) => arg?.AsJsonString(true);

        public static TArg ParseTo<TArg>(this string jsonString) =>
            JsonConvert.DeserializeObject<TArg>(jsonString);
    }
}
