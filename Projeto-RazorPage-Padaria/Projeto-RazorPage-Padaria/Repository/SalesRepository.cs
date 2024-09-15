using Npgsql;
using Projeto_RazorPage_Padaria.Models;
using Projeto_RazorPage_Padaria.Enumerations;


namespace Projeto_RazorPage_Padaria.Repository
{
    public class SaleRepository : IRepository<Sale>
    {

        private string _connectionString = "Host=dpg-crcb7cjqf0us738ikg5g-a.oregon-postgres.render.com;Port=5432;Username=moutsmaster;Password=HLnW2jj3GqvAlyo2HLnmtdCdo4uL1TJ7;Database=mouts_padaria";
        public override Sale Alter(Sale t)
        {
            throw new NotImplementedException();
        }

        public override Sale Create(Sale t)
        {
            if(t.Id is not null)
            {
                throw new InvalidDataException("Sale already has an id");
            }
            else if (t.Buyer.Id is null)
            {
                t.Buyer.Id = 12;
            }
           /*List<SalesItem> productsLackingId = t.ProductList.Where(x => x.Id is null).ToList();
            if (productsLackingId.Count > 0) {
                throw new InvalidDataException("There an item in the sale which doesn´t have an Id");
            }*/
            string insertSaleQuery = "insert into sales(customerid, payment_form) values (@customerId, @paymentForm) returning id";
            string insertSalesItemQuery = "insert into salesproducts(saleid, productid, quantity) values(@saleId, @productId, @quantity)";
            using (NpgsqlConnection connection = new(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction()) 
                {
                    
                    using (var command = new NpgsqlCommand(insertSaleQuery, connection))
                    {
                        command.Parameters.AddWithValue("@customerId", t.Buyer.Id);
                        command.Parameters.AddWithValue("@paymentForm", t.PaymentForm.ToString());
                        t.Id = Convert.ToInt32(command.ExecuteScalar());
                    }

                    using (var command = new NpgsqlCommand(insertSalesItemQuery, connection))
                    {
                        foreach (var salesItem in t.ProductList)
                        {
                            command.Parameters.Clear(); // Clear previous parameters for each iteration
                            command.Parameters.AddWithValue("@saleId", t.Id);
                            command.Parameters.AddWithValue("@productId", salesItem.Id);
                            command.Parameters.AddWithValue("@quantity", salesItem.Quantity);
                            command.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                    if (t.Buyer.Id != 12)
                    {
                        int points = CalculatePoints(t.GetFinalPrice());
                        AlterCustomerPoints(t.Buyer.Id, points);
                    }
                }
            }

             
            return t;
        }

        public override void Delete(int id)
        {
            string deleteGeneralSale = "delete from sales where id =@saleId";
            string deleteProductSales = "delete from salesproducts where saleid = @prodSaleId";
            using (NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    using (NpgsqlCommand command = new(deleteProductSales, connection))
                    {
                        command.Parameters.AddWithValue("@prodSaleId", id);
                        command.ExecuteNonQuery();
                    }
                    using (NpgsqlCommand command = new(deleteGeneralSale, connection))
                    {
                        command.Parameters.AddWithValue("@saleId", id);
                        command.ExecuteNonQuery();
                    }
                    transaction.Commit();
                }
            }
        }

        public override List<Sale> FindAll()
        {
            string generalSaleQuery = "select s.id as id_venda,s.payment_form, custom.id as customer_id, custom.\"document\" as \"document\", custom.\"name\" as customer, custom.points from sales s inner join customers custom on s.customerid = custom.id order by s.id desc";
            List<Sale> salesList = new List<Sale>();
            using (NpgsqlConnection connection = new(_connectionString))
            {
                connection.Open();
                using (NpgsqlCommand command = new(generalSaleQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PaymentForm form;

                            switch (reader.GetString(1))
                            {
                                case "CREDIT_CARD":
                                    {
                                        form = PaymentForm.CREDIT_CARD;
                                        break;
                                    }
                                case "DEBIT_CARD":
                                    {
                                        form = PaymentForm.DEBIT_CARD;
                                        break;
                                    }
                                case "CASH":
                                    {
                                        form = PaymentForm.CASH;
                                        break;
                                    }
                                case "DIGITAL_WALLET":
                                    {
                                        form = PaymentForm.DIGITAL_WALLET;
                                        break;
                                    }
                                default:
                                    {
                                        throw new ArgumentException("Invalid payment form");
                                    }
                            }


                            salesList.Add(new Sale
                            {
                                Id = reader.GetInt32(0),
                                PaymentForm = form,
                                Buyer = new Costomers()
                                {
                                    Id = reader.GetInt32(2),
                                    Document = reader.GetString(3),
                                    Name = reader.GetString(4),
                                    Points = reader.GetInt32(5),
                                }
                            });

                        }

                    }
                }
                foreach (Sale sales in salesList)
                {
                    string salesItemsQuery = "select item.itemid, p.description, p.price, item.quantity FROM salesproducts item INNER JOIN sales s ON s.id = item.saleid INNER JOIN products p ON p.id = item.productid WHERE s.id = @id";
                    using (NpgsqlCommand command = new(salesItemsQuery, connection))
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@id", sales.Id!);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                sales.ProductList.Add(new SalesItem
                                {
                                    Id = reader.GetInt32(0),
                                    Description = reader.GetString(1),
                                    Price = reader.GetFloat(2),
                                    Quantity = reader.GetInt32(3),
                                });
                            }
                        }

                    }

                }
            }


            return salesList;
        }

        public override Sale FindById(int id)
        {
            Sale sale = null;
            string generalSaleQuery = "SELECT s.id as id_venda, s.payment_form, custom.id as customer_id, custom.\"document\" as \"document\", custom.\"name\" as customer, custom.points " +
                                      "FROM sales s " +
                                      "INNER JOIN customers custom ON s.customerid = custom.id " +
                                      "WHERE s.id = @saleId ORDER BY data_cadastro ASC LIMIT 1";

            using (NpgsqlConnection connection = new(_connectionString))
            {
                connection.Open();

                // First query to get Sale and Customer
                using (NpgsqlCommand command = new(generalSaleQuery, connection))
                {
                    command.Parameters.AddWithValue("@saleId", id);

                    // Debug log for parameter and query
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            PaymentForm form;
                            switch (reader.GetString(1))  
                            {
                                case "CREDIT_CARD":
                                    form = PaymentForm.CREDIT_CARD;
                                    break;
                                case "DEBIT_CARD":
                                    form = PaymentForm.DEBIT_CARD;
                                    break;
                                case "CASH":
                                    form = PaymentForm.CASH;
                                    break;
                                case "DIGITAL_WALLET":
                                    form = PaymentForm.DIGITAL_WALLET;
                                    break;
                                default:
                                    throw new ArgumentException("Invalid payment form");
                            }

                            sale = new Sale
                            {
                                Id = reader.GetInt32(0),
                                PaymentForm = form,
                                Buyer = new Costomers
                                {
                                    Id = reader.GetInt32(2),
                                    Document = reader.GetString(3),
                                    Name = reader.IsDBNull(4) ? string.Empty : reader.GetString(4),
                                    Points = reader.GetInt32(5)
                                }
                            };
                        }
                        else
                        {
                            throw new InvalidOperationException("Sale not found.");
                        }
                    }
                }

                // Second query to get the SaleItems
                string salesItemsQuery = "SELECT item.productid, p.description, p.price, item.quantity " +
                                         "FROM salesproducts item " +
                                         "INNER JOIN sales s ON s.id = item.saleid " +
                                         "INNER JOIN products p ON p.id = item.productid " +
                                         "WHERE s.id = @saleId";

                using (NpgsqlCommand command = new(salesItemsQuery, connection))
                {
                    command.Parameters.AddWithValue("@saleId", sale.Id);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sale.ProductList.Add(new SalesItem
                            {
                                Id = reader.GetInt32(0),
                                Description = reader.GetString(1),
                                Price = reader.GetDouble(2),
                                Quantity = reader.GetInt32(3)
                            });
                        }
                    }
                }

                return sale;
            }
        }



        private int CalculatePoints(double saleValue)
        {
            double total = 0.00;
            int points = 0;

            while(total < saleValue)
            {
                total += 5.00;
                points++;
            }
            return points;
        }
        private void AlterCustomerPoints(int? customerId, int points)
        {

            string updateQuery = "update customers set points = points + @points where id = @customerid";

            using(NpgsqlConnection connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                using(NpgsqlCommand command = new NpgsqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@points", points);
                    command.Parameters.AddWithValue("@customerid", customerId);

                    command.ExecuteNonQuery();
                }
            }
        }
    }

}