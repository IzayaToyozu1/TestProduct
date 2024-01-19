namespace RepeirRequestWEB.Middleware
{
    public class RepairRequestMiddleware
    {
        private RequestDelegate _next;

        public RepairRequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if(con)
        }
    }
}
