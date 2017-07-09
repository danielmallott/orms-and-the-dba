using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace EFStoredProcedureDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var log = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console(theme: SystemConsoleTheme.Literate)
                .CreateLogger();
            Log.Logger = log;

            var peopleRepository = new PeopleRepository();

            var people = peopleRepository.GetPeople();

            Console.WriteLine($"{people.Count()} people retrieved.");

            var cityRepository = new CityRepository();

            var allCities = cityRepository.GetCities();

            Console.WriteLine($"{allCities.Count()} cities retrieved.");

            var abbevilleCities = cityRepository.GetCities("Abbeville");

            Console.WriteLine($"{abbevilleCities.Count()} cities retrieved.");

            var abbevilleCitiesNoState = cityRepository.GetCities("Abbeville", null);

            Console.WriteLine($"{abbevilleCitiesNoState.Count()} cities retrieved.");

            var abbevilleAlabamaCities = cityRepository.GetCities("Abbeville", "Alabama");

            Console.WriteLine($"{abbevilleAlabamaCities.Count()} cities retrieved.");

            Console.Read();
        }
    }
}
