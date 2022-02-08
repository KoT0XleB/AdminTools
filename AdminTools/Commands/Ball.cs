using System;
using CommandSystem;
using Qurre.API;

namespace AdminTools.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Ball : ICommand
    {
        public string Command => "ball";
        public string[] Aliases => new string[] { };
        public string Description => "Кинуть в человека мячик: ball (id)";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
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
