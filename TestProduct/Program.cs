using System;
using System.Text.RegularExpressions;
using TestProduct.Service;

namespace TestProduct
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder();

            builder.Services.AddTransient<ITimeService, ShortTimeService>();

            var app = builder.Build();
            

            //app.UseMiddleware<ErrorHandlingMiddleware>();
            //app.UseMiddleware<AuthenticationMiddleware>();
            //app.UseMiddleware<RoutingMiddleware>();

            app.Run(async (context) =>
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                //var timeService = app.Services.GetService<ITimeService>();
                //await context.Response.WriteAsync($"Time: {timeService.GetTime()}");
                if (context.Request.Path == "/index")
                {
                    await context.Response.SendFileAsync("html/index.html");
                }
            });

            app.Run();
        }
    }

    public class Person
    {
        public string Id { get; set; } = "";
        public string Name { get; set; } = "";
        public int Age { get; set; }
    }
    
}