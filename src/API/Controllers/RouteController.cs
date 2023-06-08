using API.Services;
using Microsoft.AspNetCore.Mvc;
using Route = TrainsClasses.Route;

namespace API.Controllers
{
    [Route("/route")]
    public class RouteController : Controller
    {
        private readonly RouteService _routeService;

        public RouteController(RouteService routeService)
        {
            _routeService = routeService;
        }

        [HttpGet]
        public List<Route> Routes()
        {
            return _routeService.Objects;
        }

        [HttpGet("{id:int}")]
        public IActionResult Details(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            var user = _routeService.Get(id);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost("")]
        public ActionResult Create([FromBody] Route route, string token)
        {
            if (!_routeService.Validate(route))
            {
                return BadRequest("Данные пользователя некорректны");
            }
            route = _routeService.Add(route);
            return Ok(route);
        }

        [HttpPut("")]
        public ActionResult Edit([FromBody] Route route, string token)
        {
            //if (!_userService.CheckToken(token))
            //{
            //    return Unauthorized();
            //}

            if (route.Id < 0)
            {
                return BadRequest();
            }

            var found = _routeService.Get(route.Id);

            if (found is null)
            {
                return NotFound();
            }

            _routeService.Update(route);

            return Ok(route);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            _routeService.Delete(id);
            return Ok();
        }
    }
}
