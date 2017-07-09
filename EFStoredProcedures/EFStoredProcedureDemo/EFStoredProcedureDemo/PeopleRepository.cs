using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Serilog;

namespace EFStoredProcedureDemo
{
    public class PeopleRepository
    {
        public IEnumerable<Person> GetPeople()
        {
            var results = new List<Person>();

            using (var context = new WorldWideImporters())
            {
                context.Database.Log = message => Log.Debug(message);

                results = context.Database.SqlQuery<Person>("dbo.GetPeople").ToList();
            }

            return results;
        }
    }
}
