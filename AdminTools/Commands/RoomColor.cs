using System;
using CommandSystem;
using Qurre.API;
using UnityEngine;

namespace AdminTools.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class RoomColor : ICommand
    {
        public string Command => "roomcolor";
        public string[] Aliases => new string[] { };
        public string Description => "Задать цвет команты: roomcolor (color)";
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {

            if (arguments.Count != 1)
            {
                response = "Используйте: roomcolor (color)";
                return false;
            }
            string userid = (sender as CommandSender).SenderId;
            Player player = Player.Get(userid);
            switch (arguments.At(0))
            {
                case "white":
                {
                    EventHandler.ChangeRoomColor(player, Color.white);
                    response = $"Цвет комнаты стал белым";
                    return true;
                }
                case "yellow":
                {
                    EventHandler.ChangeRoomColor(player, Color.yellow);
                    response = $"Цвет комнаты стал желтым";
                    return true;
                }
                case "red":
                {
                    EventHandler.ChangeRoomColor(player, Color.red);
                    response = $"Цвет комнаты стал красным";
                    return true;
                }
                case "green":
                {
                    EventHandler.ChangeRoomColor(player, Color.green);
                    response = $"Цвет комнаты стал зеленым";
                    return true;
                }
                case "blue":
                {
                    EventHandler.ChangeRoomColor(player, Color.blue);
                    response = $"Цвет комнаты стал синим";
                    return true;
                }
                case "black":
                {
                    EventHandler.ChangeRoomColor(player, Color.black);
                    response = $"Цвет комнаты стал черным";
                    return true;
                }
                default:
                {
                    response = $"Укажите цвет: white / yellow / red / green / blue / black";
                    return false;
                }
            }
        }
    }
}
