using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Serilog;

namespace LoggingDemo
{
    public class OrderRepository
    {
        public IEnumerable<Order> GetOrders()
        {
            var results = new List<Order>();

            using (var context = new WideWorldImporters())
            {
                using (TextWriter logger = new StringWriter())
                {
                    context.Database.Log = logger.Write;

                    var query = context.Orders.Take(100);

                    results = query.ToList();

                    System.Diagnostics.Trace.Write(logger);
                }
            }

            return results;
        }

        public IEnumerable<Order> GetOrdersWithSerilog()
        {
            var results = new List<Order>();

            using (var context = new WideWorldImporters())
            {
                //context.Database.Log = message => Log.Debug(message);

                var query = context.Orders.Take(100);

                results = query.ToList();
            }

            return results;
        }
    }
}
