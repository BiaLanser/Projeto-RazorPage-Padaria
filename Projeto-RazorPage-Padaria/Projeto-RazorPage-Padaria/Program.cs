using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Projeto_RazorPage_Padaria.Data;
using Projeto_RazorPage_Padaria.Repository;
using System.Net.Http.Headers;

namespace Projeto_RazorPage_Padaria
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Inicialização da aplicação
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<ConnectionDB>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectionDB") ?? throw new InvalidOperationException("Connection string 'AulaDb' not found.")));

            builder.Services.AddControllers();
            builder.Services.AddAntiforgery();
            builder.Services.AddSingleton<SaleRepository>();
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configuração do pipeline de requisição HTTP
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.MapRazorPages();

            // Executa a aplicação
            app.Run();

            // Fazer a requisição HTTP
            await ProcessRepositoriesAsync();
        }

        // Método separado para realizar a requisição
        static async Task ProcessRepositoriesAsync()
        {
            using HttpClient client = new();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            // Faz a requisição para a API do GitHub
            var json = await client.GetStringAsync("http://localhost:7212/api/v1/Costumer");

            Console.WriteLine(json);

        }
    }
}
