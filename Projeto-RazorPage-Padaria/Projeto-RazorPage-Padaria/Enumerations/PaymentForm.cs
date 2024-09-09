using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace Projeto_RazorPage_Padaria.Enumerations
{
    public enum PaymentForm
    {
        [Display(Name="Debit Card")]
        DEBIT_CARD,
        [Display(Name = "Credit Card")]
        CREDIT_CARD,
        [Display(Name = "Digital Wallet")]
        DIGITAL_WALLET,
        [Display(Name = "Cash")]
        CASH
    }
}
