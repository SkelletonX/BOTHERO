using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotHero.Component.Buttons
{
    internal class BT_Status
    {
        public static async Task runAsync(SocketMessageComponent component)
        {
            await component.RespondAsync($"{component.User.Mention} Servidor em manutenção!");
        }
    }
}
