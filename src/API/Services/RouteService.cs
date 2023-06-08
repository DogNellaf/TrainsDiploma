using TrainsClasses;
using Route = TrainsClasses.Route;

namespace API.Services
{
    public class RouteService: Service<Route>
    {
        public override string GetUpdateValues(Route element)
        {
            return $"DepartureTime = '{element.DepartureTime:yyyy-MM-dd HH:mm:ss.fff}', DepartureCity = '{element.DepartureCity}', DurationInMinutes = {element.DurationInMinutes}, ArrivalCity = '{element.ArrivalCity}'";
        }

        public override string GetCreateValues(Route element)
        {
            return $"(DepartureTime, DepartureCity, DurationInMinutes, ArrivalCity) VALUES ('{element.DepartureTime:yyyy-MM-dd HH:mm:ss.fff}', '{element.DepartureCity}', {element.DurationInMinutes}, '{element.ArrivalCity}')";
        }
    }
}
