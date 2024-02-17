using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

using Serilog;
using Serilog.Events;

namespace RefactorThis.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
               .Enrich.FromLogContext()
               .MinimumLevel.Information()
               .WriteTo.Console(
                   LogEventLevel.Verbose,
                   "{NewLine}{Timestamp:HH:mm:ss} [{Level}] {Message}{NewLine}{Exception}")
                   .CreateLogger();

            try
            {
                CreateWebHostBuilder(args).Build().Run();
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}