using ConsumePetWebAPIDemo.Services;
using LecWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ConsumePetWebAPIDemo.Controllers;

public class PetController : Controller
{
    private readonly IPetRepository _petRepo;

    public PetController(IPetRepository petRepo)
    {
        _petRepo = petRepo;
    }

    public async Task<IActionResult> Index()
    {
        var pets = await _petRepo.ReadAllAsync();
        return View(pets);
    }

    public async Task<IActionResult> Details(int id)
    {
        var pet = await _petRepo.ReadAsync(id);
        return View(pet);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Pet pet)
    {
        if (ModelState.IsValid)
        {
            await _petRepo.CreateAsync(pet);
            return RedirectToAction("Index");
        }
        return View(pet);
    }

}

