using Projeto_RazorPage_Padaria.Enumerations;

namespace Projeto_RazorPage_Padaria.Entities
{
    public class Sale
    { 
        public int? Id { get; set; }
        public Costumer? Buyer
        {
            get;set;
        }
        public List<SalesItem> ProductList { get; set; } = new();
        public PaymentForm PaymentForm { get; set; }

        public Sale() { }


        public override string ToString()
        {
            string items = "";

            foreach (Product item in this.ProductList)
            {
                items += "" + item.ToString() + "\n";
            }
            return $"Id: {this.Id}\nBuyer:{this.Buyer}\nProductList:{items}\n Payment Form: {this.PaymentForm}\nFinal Price: {GetFinalPrice()}";
        }

        public string GenerateCoupom()
        {
            string items = "";

            foreach (Product item in this.ProductList)
            {
                items += "" + item.ToString() + "\n";
            }
            return $"Cupom Fiscal:\nBuyer:{this.Buyer.ToString()}\nProductList:{items}\nPayment Form: {this.PaymentForm}\nFinal Price: {GetFinalPrice()}";
        }

        public double GetFinalPrice()
        {
            double finalPrice = this.ProductList.Sum(x => x.Price * x.Quantity);

            return finalPrice;
        }
    };

}

