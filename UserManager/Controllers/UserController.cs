using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using UserManager.Models;
using UserManager.Validators;

namespace UserManager.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        public static List<User> Users = new List<User>
        {
            new User
            {
                Id = 1,
                Name = "Zehra",
                Surname = "Tunc",
                Email = "zehra@gmail.com",
                Phone = "+905363372597"
            },
            new User
            {
                Id = 2,
                Name = "Gizem",
                Surname = "Tunc",
                Email = "gizem@gmail.com",
                Phone = "+905354786891"
            }
        };

        [HttpGet("GetAll")]
        public IActionResult Get()
        {
            return Ok(Users);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult Get([FromRoute] int id)
        {
            var user = Users.Find(u => u.Id == id);

            if (user == null)
            {
                return NotFound(id);
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {
            var validator = new UserValidator();

            var result = validator.Validate(user);

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            Users.Add(user); // olusturulan user`i Users listesine ekle

            return Ok(user);
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] User user)
        {
            var validator = new UserValidator();

            var result = validator.Validate(user);

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            var currentUser = Users.Find(u => u.Id == id);

            if (currentUser == null)
            {
                return NotFound(id);
            }

            currentUser = user; // currentUser`i user olarak guncelleriz
            currentUser.Id = id; //Id degerinin degismemesi icin

            return Ok(currentUser);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch([FromRoute] int id, [FromBody] JsonPatchDocument<User> userPatch)
        {
            if (userPatch == null)
            {
                return BadRequest();
            }

            var user = Users.Find(u => u.Id == id);

            if (user == null)
            {
                return NotFound(id);
            }

            userPatch.ApplyTo(user);

            var validator = new UserValidator();
            var result = validator.Validate(user);

            if (!result.IsValid)
            {
                return BadRequest(result.Errors);
            }

            return Ok(user);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var user = Users.Find(u => u.Id == id);

            if (user == null)
            {
                return NotFound(id);
            }

            Users.Remove(user);

            return Ok(id);
        }
    }
}
