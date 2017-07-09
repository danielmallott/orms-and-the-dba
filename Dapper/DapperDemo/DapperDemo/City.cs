using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace DapperDemo
{
    [Table("Application.Cities")]
    public class City
    {
        [ExplicitKey]
        public int CityID { get; set; }

        public string CityName { get; set; }

        public int StateProvinceID { get; set; }

        public long? LatestRecordedPopulation { get; set; }

        public int LastEditedBy { get; set; }

        public DateTime ValidFrom { get; set; }

        public DateTime ValidTo { get; set; }
    }
}
