using AngularProjectAPI.Models;
using AngularProjectAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace AngularProjectAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : Controller
    {
        private readonly UpMeetDBContext _upMeetDBContext;

        public EventsController(UpMeetDBContext upMeetDBContext)
        {
            _upMeetDBContext = upMeetDBContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var response = _upMeetDBContext.Events.ToList();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var evento = _upMeetDBContext.Events.Find(id);

            if (evento == null)
            {
                return NotFound();
            }

            return Ok(evento);
        }

        [HttpPost]
        public IActionResult Post([FromBody] PostEvents postEvents)
        {
            var evento = new Events();
            evento.Name = postEvents.Name;
            evento.Description = postEvents.Description;
            evento.StartDate = postEvents.StartDate;
            evento.Location = postEvents.Location;
            evento.Duration = postEvents.Duration;

            _upMeetDBContext.Events.Add(evento);
            _upMeetDBContext.SaveChanges();

            return Created($"/Events/{evento.Id}", evento);

        }

        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] PostEvents postEvents)
        {
            var evento = _upMeetDBContext.Events.Find(id);

            if (evento != null)
            {
                evento.Id = id;
                evento.Name = postEvents.Name;
                evento.Description = postEvents.Description;
                evento.StartDate = postEvents.StartDate;
                evento.Location = postEvents.Location;
                evento.Duration = postEvents.Duration;

                _upMeetDBContext.Events.Update(evento);
                _upMeetDBContext.SaveChanges();

                return NoContent();
            }

            return NotFound();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var evento = _upMeetDBContext.Events.Find(id);

            if (evento != null)
            {
                _upMeetDBContext.Events.Remove(evento);
                _upMeetDBContext.SaveChanges();

                return NoContent();
            }

            return NotFound();
        }
    }
}
