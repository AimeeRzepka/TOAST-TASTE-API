using Microsoft.AspNetCore.Mvc;
using Toast_and_Taste.Models;
using Toast_and_Taste.Services;

namespace Toast_and_Taste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly TNTContext _TNTContext;

        public FavoriteController(TNTContext TNTContext)
        {
            _TNTContext = TNTContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var response = _TNTContext.Favorites.ToList();
            return Ok(response);
        }

        [HttpGet("{username}")]
        public IActionResult Get([FromRoute] string username)
        {

            var response = _TNTContext.Favorites.Where(l => l.UserName.ToLower() == username.ToLower()).ToList();
            return response != null
                ? Ok(response)
                : NotFound();
        }

        [HttpPost]
        public IActionResult Post([FromBody] FavoriteModel favorite)
        {
            var fav = _TNTContext.Favorites.Add(favorite);

            _TNTContext.SaveChanges();

            var id = fav.Entity.Id;
            return Created($"/api/tnt/{id}", favorite);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var user = _TNTContext.Favorites.Find(id);

            if (user != null)
            {
                _TNTContext.Favorites.Remove(user);
                _TNTContext.SaveChanges();
                return NoContent();
            }

            return NotFound();
        }
    }
}
