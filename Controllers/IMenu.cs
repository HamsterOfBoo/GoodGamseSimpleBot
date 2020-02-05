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
        Dictionary<string,string> commandsToAnsweer();
        IAuthData authData();
        IClient client();


        void GetAuthData();
        //Нужно будет заиметь экземпляр бота
        IClient StartBot(string authData);
        void StopBot();
    }

}
