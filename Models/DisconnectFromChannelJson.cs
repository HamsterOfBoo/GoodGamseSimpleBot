using System;
using System.Collections.Generic;
using System.Text;

namespace GoodGamseSimpleBot.Models.Disconnect
{
    public class DisconnectFromChannelJson
    {
        public string type { get; set; }
        public Data data { get; set; }

        public DisconnectFromChannelJson(string ChannelId)
        {
            type = "unjoin";
            data = new Data
            {
                channel_id = ChannelId
            };
        }
        public class Data
        {
            public string channel_id { get; set; }
        }
    }
}
