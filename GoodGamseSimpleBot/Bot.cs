using System;
using System.Collections.Generic;
using System.Text;
using System.Net.WebSockets;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;
using GoodGamseSimpleBot.Models.Auth;
using GoodGamseSimpleBot.Models.Connect;
using GoodGamseSimpleBot.Models.Disconnect;
using GoodGamseSimpleBot.Models.Message;

namespace GoodGamseSimpleBot
{
    public class Bot
    {
        private AuthDataJson _authData = new AuthDataJson( "35683", "1f00bd990c9634dc3ca05a9660d53122");
        private MessageJson _messageData = new MessageJson("7450", "hue");
        private ConnectToChannelJson _connectToChannelData = new ConnectToChannelJson("7450");
        private DisconnectFromChannelJson _disconnectData = new DisconnectFromChannelJson("1");

        private static UTF8Encoding encoder = new UTF8Encoding();


        public async Task StartBot()
        {
            Uri serverUrl = new Uri("ws://chat.goodgame.ru:8081/chat/websocket");

            using (var webSocketClient = new ClientWebSocket())
            {
                
                try
                {
                    await webSocketClient.ConnectAsync(serverUrl, CancellationToken.None);
                    await Task.WhenAll(ReciveMsgs(webSocketClient),Authenticate(webSocketClient), ConnectToChat(webSocketClient));


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
            Console.WriteLine("Ушон нахуй отдыхать");
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


       

    }
}
