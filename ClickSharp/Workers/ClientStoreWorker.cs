using ClickSharp.Auth;

namespace ClickSharp.Workers
{
    public class ClientStoreWorker : BackgroundService
    {
        private readonly ClientStore _clientStore;

        public ClientStoreWorker(ClientStore clientStore)
        {
            _clientStore = clientStore;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                foreach(var client in _clientStore.Store)
                {
                    var time = client.Value.LastAttempt;
                    if (time.AddMinutes(15) < DateTime.Now)
                    {
                        _clientStore.Store[client.Key]--;
                        if(_clientStore.Store[client.Key].Attempts < 1)
                            _clientStore.Store.Remove(client.Key);
                    }
                }
                await Task.Delay(new TimeSpan(0,1,0), stoppingToken);
            }
        }
    }
}