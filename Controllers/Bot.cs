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
        private AuthDataJson _authData ;
        private List<MessageJson> _messageData;
        private ConnectToChannelJson _connectToChannelData = new ConnectToChannelJson("7450");
        private DisconnectFromChannelJson _disconnectData = new DisconnectFromChannelJson("7450");

        private UTF8Encoding encoder = new UTF8Encoding();
        private Random rnd = new Random();

        private DataReader dataReader = new DataReader();        

        public async Task StartBot()
        {
            Uri serverUrl = new Uri("ws://chat.goodgame.ru:8081/chat/websocket");

            dataReader.GetData();

            _authData = dataReader.authData;
            
            foreach(var text in dataReader.messageData)
            {
                _messageData.Add(new MessageJson("7450", text));
            }

            using (var webSocketClient = new ClientWebSocket())
            {
                
                try
                {
                    await webSocketClient.ConnectAsync(serverUrl, CancellationToken.None);
                    await Task.WhenAll(ReciveMsgs(webSocketClient),Authenticate(webSocketClient), ConnectToChat(webSocketClient),BeginCommunication(webSocketClient));


                }
                catch(Exception ex)
                {
                    Console.WriteLine("Exception: {0}", ex);
                };                
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

        private async Task ReciveMsgs(ClientWebSocket WebSocketClient)
        {
            byte[] buffer = new byte[4096];
            while (WebSocketClient.State == WebSocketState.Open)
            {
                var response = await WebSocketClient.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                if (response.MessageType == WebSocketMessageType.Close)
                {
                    Console.WriteLine("Web Socket Closed");
                    await WebSocketClient.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                    
                }
                else
                {
                    string jsonResponse = Encoding.UTF8.GetString(buffer).TrimEnd('\0');
                    Console.WriteLine("Response:  " + jsonResponse);
            
                }
            }            
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

                await Task.Delay(1000);

                AuthJson = JsonConvert.SerializeObject(_messageData);
                buffer = encoder.GetBytes(AuthJson);
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

                byte[] buffer;
                string MessageJson;
                while (WebSocketClient.State == WebSocketState.Open)
                {
                    await Task.Delay(2000);

                    MessageJson = JsonConvert.SerializeObject(_messageData);
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
