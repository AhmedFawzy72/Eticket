using Eticket.Data;
using Eticket.Models;
using Eticket.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eticket.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{SD.SuperAdmin},{SD.Admin}")]

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
            //Response.Cookies.Append("Notification", "Add Categorey suessfully");
            TempData["Notifaction"] = "Add Suessfully";
            return RedirectToAction(nameof(Index));
        }
    }
}
