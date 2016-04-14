using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Standard
{
    internal class StartupFilter : IStartupFilter
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public StartupFilter(
            IHostingEnvironment hostingEnvironment,
            ILoggerFactory loggerFactory)
        {
            _hostingEnvironment = hostingEnvironment;

            loggerFactory.AddConsole(LogLevel.Debug, true);
            loggerFactory.AddProvider(new CustomLoggerProvider());
        }

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return app =>
            {
                if (_hostingEnvironment.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage(new DeveloperExceptionPageOptions
                    {
                        //FileProvider = _hostingEnvironment.ContentRootFileProvider,
                        SourceCodeLineCount = 12,
                    });
                }
                else
                {
                    app.UseExceptionHandler(handle =>
                    {
                        handle.Run(async ctx =>
                        {
                            ctx.Response.StatusCode = 500;
                            ctx.Response.ContentType = "text/html; charset=utf-8";
                            await ctx.Response.WriteAsync(
@"<html>
    <head><title>Server Error</title></head>
    <body><h3>Server Error</h3></body>
</html>");
                        });
                    });
                }

                app.UseWebSockets();

                next(app);
            };
        }
    }
}
