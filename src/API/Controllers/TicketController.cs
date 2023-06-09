using API.Services;
using Microsoft.AspNetCore.Mvc;
using TrainsClasses;

namespace API.Controllers
{
    public class TicketController: Controller
    {
        private readonly TicketService _ticketService;

        public TicketController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }

        // GET: UserController
        [HttpGet]
        public List<Ticket> Tickets()
        {
            return _ticketService.Objects;
        }

        [HttpGet("{id:int}")]
        public IActionResult Details(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }

            var user = _ticketService.Get(id);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost("")]
        public ActionResult Create([FromBody] Ticket ticket)
        {
            if (!_ticketService.Validate(ticket))
            {
                return BadRequest("Данные пользователя некорректны");
            }
            ticket = _ticketService.Add(ticket);
            return Ok(ticket);
        }

        //// PUT: user/5
        //[HttpPut("")]
        //public ActionResult Edit([FromBody] User ticket, string token)
        //{
        //    //if (!_userService.CheckToken(token))
        //    //{
        //    //    return Unauthorized();
        //    //}

        //    if (ticket.Id < 0)
        //    {
        //        return BadRequest();
        //    }

        //    var found = _ticketService.Get(ticket.Id);

        //    if (found is null)
        //    {
        //        return NotFound();
        //    }

        //    _ticketService.Update(ticket);

        //    return Ok(ticket);
        //}

        // DELETE: user/5
        //[HttpDelete("{id:int}")]
        //public ActionResult Delete(int id)
        //{
        //    _ticketService.Delete(id);
        //    return Ok();
        //}
    }
}
