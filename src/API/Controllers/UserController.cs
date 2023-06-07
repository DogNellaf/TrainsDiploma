using API.Services;
using Microsoft.AspNetCore.Mvc;
using TrainsClasses;

namespace API.Controllers
{
    [Route("/user")]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        // GET: UserController
        [HttpGet]
        public List<User> Users()
        {
            return _userService.Users;
        }

        // GET: user/5
        [HttpGet("{id:int}")]
        public IActionResult Details(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            var user = _userService.GetUser(id);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST: user
        [HttpPost("")]
        public ActionResult Create(User user)
        {
            if (!_userService.Validate(user))
            {
                return BadRequest("Данные пользователя некорректны");
            }
            user = _userService.Add(user);
            return Ok(user);
        }

        // PUT: user/5
        [HttpPut("{id:int}")]
        public ActionResult Edit(int id)
        {
            return Ok("Test Edit");
        }

        // DELETE: user/5
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            return Ok("Test Delete");
        }
    }
}
