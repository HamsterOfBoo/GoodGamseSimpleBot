using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GoodGamseSimpleBot.Models;
using GoodGamseSimpleBot.Models.Auth;
using Newtonsoft.Json;

namespace GoodGamseSimpleBot.Controllers
{
    class GoodGameListener:ILisenter
    {
        public IClient Client { get; set; }
        public ICommunicator Communicator { get; set; }
        public async Task<string> Lisent(ClientWebSocket CWS, ISettings settings)
        {
            
                byte[] buffer = new byte[4096];
                while (CWS.State == WebSocketState.Open)
                {
                    var response = await CWS.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                    if (response.MessageType != WebSocketMessageType.Close)
                    {
                        string jsonResponse = Encoding.UTF8.GetString(buffer).TrimEnd('\0');
                        Console.WriteLine("Response:  " + jsonResponse);

                        yield return jsonResponse;
                }
                    else
                    {
                        Console.WriteLine("Web Socket Closed");
                        await CWS.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                    }

                }
        }

        public IConnector InitCommunicate(IClient client)
        {
            throw new NotImplementedException();
        }
    }
}
