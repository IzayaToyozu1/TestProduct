using TestProduct.Service;

namespace TestProduct
{
    public class RoutingMiddleware
    {
        public RoutingMiddleware(RequestDelegate _)
        {

        }

        public async Task InvokeAsync(HttpContext context, ITimeService time)
        {
            string path = context.Request.Path;
            if (path == "/index")
            {
                context.Response.ContentType = "text/html; charset=utf-8";
                await context.Response.SendFileAsync("html/index.html");
            }
            else if (path == "/about")
            {
                await context.Response.WriteAsync("About Page");
            }
            else if (path == "/time")
            {
                await context.Response.WriteAsync($"Time: {time.GetTime()}");
            }
            else
            {
                context.Response.StatusCode = 404;
            }
        }
    }
}
