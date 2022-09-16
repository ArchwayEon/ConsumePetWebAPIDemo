using LecWebAPI.Models;

namespace ConsumePetWebAPIDemo.Services;

public interface IPetRepository
{
    Task<ICollection<Pet>> ReadAllAsync();
    Task<Pet?> ReadAsync(int id);
    Task<Pet?> CreateAsync(Pet pet);
    Task UpdateAsync(int oldId, Pet pet);
    Task DeleteAsync(int id);
}

