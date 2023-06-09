using API.Services;
using Microsoft.AspNetCore.Mvc;
using TrainsClasses;

namespace API.Controllers
{
    [Route("/city")]
    public class CityController: Controller
    {
        private readonly CityService _cityService;

        public CityController(CityService citySerivce)
        {
            _cityService = citySerivce;
        }

        [HttpGet]
        public List<City> Cities()
        {
            return _cityService.Objects;
        }

        [HttpGet("{id:int}")]
        public IActionResult Details(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            var city = _cityService.Get(id);

            if (city is null)
            {
                return NotFound();
            }

            return Ok(city);
        }

        [HttpPost("")]
        public ActionResult Create([FromBody] City city, string token)
        {
            if (!_cityService.Validate(city))
            {
                return BadRequest("Данные города некорректны");
            }
            city = _cityService.Add(city);
            return Ok(city);
        }

        [HttpPut("")]
        public ActionResult Edit([FromBody] City city, string token)
        {
            //if (!_userService.CheckToken(token))
            //{
            //    return Unauthorized();
            //}

            if (city.Id < 0)
            {
                return BadRequest();
            }

            var found = _cityService.Get(city.Id);

            if (found is null)
            {
                return NotFound();
            }

            _cityService.Update(city);

            return Ok(city);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            _cityService.Delete(id);
            return Ok();
        }
    }
}
