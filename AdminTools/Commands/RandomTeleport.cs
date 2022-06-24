using System;
using CommandSystem;
using Qurre.API;

namespace AdminTools.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class RandomTeleport : ICommand
    {
        public string Command => "randomtp";
        public string[] Aliases => new string[] { };
        public string Description => "Тп на случайную позицию: randomtp (id / all)";
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count != 1)
            {
                response = "Используйте: randomtp (id / all)";
                return false;
            }

            switch (arguments.At(0))
            {
                case "all":
                    {
                        foreach (Player player in Player.List)
                        {
                            if (player.Role != RoleType.None || player.Role != RoleType.Spectator)
                                EventHandler.TpPlayerRandom(player);
                        }

                        response = $"Все люди были телепортированы случайно";
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

                        EventHandler.TpPlayerRandom(pl);

                        response = $"Игрок {pl.Nickname} был телепортирован случайно";
                        return true;
                    }
            }
        }
    }
}
