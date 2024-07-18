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

        public async Task<IActionResult> Index()
        {
            return View(await _context.Dishes.ToListAsync());
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
