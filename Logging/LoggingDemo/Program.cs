using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace LoggingDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var log = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();
            Log.Logger = log;

            var orderRepository = new OrderRepository();

            var orders = orderRepository.GetOrders();

            var serilogOrders = orderRepository.GetOrdersWithSerilog();

            Log.CloseAndFlush();

            Console.Read();
        }
    }
}
