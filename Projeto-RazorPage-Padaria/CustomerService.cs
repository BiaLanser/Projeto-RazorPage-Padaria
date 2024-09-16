public class CustomerService
{
    private readonly HttpClient _client;

    public CustomerService(HttpClient client)
    {
        _client = client;
        _client.BaseAddress = new Uri("https://sua-api.com/");
        _client.DefaultRequestHeaders.Accept.Clear();
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<bool> CreateCustomerAsync(Customers customer)
    {
        var jsonContent = new StringContent(
            System.Text.Json.JsonSerializer.Serialize(customer),
            Encoding.UTF8,
            "application/json");

        HttpResponseMessage response = await _client.PostAsync("api/customers", jsonContent);
        return response.IsSuccessStatusCode;
    }
}
