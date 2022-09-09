using LecWebAPI.Models;
using System.Text.Json;

namespace ConsumePetWebAPIDemo.Services;

public class WebAPIPetRepository : IPetRepository
{
    private readonly HttpClient _client;

    public WebAPIPetRepository(HttpClient client)
    {
        _client = client;
        _client.BaseAddress = new Uri("https://localhost:7289/api/");
    }

    public async Task<ICollection<Pet>> ReadAllAsync()
    {
        List<Pet>? pets = null;
        // HTTP GET
        var response = await _client.GetAsync("pet/all");

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();

            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            pets = JsonSerializer.Deserialize<List<Pet>>(json, serializeOptions);
        }
        pets ??= new List<Pet>();
        return pets;
    }
}
