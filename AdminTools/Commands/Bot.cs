using System;
using CommandSystem;
using Qurre.API;

namespace AdminTools.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    public class Bot : ParentCommand
    {
        public Bot() => LoadGeneratedCommands();
        public override string Command => "bot";
        public override string[] Aliases => new string[] { };
        public override string Description => "Создать бота копией игрока: bot (id)";
        public override void LoadGeneratedCommands() { }
        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
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
