using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Projeto_RazorPage_Padaria.Models;

namespace Projeto_RazorPage_Padaria.Data
{
    public class Projeto_RazorPage_PadariaContext : DbContext
    {
        public Projeto_RazorPage_PadariaContext (DbContextOptions<Projeto_RazorPage_PadariaContext> options)
            : base(options)
        {
        }

        public DbSet<Projeto_RazorPage_Padaria.Models.Costomers> Costumer { get; set; } = default!;
        public DbSet<Sale> Sales { get; set; } = default!;
    }
}
