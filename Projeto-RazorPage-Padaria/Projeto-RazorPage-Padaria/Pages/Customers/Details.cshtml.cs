﻿using System;
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
    public class DetailsModel : PageModel
    {
        private readonly Projeto_RazorPage_Padaria.Data.ConnectionDB _context;

        public DetailsModel(Projeto_RazorPage_Padaria.Data.ConnectionDB context)
        {
            _context = context;
        }

        public Models.Costomers Costumer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var costumer = await _context.Costumers.FirstOrDefaultAsync(m => m.Id == id);
            if (costumer == null)
            {
                return NotFound();
            }
            else
            {
                Costumer = costumer;
            }
            return Page();
        }
    }
}
