using System;
using CommandSystem;
using Qurre;

namespace AdminTools.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    public class Size : ParentCommand
    {
        public Size() => LoadGeneratedCommands();
        public override string Command => "size";
        public override string[] Aliases => new string[] { };
        public override string Description => "Изменить размер человека: size (id / all) (x) (y) (z)";
        public override void LoadGeneratedCommands() { }
        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count < 1)
            {
                response = "Нужно хотя бы одну переменную ввести";
                return false;
            }
            if (arguments.Count != 4)
            {
                response = "Используйте: size (id / all) (x) (y) (z)";
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
                        foreach(Player player in Player.List)
                        {
                            if (player.Role != RoleType.None || player.Role != RoleType.Spectator)
                                EventHandler.SetPlayerSize(player, x, y, z);
                        }

                        response = $"Размер всех людей был изменён по {x} {y} {z}";
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

                        EventHandler.SetPlayerSize(pl, x, y, z);

                        response = $"Размер игрока {pl.Nickname} был изменён по {x} {y} {z}";
                        return true;
                    }
            }
        }
    }
}
