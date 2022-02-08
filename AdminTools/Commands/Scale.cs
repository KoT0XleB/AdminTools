using System;
using CommandSystem;
using Qurre.API;

namespace AdminTools.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Scale : ICommand
    {
        public string Command => "scale";
        public string[] Aliases => new string[] { };
        public string Description => "Изменить размер человека: scale (id / all) (value)";
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count < 1)
            {
                response = "Нужно хотя бы одну переменную ввести";
                return false;
            }
            if (arguments.Count != 2)
            {
                response = "Используйте: scale (id / all) (value)";
                return false;
            }
            if (!float.TryParse(arguments.At(1), out float value))
            {
                response = $"Неверное значение value: {arguments.At(1)}";
                return false;
            }

            switch (arguments.At(0))
            {
                case "all":
                {
                    foreach (Player player in Player.List)
                    {
                        if (player.Role != RoleType.None || player.Role != RoleType.Spectator)
                            EventHandler.SetPlayerScale(player, value);
                    }

                    response = $"Размер всех людей был изменён в {value} раза";
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

                    EventHandler.SetPlayerScale(pl, value);

                    response = $"Размер игрока {pl.Nickname} был изменён в {value} раза";
                    return true;
                }
            }
        }
    }
}
