using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ChinookStore.Web.Common;

public class DateOnlyAlwaysJsonConverter : JsonConverter<object>
{
    public override bool CanConvert(Type typeToConvert)
    {
        return typeToConvert == typeof(DateOnly) || typeToConvert == typeof(DateOnly?);
    }

    public override object? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        const int dateLength = 10;

        var stringValue = reader.GetString();
        if (stringValue == null)
        {
            if (typeToConvert == typeof(DateOnly?))
            {
                return null;
            }
            throw new FormatException("Value can't be null");
        }

        if (stringValue.Length < dateLength)
        {
            throw new FormatException("Invalid DateOnly format");
        }
        return DateOnly.ParseExact(stringValue[..dateLength], "O", CultureInfo.InvariantCulture);
    }
    public override void Write(Utf8JsonWriter writer, object? value, JsonSerializerOptions options)
    {
        if (value == null)
        {
            writer.WriteNullValue();
        }
        else
        {
            writer.WriteStringValue(((DateOnly)value).ToString("O"));
        }
    }
}
