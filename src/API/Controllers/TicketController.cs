using API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net.Sockets;
using System.Text.Json.Nodes;
using TrainsClasses;

namespace API.Controllers
{
    [Route("/ticket")]
    public class TicketController: Controller
    {
        private readonly TicketService _ticketService;

        public TicketController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }

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

        [HttpPost("/ticket/user")]
        public ActionResult AddTicketToUser(int userId, int ticketId, string token)
        {
            var result = _ticketService.AddTicketToUser(userId, ticketId);
            if (result)
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpGet("/ticket/user")]
        public ActionResult GetUserTickets(int userId, string token)
        {
            var result = _ticketService.GetUserTickets(userId);
            return Ok(result);
        }

        [HttpPut("/ticket/user")]
        public ActionResult ReturnTicket(int ticketId, string token)
        {
            _ticketService.ReturnTicket(ticketId);
            return Ok();
        }
    }
}
