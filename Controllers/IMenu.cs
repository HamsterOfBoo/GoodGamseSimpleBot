using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using GoodGamseSimpleBot.Models;
using GoodGamseSimpleBot.Models.Auth;

namespace GoodGamseSimpleBot.Controllers
{
    public interface IMenu
    {
        Dictionary<string, string> CommandsToAnsweer { get; set; }
        IAuthData AuthData { get; set; }
        IClient Client { get; set; }


        void GetAuthData();
        //Нужно будет заиметь экземпляр бота
        IClient StartBot(string authData);
        void StopBot();
    }

}
