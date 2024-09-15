using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projeto_RazorPage_Padaria.Data;
using Projeto_RazorPage_Padaria.Models;
using Projeto_RazorPage_Padaria.Enumerations;
using Projeto_RazorPage_Padaria.Enumerations.Utilities;
using Projeto_RazorPage_Padaria.Models;
using Projeto_RazorPage_Padaria.Repository;
using Microsoft.EntityFrameworkCore;

namespace Projeto_RazorPage_Padaria.Pages.Sales
{
    public class CreateModel : PageModel
    {
        private SaleRepository _salesRepository;
        private readonly Projeto_RazorPage_Padaria.Data.ConnectionDB _context;
        public PaymentForm SelectedPaymentForm { get; set; }
        public IEnumerable<SelectListItem> PaymentFormsAvailable { get; set; }
        public List<Costomers> CostumerList { get; set; } = new List<Costomers>();
        public List<Product> ProductList = new();
        public CreateModel(SaleRepository context, Projeto_RazorPage_Padaria.Data.ConnectionDB contextDb)
        {
            PaymentFormsAvailable = EnumUtilities.GetSelectList<PaymentForm>();
            _salesRepository = context;
            _context = contextDb;
        }

        public IActionResult OnGet()
        {
            CostumerList = _context.Costumers.AsNoTracking().ToList();
            ProductList = _context.Product.AsNoTracking().ToList();
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
