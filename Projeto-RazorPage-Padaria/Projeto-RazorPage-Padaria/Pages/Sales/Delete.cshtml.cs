using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Projeto_RazorPage_Padaria.Data;
using Projeto_RazorPage_Padaria.Models;
using Projeto_RazorPage_Padaria.Repository;

namespace Projeto_RazorPage_Padaria.Pages.Sales
{
    public class DeleteModel : PageModel
    {
        private SaleRepository _saleRepository;

        public DeleteModel(SaleRepository context)
        {
            _saleRepository = context;
        }

        [BindProperty]
        public Sale Sales { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sales = _saleRepository.FindById(id);

            if (sales == null)
            {
                return NotFound();
            }
            else
            {
                Sales = sales;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sales = _saleRepository.FindById(id);
            if (sales != null)
            {
                _saleRepository.Delete(id);
                
            }

            return RedirectToPage("./Index");
        }
    }
}
