using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Serilog;

namespace EFStoredProcedureDemo
{
    public class CityRepository
    {
        public IEnumerable<City> GetCities()
        {
            var results = new List<City>();

            using (var context = new WorldWideImporters())
            {
                context.Database.Log = message => Log.Debug(message);

                results = context.Database.SqlQuery<City>("dbo.SearchCities").ToList();
            }

            return results;
        }

        public IEnumerable<City> GetCities(string cityName)
        {
            var results = new List<City>();

            using (var context = new WorldWideImporters())
            {
                context.Database.Log = message => Log.Debug(message);
                var cityNameParameter = new SqlParameter()
                {
                    ParameterName = "@cityName"
                };
                if (string.IsNullOrWhiteSpace(cityName))
                {
                    cityNameParameter.Value = DBNull.Value;
                }
                else
                {
                    cityNameParameter.Value = cityName;
                }

                results = context.Database.SqlQuery<City>("dbo.SearchCities @cityName", cityNameParameter).ToList();
            }

            return results;
        }

        public IEnumerable<City> GetCities(string cityName, string stateName)
        {
            var results = new List<City>();

            using (var context = new WorldWideImporters())
            {
                context.Database.Log = message => Log.Debug(message);
                var cityNameParameter = new SqlParameter()
                {
                    ParameterName = "@cityName"
                };
                if (string.IsNullOrWhiteSpace(cityName))
                {
                    cityNameParameter.Value = DBNull.Value;
                }
                else
                {
                    cityNameParameter.Value = cityName;
                }
                var stateNameParameter = new SqlParameter()
                {
                    ParameterName = "@stateName"
                };
                if(string.IsNullOrWhiteSpace(stateName))
                {
                    stateNameParameter.Value = DBNull.Value;
                }
                else
                {
                    stateNameParameter.Value = stateName;
                }

                results = context.Database.SqlQuery<City>("dbo.SearchCitiesWithState @cityName, @stateName", cityNameParameter, stateNameParameter).ToList();
            }

            return results;
        }
    }
}
