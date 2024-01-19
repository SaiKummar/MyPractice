using Microsoft.AspNetCore.Mvc;
using MyPractice.API.Models;

namespace MyPractice.API.Controllers
{
    [ApiController]
    [Route("api/Movies")]
    public class MoviesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<MoviesDto>> GetMovies()
        {
            return Ok(MoviesDatastore.Current.MoviesList);
        }

        [HttpGet("{id}")]
        public ActionResult<MoviesDto> GetMovie(int id)
        {
            var movie = MoviesDatastore.Current.MoviesList.FirstOrDefault(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }
    }
}
