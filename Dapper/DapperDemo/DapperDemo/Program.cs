using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;

namespace DapperDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var sqlConnection = new SqlConnection("Server=localhost;Database=WideWorldImporters;Trusted_Connection=true"))
            {
                var cities = sqlConnection.Query<City>("dbo.SearchCities", new { cityName = "Abbeville" },
                    commandType: System.Data.CommandType.StoredProcedure);

                Console.WriteLine($"{cities.Count()} cities retrieved.");

                var city = sqlConnection.Get<City>(1);

                Console.WriteLine($"{city.CityName}");
            }

            Console.Read();
        }
    }
}
