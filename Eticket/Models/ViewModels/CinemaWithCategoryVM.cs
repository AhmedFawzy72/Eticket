using Eticket.Migrations;

namespace Eticket.Models.ViewModels
{
    public class CinemaWithCategoryVM
    {
        public List<Category> categories { get; set; } = null!;
        public List<Cinema> cinemas { get; set; }=null!;
        public Movie Movie { get; set; } = new Movie();

    }
}
