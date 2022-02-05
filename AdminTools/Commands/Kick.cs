using System;
using CommandSystem;
using Qurre.API;

namespace AdminTools.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    public class Kick : ParentCommand
    {
        public Kick() => LoadGeneratedCommands();
        public override string Command => "kick";
        public override string[] Aliases => new string[] { };
        public override string Description => "Кикнуть человека: kick (id) [причина]";
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
                response = "Кикнуть человека: kick (id) [причина]";
                return false;
            } // Немного некорректно например: Ты был забанен за абуз или читы // дофига символов
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

            EventHandler.KickPlayer(pl, reason, player.Nickname);

            response = $"Игрок {pl.Nickname} был кикнут";
            return true;
        }
    }
}
