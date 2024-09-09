using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projeto_RazorPage_Padaria.Models;
using Projeto_RazorPage_Padaria.Repository;

namespace Projeto_RazorPage_Padaria.Pages.Sales
{
    public class IndexModel : PageModel
    {
        private SaleRepository _salesRepository;

        public IndexModel(SaleRepository context)
        {
            _salesRepository = context;
        }

        public List<Sale> Sales { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Sales = _salesRepository.FindAll();
        }
    }
}
