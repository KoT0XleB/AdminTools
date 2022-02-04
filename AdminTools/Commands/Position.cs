using System;
using CommandSystem;
using Qurre.API;

namespace AdminTools.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    public class Position : ParentCommand
    {
        public Position() => LoadGeneratedCommands();
        public override string Command => "pos";
        public override string[] Aliases => new string[] { "position" };
        public override string Description => "Определить свою позицию: pos / position";
        public override void LoadGeneratedCommands() { }
        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
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
