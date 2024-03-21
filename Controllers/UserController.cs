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
            user = _TNTContext.Users.FirstOrDefault(l => l.UserName.ToLower() == username.ToLower());
            if (user != null)
            {
                
                var response = new UserModel();
                response.UserName = user.UserName;
                response.Email = user.Email;
                response.FirstName = user.FirstName;
                response.LastName = user.LastName;

                // remove password security
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
            user.UserName = userModel.UserName;
            user.Email = userModel.Email;
            user.FirstName = userModel.FirstName;
            user.LastName = userModel.LastName;

            // add password security
            user.UserPassword = userModel.UserPassword;
            /*
            if (user.UserPassword != null) 
            {
                var data = Encoding.Unicode.GetBytes(user.UserPassword);
                byte[] encrypted = ProtectedData.Protect(data, null, DataProtectionScope.CurrentUser);

                //return as base64 string
                user.UserPassword = Convert.ToBase64String(encrypted);
            }

            */

            _TNTContext.Users.Add(user);
            _TNTContext.SaveChanges();

            return Created($"/api/User/{user.UserName}", user);


        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserModel user)
        {
            return Created($"/api/[controller]/{id})", user);
        }

        
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var user = _TNTContext.Users.Find(id);

            if (user != null)
            {
                _TNTContext.Users.Remove(user);
                _TNTContext.SaveChanges();
                return NoContent();
            }

            return NotFound();
        }

    }
}
