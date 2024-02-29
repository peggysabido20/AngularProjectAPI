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

        [HttpPost]
        public IActionResult Post([FromBody] Favorites Favorites)
        {
            var favorito = new Favorites();
            favorito.Id = Favorites.Id;

            _upMeetDBContext.Favorites.Add(favorito);
            _upMeetDBContext.SaveChanges();

            return Created($"/Favorites/{favorito.Id}", favorito);

        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var favorito = _upMeetDBContext.Favorites.Find(id);

            if (favorito != null)
            {
                _upMeetDBContext.Favorites.Remove(favorito);
                _upMeetDBContext.SaveChanges();

                return NoContent();
            }

            return NotFound();
        }
    }
}
