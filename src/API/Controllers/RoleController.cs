using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("/role")]
    public class RoleController : Controller
    {
        private readonly RoleService _roleService;

        public RoleController(RoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("{id:int}")]
        public IActionResult Details(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            var role = _roleService.Get(id);

            if (role is null)
            {
                return NotFound();
            }

            return Ok(role);
        }
    }
}
