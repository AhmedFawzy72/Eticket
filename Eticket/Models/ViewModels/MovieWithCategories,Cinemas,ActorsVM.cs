using Microsoft.AspNetCore.Mvc.Rendering;

namespace Eticket.Models.ViewModels
{
    public class MovieWithCategories_Cinemas_ActorsVM
    {
        public List<SelectListItem> Categories { get; set; }
        public List<SelectListItem> Cinemas { get; set; }
        public List<SelectListItem> Actors { get; set; }
        public Movie Movie { get; set; }
    }
}
