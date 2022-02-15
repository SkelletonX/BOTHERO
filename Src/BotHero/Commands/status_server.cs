using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotHero.Commands
{
	public class status_server : ModuleBase<SocketCommandContext>
	{
		[Command("status")]
		[Summary("status do servidor.")]
		[Alias("statusserver", "status_server")]
		public async Task UserInfoAsync([Summary("The (optional) user to get info from")] SocketUser user = null)
		{


			var builder = new ComponentBuilder()
				.WithButton("check", "bt-status");

			await ReplyAsync("Here is a button!", components: builder.Build());
		}
	}
}
