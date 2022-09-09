using LecWebAPI.Models;

namespace ConsumePetWebAPIDemo.Services;

public interface IPetRepository
{
    Task<ICollection<Pet>> ReadAllAsync();
}

