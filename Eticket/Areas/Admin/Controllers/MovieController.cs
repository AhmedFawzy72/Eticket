using System.Linq.Expressions;
using System.Threading.Tasks;
using Eticket.Data;
using Eticket.Models;
using Eticket.Models.ViewModels;
using Eticket.Repository;
using Eticket.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eticket.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = $"{SD.SuperAdmin},{SD.Admin}")]

    public class MovieController : Controller
    {
        private ApplicationDbContext _context = new();
        private MovieRepository movieRepository = new();
        public async Task<IActionResult> Index()
        {
            //var AllMovie = _context.Movies.Include(e => e.Category).Include(e => e.Cinema);
            var allMovies = await movieRepository.GetAsync(incluedies: [e=>e.Category,e=>e.Cinema]);
           
            return View(allMovies);
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
        public async Task<IActionResult> Create(Movie movie)
        {
         await  movieRepository.CreateAsync(movie);
         await movieRepository.CommitAsync();
          return RedirectToAction(nameof(Index));
        
        }
        public async Task<IActionResult> Delete(int id)
        {
            var movie = await movieRepository.GetOneAsync(e => e.Id == id);
            if(movie is not null)
            {
             movieRepository.Delete(movie);

            }
            await movieRepository.CommitAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
