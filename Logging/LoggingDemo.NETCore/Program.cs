using System;
using System.Linq;
using LoggingDemo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace LoggingDemo.NETCore
{
    public static class Program
    {
        private static IServiceProvider ServiceProvider { get; set; }

        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console(theme: SystemConsoleTheme.Literate)
                .CreateLogger();

            ServiceProvider = new ServiceCollection()
                .AddLogging()
                .AddDbContext<WideWorldImportersContext>(optionsAction => optionsAction.UseSqlServer(@"Server=localhost;Database=WideWorldImporters;Trusted_Connection=true"))
                .BuildServiceProvider();

            ServiceProvider.GetService<ILoggerFactory>()
                .AddSerilog();

            var context = ServiceProvider.GetService<WideWorldImportersContext>();
            var query = context.Orders.Take(100);
            var results = query.ToList();

            Console.Read();
        }
    }
}
