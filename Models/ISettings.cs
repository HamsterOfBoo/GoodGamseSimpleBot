using System;
using System.Collections.Generic;
using System.Text;

namespace GoodGamseSimpleBot.Models
{
    public interface ISettings
    {
        string UserId { get; set; }
        string Token { get; set; }
        string ServerUri { get; set; }
    }
}
