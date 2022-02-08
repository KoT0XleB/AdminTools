using System;
using CommandSystem;
using Qurre.API;

namespace AdminTools.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Bot : ICommand
    {
        public string Command => "bot";
        public string[] Aliases => new string[] { };
        public string Description => "Создать бота копией игрока: bot (id)";
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count != 1)
            {
                response = "Используйте: bot (id)";
                return false;
            }

            Player pl = Player.Get(arguments.At(0));
            if (pl == null)
            {
                response = $"Игрок не найден: {arguments.At(0)}";
                return false;
            }

            string userid = (sender as CommandSender).SenderId;
            Player admin = Player.Get(userid);

            EventHandler.CreateBot(pl, admin);

            response = $"Вы создали копию игрока {pl.Nickname}";
            return true;
        }
    }
}
