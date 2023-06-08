using API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
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
            return _userService.Objects;
        }

        // GET: user/5
        [HttpGet("{id:int}")]
        public IActionResult Details(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            var user = _userService.Get(id);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // POST: user
        [HttpPost("")]
        public ActionResult Create([FromBody] User user)
        {
            if (!_userService.Validate(user))
            {
                return BadRequest("Данные пользователя некорректны");
            }
            user = _userService.Add(user);
            return Ok(user);
        }

        // PUT: user/5
        [HttpPut("")]
        public ActionResult Edit([FromBody] User user, string token)
        {
            //if (!_userService.CheckToken(token))
            //{
            //    return Unauthorized();
            //}

            if (user.Id < 0)
            {
                return BadRequest();
            }

            var foundUser = _userService.Get(user.Id);

            if (foundUser is null)
            {
                return NotFound();
            }

            _userService.Update(user);

            return Ok(user);
        }

        // DELETE: user/5
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            _userService.Delete(id);
            return Ok();
        }

        [HttpGet("auth")]
        public ActionResult Auth(string login, string password)
        {
            var user = _userService.GetByLoginAndPassword(login, password);
            if (user is null)
            {
                return NotFound("Пользователь не найден");
            }
            return Ok(user);
        }

        [HttpGet("isworker")]
        public bool CheckIsWorker(int id)
        {
            return _userService.CheckWorkerById(id);
        }

        [HttpGet("isadmin")]
        public bool CheckIsAdmin(int id)
        {
            return _userService.CheckAdminById(id);
        }
    }
}
