using TrainsClasses;

namespace API.Services
{
    public class CityService: Service<City>
    {
        public override string GetUpdateValues(City city)
        {
            return $"name = '{city.Name}'";
        }

        public override string GetCreateValues(City city)
        {
            return $"(name) VALUES ('{city.Name}')";
        }
    }
}
