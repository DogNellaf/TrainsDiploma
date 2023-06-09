using TrainsClasses;
using Route = TrainsClasses.Route;

namespace API.Services
{
    public class RouteService: Service<Route>
    {
        public override string GetUpdateValues(Route route)
        {
            return $"DepartureTime = '{route.DepartureTime:yyyy-dd-MM HH:mm:ss.fff}', DepartureCityId = {route.DepartureCityId}, Duration = {route.Duration}, ArrivalCityId = {route.ArrivalCityId}, Price = {route.Price}";
        }

        public override string GetCreateValues(Route route) 
        {
            return $"(DepartureTime, DepartureCityId, Duration, ArrivalCityId, Price) VALUES ('{route.DepartureTime:yyyy-dd-MM HH:mm:ss.fff}', {route.DepartureCityId}, {route.Duration}, {route.ArrivalCityId}, {route.Price})";
        }
    }
}
