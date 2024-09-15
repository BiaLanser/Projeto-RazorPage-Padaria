using System.Text.Json;
using System.Text.Json.Serialization;

namespace Projeto_RazorPage_Padaria.Enumerations.Utilities
{
    public class PaymentFormConverter : JsonConverter<PaymentForm>
    {
        public override PaymentForm Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var enumValue = reader.GetString();

            if (Enum.TryParse(enumValue,
     out PaymentForm paymentForm))
            {
                return paymentForm;
            }

            throw new JsonException($"Invalid PaymentForm value: {enumValue}");
        }

        public override void Write(Utf8JsonWriter writer, PaymentForm value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
