using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace UniversityWebApp.Utility
{
    public class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        string format = "YYYY-MM-DD";
        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            if(!string.IsNullOrEmpty(value))
            {
                var span = value.AsMemory().Span;
                if (DateOnly.TryParseExact(span, format.AsMemory().Span, out DateOnly result))
                    return result;
            }
            throw new InvalidCastException();
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            var converted = value.ToShortDateString();
            if (!string.IsNullOrEmpty(converted))
                writer.WriteStringValue(converted);
            writer.Flush();
        }
    }
}
