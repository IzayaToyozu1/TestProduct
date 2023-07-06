namespace TestProduct
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next=next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            await _next.Invoke(context);
            if (context.Response.StatusCode == 403)
            {
                await context.Response.WriteAsync("Access Denied");
            }
            else if (context.Response.StatusCode == 404)
            {
                await context.Response.WriteAsync("Not Found");
            }
        }
    }
}
