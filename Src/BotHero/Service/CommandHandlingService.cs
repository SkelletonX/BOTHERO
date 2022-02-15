using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace BotHero.Commands
{
    internal class CommandHandlingService
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private IServiceProvider _provider;

        public CommandHandlingService(IServiceProvider provider)
        {
            _client = provider.GetRequiredService<DiscordSocketClient>();
            _commands = provider.GetRequiredService<CommandService>();
            _provider = provider;
            

            _commands.CommandExecuted += CommandExecutedAsync;
            _client.MessageReceived += HandleCommandAsync;
        }

        public async Task InitializeAsync()
        {
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);
        }

        private async Task HandleCommandAsync(SocketMessage rawMessage)
        {
            if (!(rawMessage is SocketUserMessage message)) return;
            if (message.Source != MessageSource.User) return;

            int argPos = 0;
            if (!message.HasStringPrefix("/", ref argPos, StringComparison.OrdinalIgnoreCase)) return;

            var context = new SocketCommandContext(_client, message);
            var result = await _commands.ExecuteAsync(context, argPos, _provider);

            if (result.Error.HasValue &&
                result.Error.Value != CommandError.UnknownCommand)
                await context.Channel.SendMessageAsync(result.ToString());
        }

        public async Task CommandExecutedAsync(Optional<CommandInfo> command, ICommandContext context, IResult result)
        {
            if (!command.IsSpecified)
            {
                await context.Channel.SendMessageAsync($"Ops, Commando inexistente.");
                return;
            }
            if (result.IsSuccess) return;
            await context.Channel.SendMessageAsync($"error: {result}");
        }
    }
}
