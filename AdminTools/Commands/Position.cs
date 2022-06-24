using System;
using CommandSystem;
using Qurre.API;

namespace AdminTools.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Position : ICommand
    {
        public string Command => "pos";
        public string[] Aliases => new string[] { "position" };
        public string Description => "Определить свою позицию: pos / position";
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count > 0)
            {
                response = "Напишите только pos / position, чтобы определить свою позицию";
                return false;
            }

            string userid = (sender as CommandSender).SenderId;
            Player player = Player.Get(userid);

            string xpos = string.Format("{0:0.##}", player.Position.x);
            string ypos = string.Format("{0:0.##}", player.Position.y);
            string zpos = string.Format("{0:0.##}", player.Position.z);

            response = $"Ваша позиция: {xpos} {ypos} {zpos}";
            return true;
        }
    }
}
