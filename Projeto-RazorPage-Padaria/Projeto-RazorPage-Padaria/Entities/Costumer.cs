namespace Projeto_RazorPage_Padaria.Entities
{

        public class Costumer
        {
            public int? Id { get; set; }
            public string Name { get; set; }
            public string Document { get; set; }
            public int Points { get; set; }
            private string connectionString = "Host=dpg-crcb7cjqf0us738ikg5g-a.oregon-postgres.render.com;Port=5432;Username=moutsmaster;Password=HLnW2jj3GqvAlyo2HLnmtdCdo4uL1TJ7;Database=mouts_padaria";

            public Costumer() { }
            public Costumer(string Name, string document)
            {
                this.Name = Name;
                this.Document = document;
            }

            public override string ToString()
            {
                return $"Id: {this.Id}\nName:{this.Name}\nDocument:{this.Document}\nPoints:{this.Points}";
            }

            /*public void AddPoints(decimal value)
            {
                if (value < 0)
                {

                    throw new ArgumentOutOfRangeException("Value below zero");
                }
                decimal soma = 0;
                decimal valor = 10;
                int points = 0;
                while (soma < value)
                {
                    points++;
                    soma += valor;
                }
                try
                {
                    string updateQuery = "UPDATE customers SET points = points + @points WHERE id = @id";
                    using (var connection = new NpgsqlConnection(connectionString))
                    {
                        connection.Open();
                        using (var command = new NpgsqlCommand(updateQuery, connection))
                        {
                            command.Parameters.AddWithValue("@points", points);
                            command.Parameters.AddWithValue("@id", this.Id);
                            command.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception)
                {
                    // Rethrow the caught exception
                    throw;
                }
            }*/
        }
}
