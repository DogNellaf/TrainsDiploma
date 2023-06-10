using RestaurantsClasses;
using TrainsClasses;

namespace API.Services
{
    public class TicketService: Service<Ticket>
    {
        private readonly UserService _userService;

        public TicketService(UserService userService)
        {
            _userService = userService;
        }

        public bool AddTicketToUser(int userId, int ticketId)
        {
            var user = _userService.Get(userId);
            if (user == null)
            {
                return false;
            }
            var ticket = Get(ticketId);
            if (ticket == null)
            {
                return false;
            }
            Database.AddTicketToUser(userId, ticketId);
            return true;
        }

        public List<Ticket> GetUserTickets(int id)
        {
            return Database.GetUserTickets(id);
        }

        public void ReturnTicket(int id)
        {
            Database.ReturnTicket(id);
        }

        public override string GetUpdateValues(Ticket ticket)
        {
            return $"BuyTime = '{ticket.BuyTime:yyyy-dd-MM HH:mm:ss.fff}', " +
                   $"Price = {ticket.Price}, " +
                   $"RouteId = {ticket.RouteId}, " +
                   $"StatusId = {ticket.StatusId}, " +
                   $"UserId = {ticket.UserId}";
        }

        public override string GetCreateValues(Ticket ticket)
        {
            return $"(BuyTime, Price, RouteId, StatusId, UserId) " +
                   $"VALUES (" +
                   $"'{ticket.BuyTime:yyyy-dd-MM HH:mm:ss.fff}', " +
                   $"{ticket.Price}, " +
                   $"{ticket.RouteId}, " +
                   $"{ticket.StatusId}, " +
                   $"{ticket.UserId})";
        }
    }
}
