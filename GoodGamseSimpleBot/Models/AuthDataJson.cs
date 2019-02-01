using System;
using System.Collections.Generic;
using System.Text;

namespace GoodGamseSimpleBot.Models.Auth
{
    [Serializable]
    class AuthDataJson
    {
        public string type { get; set; }
        public Data data { get; set; }

        public AuthDataJson( string User_id, string Token)
        {
            type = "auth";
            data = new Data
            {
                user_id = User_id,
                token = Token
            };
        }
    }
    public class Data
    {
        public string user_id { get; set; }
        public string token { get; set; }
    }
}
