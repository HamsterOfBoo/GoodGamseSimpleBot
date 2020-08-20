using System;
using System.Collections.Generic;
using System.Text;

namespace GoodGamseSimpleBot.Controllers
{
    public interface ICommunicator
    {
        Dictionary<string, string> commandsToAnsweer();
        IList<string> periodicMessage();
        IClient client();

        void StartPeriodicMessage();

        void AnsweerToCommand(string command);
    }
}
