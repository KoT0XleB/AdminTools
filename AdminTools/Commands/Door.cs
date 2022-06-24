using System;
using CommandSystem;
using Qurre.API;

namespace AdminTools.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Door : ICommand
    {
        public string Command => "door";
        public string[] Aliases => new string[] { };
        public string Description => "Создать дверь: door [id / delete]";
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count != 1)
            {
                response = "Используйте: door (id)";
                return false;
            }
            if (arguments.At(0) == "delete")
            {
                EventHandler.BotDestroy();
                response = "<color=green>Все двери уничтожены!</color>";
                return true;
            }
            Player pl = Player.Get(arguments.At(0));
            if (pl == null)
            {
                response = $"Игрок не найден: {arguments.At(0)}";
                return false;
            }

            EventHandler.DoorCreate(pl);

            response = $"Вы создали дверь.";
            return true;
        }
    }
}
