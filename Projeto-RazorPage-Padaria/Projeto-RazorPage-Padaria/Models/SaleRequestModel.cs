using Projeto_RazorPage_Padaria.Enumerations;
using Projeto_RazorPage_Padaria.Enumerations.Utilities;
using System.Text.Json.Serialization;

namespace Projeto_RazorPage_Padaria.Models
{
    public class SaleRequestModel
    {
        public int CustomerId { get; set; }
        public List<SalesItem> SalesItems { get; set; } = new();
        [JsonConverter(typeof(PaymentFormConverter))]

        public PaymentForm PaymentForm { get; set; }
    }
}
