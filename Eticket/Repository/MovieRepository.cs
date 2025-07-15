using System.Linq.Expressions;
using System.Threading.Tasks;
using Eticket.Data;
using Eticket.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace Eticket.Repository
{
    public class MovieRepository
    {
        ApplicationDbContext context = new();
public async Task CreateAsync(Movie movie)
        {
          await context.Movies.AddAsync(movie);
        }

        public void Edit(Movie movie)
        {
            context.Movies.Update(movie);
        }

        public void Delete(Movie movie)
        {
            context.Movies.Remove(movie);
        }

        public async Task<IEnumerable<Movie>> GetAsync(Expression<Func<Movie, bool>>? fliter = null,
           Expression<Func<Movie, object>>[]? incluedies=null)
        {
            var movies = context.Movies.AsQueryable<Movie>();
            if (fliter is not null)
            {
                movies = movies.Where(fliter);
            }
            if (incluedies is not null)
            {
               foreach (var item in incluedies)
                {
                    movies=movies.Include(item);
                }
            }


            return await movies.ToListAsync();
        }

        public async Task<Movie?> GetOneAsync(Expression<Func<Movie, bool>>? fliter = null,
           Expression<Func<Movie, object>>[]? incluedies = null)
        {
            return (await GetAsync(fliter, incluedies)).FirstOrDefault();
        }



        public async  Task<bool> CommitAsync()
        {
            try
            {
               await context.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

        }


    }
}
