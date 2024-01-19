using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using MyPractice.API.Models;

namespace MyPractice.API.Controllers
{
    [Route("api/movies/{movieId}/Actors")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<ActorDto>> GetActors(int movieId)
        {
            var city = MoviesDatastore.Current.MoviesList.FirstOrDefault(m => m.Id == movieId);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city.Actors);
        }

        [HttpGet("{actorId}", Name = "GetActor")]
        public ActionResult<ActorDto> GetActor(int movieId, int actorId)
        {
            var city = MoviesDatastore.Current.MoviesList.FirstOrDefault(m => m.Id == movieId);
            if (city == null)
                return NotFound();
            var actor = city.Actors.FirstOrDefault(a => a.Id == actorId);
            if (actor == null)
                return NotFound();
            return Ok(actor);
        }

        [HttpPost]
        public ActionResult<ActorDto> CreateActor(int movieId, [FromBody] ActorCreationDto actor)
        {
            var movie = MoviesDatastore.Current.MoviesList.FirstOrDefault(m => m.Id == movieId);
            if (movie == null)
                return NotFound();
            int nextActorId = movie.Actors.Max(a => a.Id);
            var newactor = new ActorDto { Id = ++nextActorId, Name = actor.Name };
            movie.Actors.Add(newactor);
            return CreatedAtRoute("GetActor",
            new
            {
                movieId = movieId,
                actorId = newactor.Id
            }, newactor);
        }

        [HttpPut("{actorId}")]
        public ActionResult UpdateActor(int movieId, int actorId, ActorUpdateDto actor)
        {
            var movie = MoviesDatastore.Current.MoviesList.FirstOrDefault(m => m.Id == movieId);
            if (movie == null)
                return NotFound();
            var actorFromStore = movie.Actors.FirstOrDefault(a => a.Id == actorId);
            if (actorFromStore == null)
                return NotFound();
            actorFromStore.Name = actor.Name;
            return NoContent();
        }

        [HttpPatch("{actorId}")]
        public ActionResult PartiallyUpdateActor(int movieId, int actorId, JsonPatchDocument<ActorUpdateDto> actor)
        {
            try
            {

                var movie = MoviesDatastore.Current.MoviesList.FirstOrDefault(m => m.Id == movieId);
                if (movie == null)
                    return NotFound();
                var actorFromStore = movie.Actors.FirstOrDefault(a => a.Id == actorId);
                if (actorFromStore == null)
                    return NotFound();
                var actorUpdateDtoObject = new ActorUpdateDto
                {
                    Name = actorFromStore.Name
                };
                actor.ApplyTo(actorUpdateDtoObject, ModelState);
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                if (!TryValidateModel(actorUpdateDtoObject))
                {
                    return BadRequest(ModelState);
                }
                actorFromStore.Name = actorUpdateDtoObject.Name;
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{actorId}")]
        public ActionResult DeleteActor(int movieId, int actorId)
        {
            var movie = MoviesDatastore.Current.MoviesList.FirstOrDefault(m => m.Id == movieId);
            if (movie == null)
                return NotFound();
            var actor = movie.Actors.FirstOrDefault(a => a.Id == actorId);
            if (actor == null)
                return NotFound();
            movie.Actors.Remove(actor);
            return NoContent();
        }
    }
}
