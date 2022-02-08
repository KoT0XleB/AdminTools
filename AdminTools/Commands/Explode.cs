using System;
using CommandSystem;
using Qurre.API;

namespace AdminTools.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Explode : ICommand
    {
        public string Command => "explode";
        public string[] Aliases => new string[] { };
        public string Description => "Взорвать человека: explode (id) [причина]";
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count < 1)
            {
                response = "Введите хотя бы (id) игрока";
                return false;
            }
            if (arguments.Count != 2)
            {
                response = "Взорвать человека: explode (id) [причина]";
                return false;
            } // Немного некорректно например: Ты был убит за абуз // дофига символов
            Player pl = Player.Get(arguments.At(0));
            if (pl == null)
            {
                response = $"Игрок не найден: {arguments.At(0)}";
                return false;
            }
            string reason = string.Empty;
            if (!string.IsNullOrEmpty(arguments.At(1)))
            {
                reason = arguments.At(1);
            }

            string userid = (sender as CommandSender).SenderId;
            Player player = Player.Get(userid);

            EventHandler.ExplodePlayer(pl, $"Админ [{player.Nickname}] взорвал вас:\n{reason}");

            response = $"Игрок {pl.Nickname} был взорван";
            return true;
        }
    }
}
