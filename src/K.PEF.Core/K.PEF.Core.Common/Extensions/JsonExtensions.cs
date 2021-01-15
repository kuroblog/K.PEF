using System.Text.Json;

namespace K.PEF.Core.Common.Extensions
{
    public static class JsonExtensions
    {
        public static string AsFormattedJsonString<TArg>(this TArg arg) => arg.AsJsonString(true);

        public static string AsJsonString<TArg>(this TArg arg, bool isFormatted = false) =>
            JsonSerializer.Serialize(arg, new JsonSerializerOptions { WriteIndented = isFormatted });

        public static TArg AsJsonObject<TArg>(this string jsonString) =>
            JsonSerializer.Deserialize<TArg>(jsonString);
    }
}
