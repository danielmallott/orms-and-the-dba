using System;
using System.Linq;
using LoggingDemo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace LoggingDemo.NETCore
{
    public static class Program
    {
        private static IServiceProvider ServiceProvider { get; set; }

        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console(theme: SystemConsoleTheme.Literate)
                .CreateLogger();

            ServiceProvider = new ServiceCollection()
                .AddLogging()
                .AddDbContext<WideWorldImportersContext>(optionsAction =>
                {
                    optionsAction.UseSqlServer(@"data source=localhost;initial catalog=WideWorldImporters;User Id=ormsuser;Password=Password123!;MultipleActiveResultSets=True;App=EntityFramework", sqlServerOptions => {
                        sqlServerOptions.CommandTimeout(60);
                    });
                    optionsAction.ConfigureWarnings(warningsAction =>
                    {
                        warningsAction.Default(WarningBehavior.Log);
                        warningsAction.Ignore(RelationalEventId.BoolWithDefaultWarning);
                        warningsAction.Ignore(SqlServerEventId.DecimalTypeDefaultWarning);
                    });
                    optionsAction.EnableDetailedErrors();
                    // WARNING: DO NOT DO THIS IN PRODUCTION
                    optionsAction.EnableSensitiveDataLogging();
                    // Warning: the following line automatically makes all queries use No Tracking, so entities cannot be later saved
                    optionsAction.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                })
                .BuildServiceProvider();

            ServiceProvider.GetService<ILoggerFactory>()
                .AddSerilog();

            using (var context = ServiceProvider.GetService<WideWorldImportersContext>())
            {
                var query = context.Orders.Take(100);
                var results = query.ToList();
            }

            Console.Read();
        }
    }
}
