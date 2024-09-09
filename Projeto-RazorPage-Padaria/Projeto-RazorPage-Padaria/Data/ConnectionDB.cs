using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Collections.Generic;
using Projeto_RazorPage_Padaria.Models;

namespace Projeto_RazorPage_Padaria.Data
{
    public class ConnectionDB : DbContext
    {
        public ConnectionDB(DbContextOptions<ConnectionDB> options)
            : base(options)
        {
        }
        public DbSet<Projeto_RazorPage_Padaria.Models.Costomers> Costumers { get; set; } = default!;
        public DbSet<Projeto_RazorPage_Padaria.Models.Product> Product { get; set; } = default!;

    }
}
