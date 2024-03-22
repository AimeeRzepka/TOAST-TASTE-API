using Microsoft.AspNetCore.Mvc;
using Toast_and_Taste.Models;
using Toast_and_Taste.Services;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Toast_and_Taste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TNTContext _TNTContext;

        public UserController(TNTContext TNTContext)
        {
            _TNTContext = TNTContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var response = _TNTContext.Users.ToList();
            return Ok(response);
        }

        [HttpGet("{username}")]
        public IActionResult Get([FromRoute] string username)
        {

            var user = new UserModel();
            user = _TNTContext.Users.FirstOrDefault(l => l.Username.ToLower().Trim() == username.ToLower().Trim());
            if (user != null)
            {                
                var response = new UserModel();
                response.Id = user.Id;
                response.Username = user.Username;
                response.Email = user.Email;
                response.FirstName = user.FirstName;
                response.LastName = user.LastName;
                response.UserPassword = user.UserPassword;
                return Ok(response);
            }
            else
            {
                return NotFound();
            }            
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserModel userModel)
        {
            var user = new UserModel();
            user.Username = userModel.Username;
            user.Email = userModel.Email;
            user.FirstName = userModel.FirstName;
            user.LastName = userModel.LastName;
            user.UserPassword = userModel.UserPassword;

            _TNTContext.Users.Add(user);
            _TNTContext.SaveChanges();

            return Created($"/api/User/{user.Username}", user);


        }

        [HttpPut("{id}")]
        public IActionResult Put([FromBody] PostUserModel postuser, int id)
        {

            var user = _TNTContext.Users.Find(id);

            if (user != null)
            {
                user.Username = postuser.Username;
                user.Email = postuser.Email;
                user.FirstName = postuser.FirstName;
                user.LastName = postuser.LastName;
                user.UserPassword= postuser.UserPassword;

                _TNTContext.Users.Update(user);
                _TNTContext.SaveChanges();

                return NoContent();
            }

            return NotFound();
        }

        
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var user = _TNTContext.Users.Find(id);            

            if (user != null)
            {
                var favorite = _TNTContext.Favorites.FirstOrDefault(x => x.UserId == id);
                if (favorite != null)
                {
                    return Ok(favorite);
                }
                _TNTContext.Users.Remove(user);
                _TNTContext.SaveChanges();
                return NoContent();
            }

            return NotFound();
        }

    }
}
