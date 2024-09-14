namespace Projeto_RazorPage_Padaria.Models
{
    public class SalesItem : Product
    {
        public int Quantity { get; set; }

        public SalesItem()
        {

        }

        public double returnQuantity()
        {
            return this.Price * this.Quantity;
        }
    }
}
