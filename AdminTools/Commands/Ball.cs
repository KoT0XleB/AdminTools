using System;
using CommandSystem;
using Qurre.API;

namespace AdminTools.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    public class Ball : ParentCommand
    {
        public Ball() => LoadGeneratedCommands();
        public override string Command => "ball";
        public override string[] Aliases => new string[] { };
        public override string Description => "Кинуть в человека мячик: ball (id)";
        public override void LoadGeneratedCommands() { }
        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count != 1)
            {
                response = "Используйте: ball (id)";
                return false;
            }

            Player pl = Player.Get(arguments.At(0));
            if (pl == null)
            {
                response = $"Игрок не найден: {arguments.At(0)}";
                return false;
            }

            EventHandler.ThrowBallOnPlayer(pl);

            response = $"Вы кинули в игрока {pl.Nickname} мячик";
            return true;
        }
    }
}
