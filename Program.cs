using System;
using GoodGamseSimpleBot.Controllers;

namespace GoodGamseSimpleBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Bot bot = new Bot();
            bot.StartBot();
            Console.ReadLine();

        }
    }
}
