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
            var favoritos = _upMeetDBContext.Favorites.ToList();
            var eventos = _upMeetDBContext.Events.ToList();
            List<FavoritesGet> response = new List<FavoritesGet>();

            foreach (Favorites favorito in favoritos)
            {
                List<Events> evento = eventos.Where(l => l.Id == favorito.Id).ToList();
                
                response.Add(new FavoritesGet() { Id = favorito.Id, Name = evento[0].Name });
            }
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
