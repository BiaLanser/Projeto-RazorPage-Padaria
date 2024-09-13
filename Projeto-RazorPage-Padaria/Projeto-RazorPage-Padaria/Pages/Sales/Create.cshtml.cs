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
using System.Text.Json;
using System.Web.Mvc;

namespace Projeto_RazorPage_Padaria.Pages.Sales
{
    public class CreateModel : PageModel
    {
        private SaleRepository _salesRepository;
        private readonly Projeto_RazorPage_Padaria.Data.ConnectionDB _context;
        public PaymentForm SelectedPaymentForm { get; set; }
        public IEnumerable<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem> PaymentFormsAvailable { get; set; }
        public List<Costomers> CostumerList { get; set; } = new List<Costomers>();
        public List<Product> ProductList = new();

        [BindProperty]
        public SaleRequestModel SaleRequestModel { get; set; }
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
        [System.Web.Mvc.HttpPost]
        public async Task<IActionResult> OnPostAsync()
        {
            using var reader = new StreamReader(Request.Body);
            var body = await reader.ReadToEndAsync();
            var customerOrder = System.Text.Json.JsonSerializer.Deserialize<SaleRequestModel>(body, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            Console.WriteLine(customerOrder.CustomerId);

           // Console.WriteLine(sale.CustomerId);

            if(!ModelState.IsValid)
            {
               
                return Page();
            }
            Sale sale = new Sale();

            var customer = _context.Costumers.FirstOrDefault(c => c.Id == customerOrder.CustomerId);
            if(customer is null)
            {
                throw new InvalidDataException("Customer must not be null");
            }
            else
            {
                sale.Buyer = customer;
                sale.ProductList = customerOrder.SalesItems;
                sale.PaymentForm = customerOrder.PaymentForm ;
                try
                {
                    _salesRepository.Create(sale);
                }
                catch (Exception ex) { 
                    
                
                }
            }

            return RedirectToPage("/Sales/Index");
        }
    }
}
