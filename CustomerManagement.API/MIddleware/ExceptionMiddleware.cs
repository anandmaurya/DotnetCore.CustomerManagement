using Newtonsoft.Json;

namespace Customer.Management.API.MIddleware
{
    public class ExceptionMiddleware
    {

        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"An exception occurred: {ex}");

                // Set the response status code
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                // Serialize the exception message to JSON
                string responseJson = JsonConvert.SerializeObject(new { error = ex.Message });

                // Write the response
                await context.Response.WriteAsync(responseJson);
            }
        }
    }
}
