using ClickSharp.Extensions.Moddlewares;
namespace ClickSharp.Extensions
{
    public static class ClientIPMiddlewareExtension
    {
        public static IApplicationBuilder UseClientIPStore(this
        IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ClientIPMiddleware>();
        }
    }
}
