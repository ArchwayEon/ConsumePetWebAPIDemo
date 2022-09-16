﻿using LecWebAPI.Models;
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

    public async Task<Pet?> ReadAsync(int id)
    {
        Pet? pet = null;
        // HTTP GET
        var response = await _client.GetAsync($"pet/one/{id}");

        if (response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();

            var serializeOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            pet = JsonSerializer.Deserialize<Pet>(json, serializeOptions);
        }
        return pet;
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

    public async Task<Pet?> CreateAsync(Pet pet)
    {
        var petData = new FormUrlEncodedContent(new Dictionary<string, string>()
        {
            ["Id"] = $"{pet.Id}",
            ["Name"] = $"{pet.Name}",
            ["Weight"] = $"{pet.Weight}"
        });
        var result = await _client.PostAsync("pet/create", petData);
        if (result.IsSuccessStatusCode)
        {
            return pet;
        }
        return null;
    }

    public async Task UpdateAsync(int oldId, Pet pet)
    {
        var petData = new FormUrlEncodedContent(new Dictionary<string, string>()
        {
            ["Id"] = $"{pet.Id}",
            ["Name"] = $"{pet.Name}",
            ["Weight"] = $"{pet.Weight}"
        });
        var result = await _client.PutAsync("pet/update", petData);

        if (!result.IsSuccessStatusCode)
        {
            // May want to do something here
        }
    }

    public async Task DeleteAsync(int id)
    {
        var result = await _client.DeleteAsync($"pet/delete/{id}");
        if (!result.IsSuccessStatusCode)
        {
            // May want to do something here
        }
    }
}
