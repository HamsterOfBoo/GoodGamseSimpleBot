using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GoodGamseSimpleBot.Models.Auth;
using GoodGamseSimpleBot.Models.Connect;

namespace GoodGamseSimpleBot.Controllers
{
    public interface IClient
    {
        
        IConnector Connector { get; set; }
        ICommunicator Communicator { get; set; }
        ILisenter Listener { get; set; }

        Task StartClient();

    }
}
