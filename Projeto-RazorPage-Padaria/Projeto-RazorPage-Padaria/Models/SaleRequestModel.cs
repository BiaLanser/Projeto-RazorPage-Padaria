using Projeto_RazorPage_Padaria.Enumerations;
using System.Text.Json.Serialization;

namespace Projeto_RazorPage_Padaria.Models
{
    public class SaleRequestModel
    {
        public int CustomerId { get; set; }
        public List<SalesItem> SalesItems { get; set; }
        [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        public PaymentForm PaymentForm { get; set; }
    }
}
