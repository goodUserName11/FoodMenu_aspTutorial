using FoodMenu.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace FoodMenu.Controllers
{
    public class FoodMenu : Controller
    {
        private readonly FoodMenuContext _context;

        public FoodMenu(FoodMenuContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string? searchString)
        {
            var dishes = _context.Dishes.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
                //searchString = searchString.ToLower();
                dishes = dishes.Where(d => d.Name.ToLower().Contains(searchString));

            return View(await dishes.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            var dish = await _context.Dishes
                .Include(di => di.DishIngridients)
                .ThenInclude(i => i.Ingredient)
                .FirstOrDefaultAsync(x => x.Id == id);

            if(dish is null)
                return NotFound();

            return View(dish);
        }
    }
}
