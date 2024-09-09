using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Projeto_RazorPage_Padaria.Data;
using Projeto_RazorPage_Padaria.Entities;
using Projeto_RazorPage_Padaria.Repository;

namespace Projeto_RazorPage_Padaria.Pages.Sales
{
    public class DetailsModel : PageModel
    {
        private SaleRepository saleRepository;

        public DetailsModel(SaleRepository context)
        {
            saleRepository = context;
        }

        public Sale Sales { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            
            return Page();
        }
    }
}
