using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using GoodGamseSimpleBot.Models;
using GoodGamseSimpleBot.Models.Auth;
using GoodGamseSimpleBot.Models.Connect;

namespace GoodGamseSimpleBot.Controllers
{
    public  interface IConnector
    {
        IAuthData AuthData { get; set; }
        IClient Client { get; set; }
        IChatData ChatData { get; set; }
        ILisenter Listener { get; set; }
        ClientWebSocket CWS { get; set; }

        Task<bool> ConnectToUri(ISettings settings);
        Task<bool> Authenticate(ISettings settings);
        Task<bool> ConnectToChat(ISettings settings);
    }
}
