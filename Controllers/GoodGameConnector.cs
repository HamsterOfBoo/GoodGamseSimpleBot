using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net.WebSockets;
using System.Threading;
using GoodGamseSimpleBot.Models;
using GoodGamseSimpleBot.Models.Auth;
using GoodGamseSimpleBot.Models.Connect;

namespace GoodGamseSimpleBot.Controllers
{
    class GoodGameConnector : IConnector
    {
        public IAuthData AuthData { get; set; }
        public IClient Client { get; set; }
        public IChatData ChatData { get; set; }
        public ILisenter Listener { get; set; }
        public ClientWebSocket CWS { get; set; }

        public GoodGameConnector()
        {

            Listener = new GoodGameListener();
        }

        public async Task<bool> ConnectToUri(ISettings settings)
        {
            var ServerUri = new Uri(settings.ServerUri);
            using (CWS = new ClientWebSocket())
            {
                try
                {
                    await CWS.ConnectAsync(ServerUri, CancellationToken.None);
                        // Task.Run(() => Listener.Lisent(CWS));
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }

            return true;
        }

        public async Task<bool> Authenticate(ISettings settings)
        {
              

            return true;
        }

        public Task<bool> ConnectToChat(ISettings authData)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Communicate(IConnector connector, Dictionary<string, string> commandsToAnsweer)
        {
            throw new NotImplementedException();
        }
    }
}
