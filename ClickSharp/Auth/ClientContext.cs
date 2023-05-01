using Microsoft.AspNetCore.Http;
using ClickSharp.Models.Auth;

namespace ClickSharp.Auth
{
    public class ClientContext
    {
        public string? ClientIpAddr { get; private set; } = null;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClientStore _clientStore;
        public ClientContext(IHttpContextAccessor httpContextAccessor, ClientStore clientStore)
        {
            _httpContextAccessor = httpContextAccessor;
            _clientStore = clientStore;
        }
        public void Init(string? ip)
        {
            ClientIpAddr = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();
            if (string.IsNullOrEmpty(ClientIpAddr))
            {
                ClientIpAddr = _httpContextAccessor.HttpContext?.Request.Headers["X-Forwarded-For"].ToString();
                if (string.IsNullOrEmpty(ClientIpAddr))
                    ClientIpAddr = ip;
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
