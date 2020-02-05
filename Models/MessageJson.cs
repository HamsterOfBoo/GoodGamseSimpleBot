using System;
using System.Collections.Generic;
using System.Text;

namespace GoodGamseSimpleBot.Models.Message
{
    public class MessageJson
    {
        public string type { get; set; }
        public Data data { get; set; }

        public MessageJson(string ChannelId, string Text)
        {
            type = "send_message";
            data = new Data
            {
                channel_id = ChannelId,
                text = Text,
                hideIcon = false,
                mobile = false
            };
        }
    }

    public class Data
    {
        public string channel_id { get; set; }
        public string text { get; set; }
        public bool hideIcon { get; set; }
        public bool mobile { get; set; }
    }
}
