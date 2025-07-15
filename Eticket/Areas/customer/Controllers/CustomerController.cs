using Eticket.Data;
using Eticket.Models;
using Eticket.Models.ViewModels;
using Eticket.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;


//using Eticket.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Eticket.Areas.customer.Controllers
{
    [Area("customer")]
    
    public class CustomerController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;


        public CustomerController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }


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
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login", "Account"); 

            return View(user);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return RedirectToAction("NotFoundPage", "Home");
            }

            var vm = new EditProfileVM
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditProfileVM editProfileVM)
        {
            if (!ModelState.IsValid)
            {
                return View(editProfileVM);
            }

            var user = await _userManager.FindByIdAsync(editProfileVM.Id);
            if (user == null)
            {
                return RedirectToAction("NotFoundPage", "Home");
            }

            user.FirstName = editProfileVM.FirstName;
            user.LastName = editProfileVM.LastName;
            user.Email = editProfileVM.Email;
            user.PhoneNumber = editProfileVM.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                TempData["success"] = "Profile Upadte Suessfully";
                return RedirectToAction("Profile", new { id = user.Id });
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }

            return View(editProfileVM);
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordVM1 changePasswordVM1)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Please fill all password fields correctly.";
                return RedirectToAction("Edit", new { id = _userManager.GetUserId(User) });
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var result = await _userManager.ChangePasswordAsync(user, changePasswordVM1.CurrentPassword, changePasswordVM1.NewPassword);

            if (result.Succeeded)
            {
                TempData["success"] = "Password changed successfully!";
                await _signInManager.RefreshSignInAsync(user);
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    TempData["error"] = error.Description;
                }
            }

            return RedirectToAction("Edit", new { id = user.Id });
        }


    }
}
