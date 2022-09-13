using LecWebAPI.Models;

namespace ConsumePetWebAPIDemo.Services;

public interface IPetRepository
{
    Task<ICollection<Pet>> ReadAllAsync();
    Task<Pet?> ReadAsync(int id);
}

