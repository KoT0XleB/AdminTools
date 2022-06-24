using System;
using CommandSystem;
using Qurre.API;

namespace AdminTools.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class ItemSpawn : ICommand
    {
        public string Command => "itemspawn";
        public string[] Aliases => new string[] { "item" };
        public string Description => "Создать предмет: itemspawn (id) / (id) (x) (y) (z)";
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            Player player = Player.Get((sender as CommandSender).SenderId);
            if (arguments.Count < 1)
            {
                response = "Нужно хотя бы одну переменную ввести";
                return false;
            }
            if (arguments.Count == 1)
            {
                if (!int.TryParse(arguments.At(0), out int id))
                {
                    response = $"Неверное значение id: {arguments.At(0)}";
                    return false;
                }
                EventHandler.ItemSpawn(player, (ItemType)id);
                response = $"Вы создали предмет {(ItemType)id}";
                return true;
            }
            if (arguments.Count == 4)
            {
                if (!int.TryParse(arguments.At(0), out int id))
                {
                    response = $"Неверное значение id: {arguments.At(0)}";
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
                EventHandler.ItemSpawn(player, (ItemType)id, x, y, z);
                response = $"Вы создали предмет {(ItemType)id} на позиции {x} {y} {z}";
                return true;
            }
            response = $"Используйте: itemspawn (id) или (id) (x) (y) (z)";
            return false;
        }
    }
}
