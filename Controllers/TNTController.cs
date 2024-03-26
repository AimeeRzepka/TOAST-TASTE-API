using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Toast_and_Taste.Models;
using Toast_and_Taste.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Toast_and_Taste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TNTController : ControllerBase
    {
        private readonly TNTContext _tNTContext;

        public TNTController(TNTContext tNTContext)
        {
            _tNTContext = tNTContext;
        }

        // GET: api/<TNTController>
        [HttpGet]
        public IActionResult Get()
        {
            var favorites = _tNTContext.Favorites.ToList();
            var cheeses = _tNTContext.Cheeses.ToList();

            List<CheeseFavoriteModel> response = new List<CheeseFavoriteModel>();

            foreach (CheeseModel cheese in cheeses)
            {
                List<FavoriteModel> favorite = favorites.Where(l => l.CheeseId == cheese.Id).ToList();
                Boolean isFavorite = false;
                if (favorite.Count() != 0)
                {
                    isFavorite = true;
                }

                response.Add(new CheeseFavoriteModel() { Id = cheese.Id, Kind = cheese.Kind, WinePair = cheese.WinePair, IsFavorite = isFavorite });
            }
            return Ok(response);
            
        }
        
        // GET api/<TNTController>/5
        [HttpGet("{id}")]
        public IActionResult Get([FromRoute] int id)
        {

            var cheese = _tNTContext.Cheeses.Find(id);
            return cheese != null
                ? Ok(cheese)
                : NotFound();
        }

        // POST api/<TNTController>
        [HttpPost]
        public IActionResult Post([FromBody] CheeseModel value)
        {
            var cheese = _tNTContext.Cheeses.Add(value);

            _tNTContext.SaveChanges();

            var id = cheese.Entity.Id;
            return Created($"/api/tnt/{id}", value);
        }

        // PUT api/<TNTController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CheeseModel value)
        {
            return Created($"/api/[controller]/{id})", value);
        }

        // DELETE api/<TNTController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var ticket = _tNTContext.Cheeses.Find(id);
            var favoriteToRemove = _tNTContext.Favorites.FirstOrDefault(l => l.CheeseId == id);

            if (ticket != null)
            {
                if (favoriteToRemove != null)
                {                    
                    if (ticket.Id == favoriteToRemove.CheeseId)
                    {
                        _tNTContext.Favorites.Remove(favoriteToRemove);
                        _tNTContext.SaveChanges();
                    }
                   
                }
                _tNTContext.Cheeses.Remove(ticket);
                _tNTContext.SaveChanges();
                return NoContent();
            }

            return NotFound();
        }
    }
}
