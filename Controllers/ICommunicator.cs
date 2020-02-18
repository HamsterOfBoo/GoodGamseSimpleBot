using System;
using System.Collections.Generic;
using System.Text;
using GoodGamseSimpleBot.Models;

namespace GoodGamseSimpleBot.Controllers
{
    public interface ICommunicator
    {
        
        ISettings Settings { get; set; }
        Dictionary<string, string> commandsToAnsweer{ get; set; }
        IList<string> periodicMessage{ get; set; }
        IClient client{ get; set; }

        void StartPeriodicMessage();
        
        int InitCommunicate(IClient client);

        void AnsweerToCommand(string command);
    }
}
