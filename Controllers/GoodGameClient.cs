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
    class GoodGameClient : IClient
    {
        public ISettings Settings
        {
            private get { return _settings; }
            set { _settings = value; }
        }

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
        private ILisenter _listener;
        private IConnector _connector;
        private ICommunicator _communicator;
        private ISettings _settings;

        public GoodGameClient(ISettings settings)
        {
            _connector = new GoodGameConnector(_settings);
            _listener = new GoodGameListener();
            //_authData = _authData;
        }

        public async Task StartClient()
        {
            string response;
            
            using (ClientWebSocket CWS = new ClientWebSocket())
            {
                Task.Run(() =>
                    {
                        while (CWS.State == WebSocketState.Open)
                        {
                           _listener.Lisent(CWS, _settings);
                        }
                    }
                );
                await _connector.ConnectToUri();
                await _connector.Authenticate();
            }
            
            return;

        }
    }
}
