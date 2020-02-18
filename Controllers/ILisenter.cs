using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using GoodGamseSimpleBot.Models;

namespace GoodGamseSimpleBot.Controllers
{
    public interface ILisenter
    {
        IClient Client { get; set; }
        ICommunicator Communicator {get;set;}


        Task<string> Lisent(ClientWebSocket CWS, ISettings settings);



    }
}
