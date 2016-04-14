using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace Standard
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class Program
    {
        public static int Main<TStartup>(string[] args) where TStartup : class
        {
            var host = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseDefaultHostingConfiguration(args)
                .UseServer("Microsoft.AspNetCore.Server.Kestrel")
                .UseStartup<TStartup>()
                .ConfigureServices(ConfigureServices)
                .Build();

            host.Run();

            return 0;
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IStartupFilter, StartupFilter>();
        }
    }
}
