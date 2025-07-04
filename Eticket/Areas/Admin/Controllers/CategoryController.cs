using Eticket.Data;
using Eticket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eticket.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private ApplicationDbContext _context = new();
        public IActionResult Index()
        {

            var AllCategeory = _context.Categories.Include(e=>e.Movies);
            return View(AllCategeory.ToList()); 
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            _context.Add(category);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
