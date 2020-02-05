using System;
using System.Collections.Generic;
using System.Text;
using GoodGamseSimpleBot.Models;
using GoodGamseSimpleBot.Models.Auth;

namespace GoodGamseSimpleBot.Controllers
{
    public interface IConnector
    {
        IAuthData authData();
        IClient client();
        IChatData chatData();

        bool TryConnectToChat(IChatData chatData);
        bool TryDisconnectToChat(IChatData chatData);
    }
}
