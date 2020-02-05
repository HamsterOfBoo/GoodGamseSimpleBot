using System;
using System.Collections.Generic;
using System.Text;

namespace GoodGamseSimpleBot.Models.Connect
{
    public class ChatDataJson
    {
        public string type { get; set; }
        public Data data { get; set; }

        public ChatDataJson(string ChannelId)
        {
            type = "join";
            data = new Data
            {
                channel_id = ChannelId,
                hidden = false,
                mobile = false
            };
        }
    }

    public class Data
    {
        public string channel_id { get; set; }
        public bool hidden { get; set; }
        public bool mobile { get; set; }
    }
}
