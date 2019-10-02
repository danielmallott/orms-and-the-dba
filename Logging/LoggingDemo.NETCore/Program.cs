using System;
using System.Linq;
using LoggingDemo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
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
                .AddDbContext<WideWorldImportersContext>(optionsAction =>
                {
                    optionsAction.UseSqlServer(@"data source=10.211.55.2;initial catalog=WideWorldImporters;User Id=sa;Password=sJm4!marjm;MultipleActiveResultSets=True;App=EntityFramework", sqlServerOptions => {
                        sqlServerOptions.CommandTimeout(60);
                    });
                    optionsAction.ConfigureWarnings(warningsAction =>
                    {
                        warningsAction.Default(WarningBehavior.Log);
                        warningsAction.Ignore(RelationalEventId.BoolWithDefaultWarning);
                        warningsAction.Log(SqlServerEventId.DecimalTypeDefaultWarning);
                    });
                    optionsAction.EnableDetailedErrors();
                    optionsAction.UseLazyLoadingProxies();
                    optionsAction.EnableSensitiveDataLogging();
                    optionsAction.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                })
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
