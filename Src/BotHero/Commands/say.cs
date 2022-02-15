using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotHero.Commands
{
    public class say : ModuleBase<SocketCommandContext>
    {
		[Command("say")]
		[Summary("Primeiro comando.")]
		[Alias("falar", "helloworld")]
		public async Task UserInfoAsync([Summary("The (optional) user to get info from")] SocketUser user = null)
		{
			await ReplyAsync($"Ola mundo!");
		}
	}
}
