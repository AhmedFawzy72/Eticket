using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace Eticket.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public MovieStatus Status { get; set; }
        public ICollection<MovieImage> Images { get; set; } = new List<MovieImage>();
        public DateTime StartDate { get; set; }
        public DateTime EndtDate { get; set; }

        [Display(Name="Cinema")]
        public int CinemaId { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public Cinema Cinema { get; set; } = new Cinema();
        public Category Category { get; set; } = new Category();
       
        public ICollection<ActorMovie> actorsMovies { get; set; } = new List<ActorMovie>();

    }


    public enum MovieStatus
    {
        coming ,
        available ,
        expire
    }
}
