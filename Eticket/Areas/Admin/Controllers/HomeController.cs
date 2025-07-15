using Eticket.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eticket.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =$"{SD.SuperAdmin},{SD.Admin}")]
    
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
