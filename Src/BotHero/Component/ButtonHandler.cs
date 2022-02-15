using BotHero.Component.Buttons;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotHero.Component
{
    public class ButtonHandler
    {
		public static async Task MyButtonHandler(SocketMessageComponent component)
		{
			switch (component.Data.CustomId)
			{
				case "bt-status":
					await BT_Status.runAsync(component);
					break;
			}
		}
	}
}
