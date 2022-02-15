using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using BotHero.Commands;
using BotHero.Component;
using BotHero.Logging;

namespace BotHero
{
    internal class Program
    {

        static void Main(string[] args) => new Program().MainAsync(args).GetAwaiter().GetResult();

        private DiscordSocketClient _client;

        
        public async Task MainAsync(string []args)
        {
            try
            {
                string VersionNumber = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

                using (var services = ConfigureServices())
                {
                    Logger.Info("BotHero V "+VersionNumber);
                    Logger.Info("Carregando arquivo de configurações");
                    Db.NpgsqlController.Initialise();
                    Db.NpgsqlController.CreateUser(541);
                    _client = services.GetRequiredService<DiscordSocketClient>();
                    _client.Log += Log;
                    services.GetRequiredService<CommandService>().Log += Log;
                    _client.ButtonExecuted += ButtonHandler.MyButtonHandler;

                    await _client.LoginAsync(TokenType.Bot, Config.Token());
                    await _client.StartAsync();

                    await services.GetRequiredService<CommandHandlingService>().InitializeAsync();

                    await Task.Delay(-1);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }


        private ServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton<CommandService>()
                .AddSingleton<CommandHandlingService>()
                .BuildServiceProvider();

        }

        private Task Log(LogMessage msg)
		{
			Console.WriteLine(msg.ToString());
			return Task.CompletedTask;
		}
	}
}
