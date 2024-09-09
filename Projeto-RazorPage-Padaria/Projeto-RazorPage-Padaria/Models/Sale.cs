using Projeto_RazorPage_Padaria.Models;
using Projeto_RazorPage_Padaria.Enumerations;

namespace Projeto_RazorPage_Padaria.Models
{
    public class Sale
    {
        public int? Id { get; set; }
        public Costomers? Buyer
        {
            get; set;
        }
        public List<SalesItem> ProductList { get; set; } = new();
        public PaymentForm PaymentForm { get; set; }

        public Sale() { }


        public override string ToString()
        {
            string items = "";

            foreach (Product item in ProductList)
            {
                items += "" + item.ToString() + "\n";
            }
            return $"Id: {Id}\nBuyer:{Buyer}\nProductList:{items}\n Payment Form: {PaymentForm}\nFinal Price: {GetFinalPrice()}";
        }

        public string GenerateCoupom()
        {
            string items = "";

            foreach (Product item in ProductList)
            {
                items += "" + item.ToString() + "\n";
            }
            return $"Cupom Fiscal:\nBuyer:{Buyer.ToString()}\nProductList:{items}\nPayment Form: {PaymentForm}\nFinal Price: {GetFinalPrice()}";
        }

        public double GetFinalPrice()
        {
            double finalPrice = ProductList.Sum(x => x.Price * x.Quantity);

            return finalPrice;
        }
    };

}

