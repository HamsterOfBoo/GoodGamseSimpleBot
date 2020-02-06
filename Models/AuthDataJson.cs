using System;
using System.Collections.Generic;
using System.Text;

namespace GoodGamseSimpleBot.Models.Auth
{
    [Serializable]
    public class AuthDataJson
    {
        public string type
        {
            get { return "auth";}
            set { type = value; }
        }
        public Data data { get; set; }

    }
    public class Data
    {
        public string user_id { get; set; }
        public string token { get; set; }

        public Data(string userId, string token)
        {
            this.user_id = userId;
            this.token = token;
        }
    }
}
