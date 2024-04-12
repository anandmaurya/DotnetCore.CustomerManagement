namespace Customer.Management.API.MIddleware
{
    // Extension method used to add the middleware to the HTTP request pipeline
    public static class ExceptionMiddlewareExtensions
    {
        public static void UseExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
