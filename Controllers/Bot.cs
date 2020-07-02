using System;
using System.Collections.Generic;
using System.Text;
using System.Net.WebSockets;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;
using GoodGamseSimpleBot.Models.Auth;
using GoodGamseSimpleBot.Models.Message;
using GoodGamseSimpleBot.Models.Connect;
using GoodGamseSimpleBot.Models.Disconnect;

namespace GoodGamseSimpleBot.Controllers
{
    public class Bot
    {
        private delegate Task AuthDelegate(ClientWebSocket client);
        private delegate Task ConnectDelega(ClientWebSocket client);
        private delegate Task BeginCommunicateDelega(ClientWebSocket client);

        private AuthDataJson _authData;
        private List<MessageJson> _messageData = new List<MessageJson>();
        //private ConnectToChannelJson _connectToChannelData = new ConnectToChannelJson("7450");
        private DisconnectFromChannelJson _disconnectData = new DisconnectFromChannelJson("7450");

        private UTF8Encoding encoder = new UTF8Encoding();
        
        
        private void GetDataFromReader()
        {
            DataReader dataReader = new DataReader();
            dataReader.GetData();

            _authData = JsonConvert.DeserializeObject<AuthDataJson>(dataReader.AuthData);

            foreach (var text in dataReader.MessageData)
            {
                _messageData.Add(new MessageJson("7450", text));
            }
        }

        public async Task StartBot()
        {
            GetDataFromReader();

            Uri serverUrl = new Uri("ws://chat.goodgame.ru:8081/chat/websocket");
            
            using (var webSocketClient = new ClientWebSocket())
            {
                try
                {
                    await webSocketClient.ConnectAsync(serverUrl, CancellationToken.None);
                    await ReciveMsgs(webSocketClient); //,Authenticate(webSocketClient), ConnectToChat(webSocketClient),BeginCommunication(webSocketClient)); 
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Exception: {0}", ex);
                };                
            }
        }
        private async Task ReciveMsgs(ClientWebSocket WebSocketClient)
        {
            byte[] buffer = new byte[4096];
            var response = await WebSocketClient.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            while (WebSocketClient.State == WebSocketState.Open)
            {

                if (response.MessageType != WebSocketMessageType.Close)
                {
                    string jsonResponse = Encoding.UTF8.GetString(buffer).TrimEnd('\0');
                    Console.WriteLine("Response:  " + jsonResponse);

                }
                else
                {
                    Console.WriteLine("Web Socket Closed");
                    await WebSocketClient.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                }
            }
        }

        private async Task Authenticate(ClientWebSocket WebSocketClient)
        {
            try
            {
                var AuthJson = JsonConvert.SerializeObject(_authData);
                Console.WriteLine(AuthJson);
                byte[] buffer = encoder.GetBytes(AuthJson);
                Console.WriteLine("Trying to login...");                
                await WebSocketClient.SendAsync(new ArraySegment<Byte>(buffer), WebSocketMessageType.Binary,
                    true, CancellationToken.None);

                Console.WriteLine("Login sended.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex);
            };
            
        }

        
        private async Task ConnectToChat(ClientWebSocket WebSocketClient)
        {

            try
            {
                await Task.Delay(1000);
                var AuthJson = JsonConvert.SerializeObject(_connectToChannelData);
                byte[] buffer = encoder.GetBytes(AuthJson);

                Console.WriteLine("Trying to join chat...");
                await WebSocketClient.SendAsync(new ArraySegment<Byte>(buffer), WebSocketMessageType.Binary,
                    true, CancellationToken.None);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex);
            };
        }

        private async Task BeginCommunication (ClientWebSocket WebSocketClient)
        {

            try
            {
                int i = 0;
                byte[] buffer;
                string MessageJson;

                while (WebSocketClient.State == WebSocketState.Open)
                {
                    await Task.Delay(2000);

                    if (i < _messageData.Count)
                    {
                        MessageJson = JsonConvert.SerializeObject(_messageData[i]);
                        i++;
                    }
                    else
                    {
                        i = 0;
                        MessageJson = JsonConvert.SerializeObject(_messageData[i]);
                        i++;
                    }
                    buffer = encoder.GetBytes(MessageJson);
                    await WebSocketClient.SendAsync(new ArraySegment<Byte>(buffer), WebSocketMessageType.Binary,
                       true, CancellationToken.None);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex);
            };
        }
       

    }
}
