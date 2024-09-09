using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projeto_RazorPage_Padaria.Data;
using Projeto_RazorPage_Padaria.Models;

namespace Projeto_RazorPage_Padaria.Pages.Costumers
{
    public class CreateModel : PageModel
    {
        private readonly Projeto_RazorPage_Padaria.Data.ConnectionDB _context;

        public CreateModel(Projeto_RazorPage_Padaria.Data.ConnectionDB context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Costomers Costumer { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Costumers.Add(Costumer);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
