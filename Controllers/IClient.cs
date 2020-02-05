using System;
using System.Collections.Generic;
using System.Text;
using GoodGamseSimpleBot.Models.Auth;
using GoodGamseSimpleBot.Models.Connect;

namespace GoodGamseSimpleBot.Controllers
{
    public interface IClient
    {
        AuthDataJson authData();
        ChatDataJson chatData();
        IConnector connector();
        ICommunicator communicator();

        bool Authenticate(AuthDataJson authData);
        IConnector ConnectToChat(ChatDataJson chatData);
        ILisenter LisentChat(IConnector Connector);
        ICommunicator Communicate(IConnector connector, Dictionary<string,string> commandsToAnsweer);


    }
}
