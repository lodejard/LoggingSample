using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;

namespace LoggingSample
{
    public class Startup
    {
        public static int Main(string[] args) => Standard.Program.Main<Startup>(args);

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IExplodingEvent, ExplodingEvent>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IExplodingEvent explodingEvent)
        {
            var sw = new Stopwatch();
            sw.Start();

            app.Run(async (context) =>
            {
                explodingEvent.Log(sw.ElapsedMilliseconds);

                throw new Exception("Boom");
            });
        }
    }
}
