using System.Text.Json.Serialization;

namespace Project_Pinterest.Constants
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ReportType
    {
        Post,
        User
    }
}
