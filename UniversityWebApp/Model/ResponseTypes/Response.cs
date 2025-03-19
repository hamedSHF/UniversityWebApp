using System.Text.Json.Serialization;

namespace UniversityWebApp.Model.ResponseTypes
{
    public class Response(string? message,string? content,ResponseActions action)
    {
        public string? Message { get; set; } = message;
        public string? Content { get; set; } = content;
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ResponseActions Action { get; set; } = action;
    }
}
