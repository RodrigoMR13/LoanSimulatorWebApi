using System.Text.Json;
using System.Text.Json.Serialization;

namespace Infrastructure.Serialization
{
    public class DecimalTwoPlacesConverter : JsonConverter<decimal>
    {
        public override decimal Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => Math.Round(reader.GetDecimal(), 2, MidpointRounding.AwayFromZero);

        public override void Write(Utf8JsonWriter writer, decimal value, JsonSerializerOptions options)
            => writer.WriteNumberValue(Math.Round(value, 2, MidpointRounding.AwayFromZero));
    }
}
