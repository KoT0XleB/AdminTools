using System;
using CommandSystem;
using Qurre.API;
using UnityEngine;

namespace AdminTools.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Primitive : ICommand
    {
        public string Command => "primitive";
        public string[] Aliases => new string[] { };
        public string Description => "Создать примитив: primitive [number или delete]";
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count != 1)
            {
                response = "Используйте: primitive [number или delete]";
                return false;
            }
            if (arguments.At(0) == "delete")
            {
                EventHandler.PrimitiveDestroy();
                response = "<color=green>Все примитивы уничтожены!</color>";
                return true;
            }
            if (int.Parse(arguments.At(0)) < 0 && int.Parse(arguments.At(0)) > 5)
            {
                response = "<color=red>Введите номер от 0 до 5</color>";
                return false;
            }
            string userid = (sender as CommandSender).SenderId;
            Player admin = Player.Get(userid);

            EventHandler.PrimitiveCreate(admin, (PrimitiveType)int.Parse(arguments.At(0)));

            response = $"Вы создали примитив.";
            return true;
            
        }
    }
}
