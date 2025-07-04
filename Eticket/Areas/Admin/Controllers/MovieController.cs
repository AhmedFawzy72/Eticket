using Eticket.Data;
using Eticket.Models;
using Eticket.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eticket.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MovieController : Controller
    {
        private ApplicationDbContext _context = new();

        public IActionResult Index()
        {
            var AllMovie = _context.Movies.Include(e => e.Category).Include(e => e.Cinema);

            return View(AllMovie.ToList());
        }
        [HttpGet]
        public IActionResult Create()
        {
            var allcategoy = _context.Categories;
            var allcinema=_context.Cinemas;
              CinemaWithCategoryVM cienmawithcategory = new()
            {
                  categories=allcategoy.ToList(),
                  cinemas=allcinema.ToList(),   
            };

            return View(cienmawithcategory);
        }

        [HttpPost]
        public IActionResult Create(Movie movie)
        { 
           _context.Movies.Add(movie);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        
        }
    }
}
