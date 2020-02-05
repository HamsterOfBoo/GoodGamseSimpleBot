using System;
using System.Threading.Tasks;
using GoodGamseSimpleBot.Controllers;

namespace GoodGamseSimpleBot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Bot bot = new Bot();
            await bot.StartBot();
            Console.ReadLine();

        }
    }
}
