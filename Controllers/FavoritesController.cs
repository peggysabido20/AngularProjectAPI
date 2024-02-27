using AngularProjectAPI.Models;
using AngularProjectAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AngularProjectAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : Controller
    {
        private readonly UpMeetDBContext _upMeetDBContext;

        public FavoritesController(UpMeetDBContext upMeetDBContext)
        {
            _upMeetDBContext = upMeetDBContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var response = _upMeetDBContext.Favorites.ToList();
            return Ok(response);
        }

        [HttpGet("{username}")]
        public IActionResult Get([FromRoute] string username)
        {
            var response = _upMeetDBContext.Favorites.ToList();
            List<Favorites> favorito = response.Where(l => l.UserName == username).ToList();

            if (favorito == null)
            {
                return NotFound();
            }

            return Ok(favorito);
        }

        [HttpPost]
        public IActionResult Post([FromBody] PostFavorites postFavorites)
        {
            var favorito = new Favorites();
            favorito.EventId = postFavorites.EventId;
            favorito.UserName = postFavorites.UserName;
            favorito.Favorite = postFavorites.Favorite;

            _upMeetDBContext.Favorites.Add(favorito);
            _upMeetDBContext.SaveChanges();

            return Created($"/Favorites/{favorito.Id}", favorito);

        }

        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] PostFavorites postFavorites)
        {
            var favorito = _upMeetDBContext.Favorites.Find(id);

            if (favorito != null)
            {
                favorito.Id = id;
                favorito.EventId = postFavorites.EventId;
                favorito.UserName = postFavorites.UserName;
                favorito.Favorite = postFavorites.Favorite;

                _upMeetDBContext.Favorites.Update(favorito);
                _upMeetDBContext.SaveChanges();

                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var favorito = _upMeetDBContext.Events.Find(id);

            if (favorito != null)
            {
                _upMeetDBContext.Events.Remove(favorito);
                _upMeetDBContext.SaveChanges();

                return NoContent();
            }

            return NotFound();
        }
    }
}
