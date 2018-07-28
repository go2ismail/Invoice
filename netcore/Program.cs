using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using netcore.Data;
using netcore.Models;
using netcore.Services;

namespace netcore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                    var netcoreService = services.GetRequiredService<INetcoreService>();
                    DbInitializer.Initialize(context, userManager, roleManager, netcoreService).Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while seeding the database.");
                }
            }

            host.Run();
        }

        //log level severity: Trace = 0, Debug = 1, Information = 2, Warning = 3, Error = 4, Critical = 5

        //AzureAppService log activated by default if application hosted on Azure
        //https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-2.1&tabs=aspnetcore2x#appservice

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                 .ConfigureLogging((hostingContext, logging) =>
                 {
                     logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                     logging.AddConsole();
                     logging.AddDebug();
                 })
                .UseStartup<Startup>()
                .Build();

        
    }
}
