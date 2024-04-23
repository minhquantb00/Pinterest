using Newtonsoft.Json;
using System.Text.Json.Serialization;
using JsonConverter = System.Text.Json.Serialization.JsonConverter;
using JsonConverterAttribute = Newtonsoft.Json.JsonConverterAttribute;

namespace Project_Pinterest.Constants
{
    public static class Enums
    {
        public enum FileType
        {
            Document,
            Pdf,
            Image,
            Video
        }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum Action
        {
            FOLLOW,
            UNFOLLOW,
            NOTCONSTRAINT
        }
    }
}
