using ClickSharp.Auth;
using ClickSharp.Models.Auth;
using Microsoft.Extensions.Options;
using System.Net;

namespace ClickSharp.Extensions.Moddlewares
{
    public class ClientIPMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ClientIPMiddleware> _logger;
        private readonly ClientStore _clientStore;
        public ClientIPMiddleware(RequestDelegate next, ILogger<ClientIPMiddleware> logger, ClientStore clientStore)
        {
            _next = next;
            _logger = logger;
            _clientStore = clientStore;
        }
        public async Task Invoke(HttpContext context)
        {
            //var client = clientContext;
            var ipAddress = context.Connection.RemoteIpAddress?.ToString();

            if (string.IsNullOrEmpty(ipAddress))
                ipAddress = context.Request.Headers["HTTP_CLIENT_IP"].FirstOrDefault();
            if (string.IsNullOrEmpty(ipAddress))
                ipAddress = context.Request.Headers["X-FORWARDED-FOR"].FirstOrDefault();
            if (string.IsNullOrEmpty(ipAddress))
                ipAddress = context.Request.Headers["HTTP_X-FORWARDED-FOR"].FirstOrDefault();
            if (string.IsNullOrEmpty(ipAddress))
                ipAddress = context.Request.Headers["HTTP_FORWARDED-FOR"].FirstOrDefault();
            if (string.IsNullOrEmpty(ipAddress))
                ipAddress = context.Request.Headers["HTTP_X-CLUSTER-CLIENT-IP"].FirstOrDefault();
            if (string.IsNullOrEmpty(ipAddress))
                ipAddress = context.Request.Headers["HTTP_X-FORWARDED"].FirstOrDefault();
            if (string.IsNullOrEmpty(ipAddress))
                ipAddress = context.Request.Headers["HTTP_FORWARDED"].FirstOrDefault();
            if (string.IsNullOrEmpty(ipAddress))
                ipAddress = context.Request.Headers["REMOTE_ADDR"].FirstOrDefault();

            if (!string.IsNullOrEmpty(ipAddress))
                if (!_clientStore.Store.ContainsKey(ipAddress))
                {
                    var clientModel = new ClientModel();
                    _clientStore.Store.Add(ipAddress, clientModel);
                    //client.Init(ipAddress);
                }


            //_logger.LogWarning(
            //"Request from Remote IP address: {RemoteIp} is forbidden.", ipAddress);
            //context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            //return;


            await _next.Invoke(context);
        }
    }
}
