using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Projeto_RazorPage_Padaria.Data;
using Projeto_RazorPage_Padaria.Repository;
namespace Projeto_RazorPage_Padaria
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<ConnectionDB>(options =>


                options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionDB") ?? throw new InvalidOperationException("Connection string 'AulaDb' not found.")));

            builder.Services.AddControllers();
            builder.Services.AddAntiforgery();
            builder.Services.AddSingleton<SaleRepository>();
            builder.Services.AddRazorPages();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
