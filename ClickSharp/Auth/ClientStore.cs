using ClickSharp.Models.Auth;

namespace ClickSharp.Auth
{
    public class ClientStore
    {
        public Dictionary<string,ClientModel> Store { get; private set; }
        public ClientStore()
        {
            Store = new Dictionary<string, ClientModel>();
        }
    }
}
