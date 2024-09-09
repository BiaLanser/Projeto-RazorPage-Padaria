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
            builder.Services.AddDbContext<Projeto_RazorPage_PadariaContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("Projeto_RazorPage_PadariaContext") ?? throw new InvalidOperationException("Connection string 'Projeto_RazorPage_PadariaContext' not found.")));

            // Add services to the container.
            builder.Services.AddRazorPages();

            builder.Services.AddSingleton<SaleRepository>();
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
