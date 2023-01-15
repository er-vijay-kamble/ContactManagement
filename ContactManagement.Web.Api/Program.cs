namespace ContactManagement.Web.Api
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Serilog;
    using System;

    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var configuration = new ConfigurationBuilder()
                  .AddJsonFile("appsettings.json")
                  .Build();

                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception e)
            {
                Log.Fatal(e, "ContactManagement.Web.API stopped unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog();
    }
}
