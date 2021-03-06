using System;
using CommandSystem;
using Qurre.API;

namespace AdminTools.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Bot : ICommand
    {
        public string Command => "bot";
        public string[] Aliases => new string[] { };
        public string Description => "Создать бота копией игрока: bot [id или delete]";
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count != 1)
            {
                response = "Используйте: bot (id)";
                return false;
            }
            if (arguments.At(0) == "delete")
            {
                EventHandler.BotDestroy();
                response = "<color=green>Все боты уничтожены!</color>";
                return true;
            }
            Player pl = Player.Get(arguments.At(0));
            if (pl == null)
            {
                response = $"Игрок не найден: {arguments.At(0)}";
                return false;
            }

            string userid = (sender as CommandSender).SenderId;
            Player admin = Player.Get(userid);

            EventHandler.BotCreate(pl, admin);

            response = $"Вы создали копию игрока {pl.Nickname}";
            return true;
        }
    }
}
