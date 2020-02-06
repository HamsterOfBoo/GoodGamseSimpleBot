using System;
using System.Collections.Generic;
using System.Text;
using GoodGamseSimpleBot.Models;

namespace GoodGamseSimpleBot.Controllers
{
    class GoodGameMenu : IMenu
    {
        public Dictionary<string, string> CommandsToAnsweer
        {
            get { return _commandToAnsweer; }
            set { _commandToAnsweer = value; }
        }

        public IAuthData AuthData
        {
            get { return _authData;}
            set { _authData = value; }
        }
        public IClient Client
        {
            get { return _client;}
            set { _client = value; }
        }

        private Dictionary<string, string> _commandToAnsweer;
        private IAuthData _authData;
        private IClient _client;

        public void GetAuthData()
        {
            throw new NotImplementedException();
        }

        public IClient StartBot(string authData)
        {
            throw new NotImplementedException();
        }

        public void StopBot()
        {
            throw new NotImplementedException();
        }
    }
}
