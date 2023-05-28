using Microsoft.AspNetCore.Http;
using ClickSharp.Models.Auth;
using System.Reflection.PortableExecutable;

namespace ClickSharp.Auth
{
    public class ClientContext
    {
        public string? ClientIpAddr { get; private set; } = null;
        public string Headers { get; private set; } = string.Empty;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClientStore _clientStore;
        public ClientContext(IHttpContextAccessor httpContextAccessor, ClientStore clientStore)
        {
            _httpContextAccessor = httpContextAccessor;
            _clientStore = clientStore;
        }
        public void Init(string? ip)
        {
            //ClientIpAddr = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
            //if (string.IsNullOrEmpty(ClientIpAddr))
            //{
            //    ClientIpAddr = _httpContextAccessor.HttpContext?.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            //    if (string.IsNullOrEmpty(ClientIpAddr))
            //        ClientIpAddr = ip;
            //}
            //ClientIpAddr = ip;

            var context = _httpContextAccessor.HttpContext;
            if (context == null)
                return;

            foreach(var header in context.Request.Headers)
            {
                Headers += $" {header.Key}";
                Console.WriteLine(header.Key);
            }

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
            {
                ClientIpAddr = ipAddress;
                if (!_clientStore.Store.ContainsKey(ipAddress))
                {
                    var clientModel = new ClientModel();
                    _clientStore.Store.Add(ipAddress, clientModel);
                    //client.Init(ipAddress);
                }
            }
        }

        public int Attempts { 
            get
            {
                if (ClientIpAddr != null && _clientStore.Store.ContainsKey(ClientIpAddr))
                    return _clientStore.Store[ClientIpAddr].Attempts;
                else
                    return 0;
            } 
            set {
                if(ClientIpAddr != null)
                {
                    if (!_clientStore.Store.ContainsKey(ClientIpAddr))
                    {
                        var client = new ClientModel();
                        _clientStore.Store.Add(ClientIpAddr, client);
                    }
                    if (_clientStore.Store.ContainsKey(ClientIpAddr))
                        _clientStore.Store[ClientIpAddr]++;
                }
            }
        }

        public bool Allow()
        {
            if (ClientIpAddr != null && _clientStore.Store.ContainsKey(ClientIpAddr))
            {
                var client = _clientStore.Store.FirstOrDefault(x => x.Key == ClientIpAddr).Value;
                if (client != null)
                {
                    var start = client.LastAttempt;
                    if (start.AddMinutes(15) < DateTime.Now || client.Attempts < 5)
                        return true;
                }
                return false;
            }
            return true;
        }
    }
}
