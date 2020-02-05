using System;
using System.Collections.Generic;
using System.Text;

namespace GoodGamseSimpleBot.Controllers
{
    public interface ILisenter
    {
        IClient Client();
        ICommunicator Communicator();

        void Lisent(IClient client);

        IConnector InitCommunicate(IClient client);


    }
}
