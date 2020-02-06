using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GoodGamseSimpleBot.Models;
using GoodGamseSimpleBot.Models.Auth;
using GoodGamseSimpleBot.Models.Connect;

namespace GoodGamseSimpleBot.Controllers
{
    class GoodGameClient : IClient
    {
        public IConnector Connector
        {
            get { return _connector; }
            set { _connector = value; }
        }
        public ICommunicator Communicator
        {
            get { return _communicator; }
            set { _communicator = value; }
        }

        public ILisenter Listener { get; set; }

        //private AuthDataJson _authData;
        //private ChatDataJson _chatData;
        private IConnector _connector;
        private ICommunicator _communicator;

        public GoodGameClient(ISettings settings)
        {
            //_authData = _authData;
        }

        public Task StartClient()
        {
            return null;
            //_connector = new IConnector();
        }
    }
}
