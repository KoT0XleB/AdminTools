using System;
using CommandSystem;
using Qurre.API;

namespace AdminTools.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Teleport : ICommand
    {
        public string Command => "tp";
        public string[] Aliases => new string[] { "teleport" };
        public string Description => "Телепортироваться на позицию: tp (id / all) (x) (y) (z)";
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count != 4)
            {
                response = "Используйте: tp (id / all) (x) (y) (z)";
                return false;
            }
            if (!float.TryParse(arguments.At(1), out float x))
            {
                response = $"Неверное значение x: {arguments.At(1)}";
                return false;
            }

            if (!float.TryParse(arguments.At(2), out float y))
            {
                response = $"Неверное значение y: {arguments.At(2)}";
                return false;
            }

            if (!float.TryParse(arguments.At(3), out float z))
            {
                response = $"Неверное значение z: {arguments.At(3)}";
                return false;
            }

            switch (arguments.At(0))
            {
                case "all":
                {
                    foreach (Player player in Player.List)
                    {
                        if (player.Role != RoleType.None || player.Role != RoleType.Spectator)
                            EventHandler.TpPlayerPosition(player, x, y, z);
                    }

                    response = $"Все люди были телепортированы на {x} {y} {z}";
                    return true;
                }
                default:
                {
                    Player pl = Player.Get(arguments.At(0));
                    if (pl == null)
                    {
                        response = $"Игрок не найден: {arguments.At(0)}";
                        return false;
                    }

                    EventHandler.TpPlayerPosition(pl, x, y, z);

                    response = $"Игрок {pl.Nickname} был телепортирован на {x} {y} {z}";
                    return true;
                }
            }
        }
    }
}
