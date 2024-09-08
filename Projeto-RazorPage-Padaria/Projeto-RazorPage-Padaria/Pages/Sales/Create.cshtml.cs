using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projeto_RazorPage_Padaria.Data;
using Projeto_RazorPage_Padaria.Entities;
using Projeto_RazorPage_Padaria.Enumerations;
using Projeto_RazorPage_Padaria.Enumerations.Utilities;
using Projeto_RazorPage_Padaria.Repository;

namespace Projeto_RazorPage_Padaria.Pages.Sales
{
    public class CreateModel : PageModel
    {
        private SaleRepository _salesRepository; 
        public IEnumerable<SelectListItem> PaymentFormsAvailable { get; set; }
        public CreateModel(SaleRepository context)
        {
            PaymentFormsAvailable = EnumUtilities.GetSelectList<PaymentForm>();
            _salesRepository = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Sale Sales { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            

            return RedirectToPage("./Index");
        }
    }
}
