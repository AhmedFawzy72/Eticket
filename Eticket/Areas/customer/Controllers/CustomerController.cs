using Eticket.Data;
using Eticket.Models;
using Eticket.Models.ViewModels;

//using Eticket.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eticket.Areas.customer.Controllers
{
    [Area("customer")]
    public class CustomerController : Controller
    {


        private ApplicationDbContext _context  = new ();
        
        
        public IActionResult Index(MovieFilterVM movieFilterVM, int page=1)
        {
            
            IQueryable<Movie> movies = _context.Movies.Include(e => e.Cinema).Include(e => e.Category).Include(e => e.Images);
            var totalpage = Math.Ceiling(movies.Count() / 6.00);
            movies = movies.Skip((page - 1) * 6).Take(6);
            ViewBag.Totalpage = totalpage;
            ViewBag.Currentpage = page;
            if(movieFilterVM.MovieName is not null)
            {
                movies= movies.Where(e=>e.Name.Contains(movieFilterVM.MovieName));
            }
            if(movieFilterVM.CategoryName is not null)
            {
                movies=movies.Where(e=>e.Category.Name.Contains(movieFilterVM.CategoryName));
            }
            return View(movies.ToList());

        }




        public IActionResult Details([FromRoute] int id)
        {
            var movie = _context.Movies.Include(e => e.Cinema).Include(e => e.Category).Include(e => e.Images)
                 .FirstOrDefault(e => e.Id == id);
            var actormovies = _context.ActorsMovies.Include(e => e.Actor).Where(e => e.MovieId == id).Include(e => e.Actor).ToList();
            if (movie == null)
            {
                return NotFound();
            }
            var model = new ModelsAndActorMovieVM() 
            {
                actorMovies = actormovies,
                Movie = movie 
            };
            return View(model);

        }
        public IActionResult ActorDetails([FromRoute]int id)
        {
            var ActorData = _context.Actors.FirstOrDefault(e => e.Id == id);
            //var actorsWithMovies = _context.Actors.Include(e=> e.actorsMovies).ThenInclude(e => e.Movie).ToList();
           
            return View(ActorData);
        }
        public IActionResult AllCategory()
        {
            var allcategory = _context.Categories;
            return View(allcategory.ToList());
        }
        public IActionResult ShowMoviesByCategory(int categoryId)
        {
            var movies = _context.Movies.Where(e => e.CategoryId == categoryId).Include(e=>e.Images).ToList();

            return View(movies); 
        }
        public IActionResult AllCinema()
        {
            var allcinema = _context.Cinemas;
            return View(allcinema.ToList());
        }

        public IActionResult ShowMoviesByCinema(int cinemaId)
        {
            var movies = _context.Movies.Where(e => e.CinemaId == cinemaId).Include(e=>e.Images).ToList();

            return View(movies);
        }

    }
}
