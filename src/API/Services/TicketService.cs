using TrainsClasses;

namespace API.Services
{
    public class TicketService: Service<Ticket>
    {
        public override string GetUpdateValues(Ticket ticket)
        {
            return $"BuyTime = '{ticket.BuyTime:yyyy-dd-MM HH:mm:ss.fff}', Price = {ticket.Price}, RouteId = {ticket.RouteId}, StatusId = {ticket.StatusId}";
        }

        public override string GetCreateValues(Ticket ticket)
        {
            return $"(BuyTime, Price, RouteId, StatusId) VALUES ('{ticket.BuyTime:yyyy-dd-MM HH:mm:ss.fff}', {ticket.Price}, {ticket.RouteId}, {ticket.StatusId})";
        }
    }
}
