using GodotAssetLibrary.Contracts;

namespace GodotAssetLibrary.Middleware
{
    public class GodotApiInterceptorMiddleware
    {
        private readonly RequestDelegate _next;

        public GodotApiInterceptorMiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task InvokeAsync(
                HttpContext context,
                IRequestLifetime requestLifetime)
        {
            bool isFrontend = true;
            if (context.Request.Path.StartsWithSegments("/api"))
            {
                context.Request.Path = context.Request.Path.ToUriComponent()[4..];
                Console.WriteLine("api call detected");
                isFrontend = false;
            }

            context.Items["isFrontend"] = isFrontend;
            requestLifetime.IsFrontend = isFrontend;

            await _next(context);
        }
    }


    public static class GodotApiInterceptorMiddlewareExtensions
    {
        public static IApplicationBuilder UseGodotApiInterceptor(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GodotApiInterceptorMiddleware>();
        }
    }
}
