using System;
using CommandSystem;
using Qurre.API;

namespace AdminTools.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    public class Explode : ParentCommand
    {
        public Explode() => LoadGeneratedCommands();
        public override string Command => "explode";
        public override string[] Aliases => new string[] { };
        public override string Description => "Взорвать человека: explode (id) (причина)";
        public override void LoadGeneratedCommands() { }
        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
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
