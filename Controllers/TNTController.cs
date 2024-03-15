using Microsoft.AspNetCore.Mvc;
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
            var response = _tNTContext.Cheeses.ToList();
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

            if (ticket != null)
            {
                _tNTContext.Cheeses.Remove(ticket);
                _tNTContext.SaveChanges();
                return NoContent();
            }

            return NotFound();
        }
    }
}
