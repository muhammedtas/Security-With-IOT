using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SecurityWithIOT.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
          var host =   BuildWebHost(args);
        //     using (var scope = host.Services.CreateScope())
        // {
        //     var services = scope.ServiceProvider;

        //     try
        //     {
        //         SeedData.Initialize(services, "").Wait();
        //     }
        //     catch (Exception ex)
        //     {
        //         var logger = services.GetRequiredService<ILogger<Program>>();
        //         logger.LogError(ex, "An error occurred seeding the DB.");
        //     }
        // }

          host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
                WebHost.CreateDefaultBuilder(args)
                // .UseKestrel()
                // .UseContentRoot(Directory.GetCurrentDirectory())
                // .UseIISIntegration()
                .UseStartup<Startup>()
                // Bu alttaraf seedleme için eklenmişti ama çalışmadı. Yine de dursun şimdilik.
                //  .ConfigureAppConfiguration((hostContext, config) =>
                // {
                //     // delete all default configuration providers
                //     config.Sources.Clear();
                //     config.AddJsonFile("appsettings.json", optional: true);
                // })
                .Build();
    }
}
