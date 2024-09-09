using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projeto_RazorPage_Padaria.Data;
using Projeto_RazorPage_Padaria.Models;
using Projeto_RazorPage_Padaria.Repository;

namespace Projeto_RazorPage_Padaria.Pages.Sales
{
    public class EditModel : PageModel
    {
        private SaleRepository saleRepository;

        public EditModel(SaleRepository context)
        {
            saleRepository = context;
        }

        [BindProperty]
        public Sale Sales { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sales =  saleRepository.FindById(id);
            if (sales == null)
            {
                return NotFound();
            }
            Sales = sales;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            return Page();
        }
    }
}
