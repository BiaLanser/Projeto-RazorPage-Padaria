using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Projeto_RazorPage_Padaria.Data;
using Projeto_RazorPage_Padaria.Models;

namespace Projeto_RazorPage_Padaria.Pages.Costumers
{
    public class IndexModel : PageModel
    {
        private readonly Projeto_RazorPage_Padaria.Data.ConnectionDB _context;

        public IndexModel(Projeto_RazorPage_Padaria.Data.ConnectionDB context)
        {
            _context = context;
        }

        public IList<Costomers> Costumer { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Costumer = await _context.Costumers.ToListAsync();
        }
    }
}
