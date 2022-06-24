using System;
using CommandSystem;
using Qurre.API;

namespace AdminTools.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Workbench : ICommand
    {
        public string Command => "workbench";
        public string[] Aliases => new string[] { };
        public string Description => "Создать стол: workbench [id / delete]";
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count != 1)
            {
                response = "Используйте: workbench [id / delete]";
                return false;
            }
            if (arguments.Count == 1)
            {
                if (arguments.At(0) == "delete")
                {
                    EventHandler.WorkbenchDestroy();
                    response = "<color=green>Все столы уничтожены!</color>";
                    return true;
                }
            }
            Player pl = Player.Get(arguments.At(0));
            if (pl == null)
            {
                response = $"Игрок не найден: {arguments.At(0)}";
                return false;
            }
            string userid = (sender as CommandSender).SenderId;
            Player admin = Player.Get(userid);

            EventHandler.WorkbenchCreate(pl);

            response = $"Вы создали стол.";
            return true;
        }
    }
}
