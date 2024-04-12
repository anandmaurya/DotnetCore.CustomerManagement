
using CustomerManagement.Infrastrucure.Helper;
using System.Text;

namespace Customer.Management.API.MIddleware
{
    public class RequestResponseMiddleware : IMiddleware
    {
        async Task IMiddleware.InvokeAsync(HttpContext context, RequestDelegate next)
        {
            //var request = context.Request;

            if (context.Request.Path.StartsWithSegments("/api/Customers") && context.Request.Method == "POST")
            {
                var bodyStream = context.Request.Body;
                var requestBody = await new StreamReader(bodyStream).ReadToEndAsync();

                //var decryptedPayload = EncryptionHelper.Decrypt(requestBody);

                // Create a new HttpRequest with the decrypted payload
                var updatedRequest = new DefaultHttpContext().Request;
                updatedRequest.Path = context.Request.Path;
                updatedRequest.Method = context.Request.Method;
                updatedRequest.Body = new MemoryStream(Encoding.UTF8.GetBytes(requestBody));
                updatedRequest.ContentLength = updatedRequest.Body.Length;
                updatedRequest.Body.Seek(0, SeekOrigin.Begin);

                context.Request.Body = updatedRequest.Body;
            }
            // Call the next middleware in the pipeline
            await next(context);

            // Logic to handle each response after it has been generated
            // For example, logging or modifying the response
            var response = context.Response;
        }

    }
}
