using PizzaFrontEnd.Models;
using System.Text.Json;

namespace PizzaFrontEnd.Services;

public class PizzaService : IPizzaService
{
    private readonly IHttpClientFactory _clientFactory;
    private const string apiEndpoint = "/api/pizzainfo/";
    private IEnumerable<PizzaInfo> pizzas;
    private readonly JsonSerializerOptions _options;

    public PizzaService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<IEnumerable<PizzaInfo>> GetPizzasAsync()
    {
        var client = _clientFactory.CreateClient("PizzaInfo");
        using (var response = await client.GetAsync(apiEndpoint))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                pizzas = await JsonSerializer
                        .DeserializeAsync<IEnumerable<PizzaInfo>>(apiResponse, _options);
            }
            else
            {
                return null;
            }
        }
        return pizzas;
    }
}