using Npgsql;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projeto_RazorPage_Padaria.Models
{
    [Table("customers")]

    public class Costomers
    {
        [Column("id")]
        public int? Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("document")]
        public string Document { get; set; }
        [Column("points")]
        public int Points { get; set; }

        private string connectionString = "Host=dpg-crcb7cjqf0us738ikg5g-a.oregon-postgres.render.com;Port=5432;Username=moutsmaster;Password=HLnW2jj3GqvAlyo2HLnmtdCdo4uL1TJ7;Database=mouts_padaria";

        public Costomers() { }
        public Costomers(string Name, string document)
        {
            this.Name = Name;
            this.Document = document;
        }

        public override string ToString()
        {
            return $"Id: {this.Id}\nName:{this.Name}\nDocument:{this.Document}\nPoints:{this.Points}";
        }

    }
}