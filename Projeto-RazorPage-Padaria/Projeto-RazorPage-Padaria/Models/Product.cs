using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto_RazorPage_Padaria.Models
{
    [Table("products")]
    public class Product
    {
        [Column("id")]
        public int? Id { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("price")]
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
