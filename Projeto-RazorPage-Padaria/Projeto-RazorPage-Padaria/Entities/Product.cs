namespace Projeto_RazorPage_Padaria.Entities
{
    public class Product
    {
        public int? Id { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }

        public Product() { }
        public Product(string Description, double Price)
        {
            this.Description = Description;
            this.Price = Price;
        }
        public override string ToString()
        {
            return $"Id: {this.Id}\nDescription:{this.Description}\nPrice:{this.Price}";
        }

    }
}
