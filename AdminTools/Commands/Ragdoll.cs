using System;
using CommandSystem;
using Qurre.API;

namespace AdminTools.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    public class Ragdoll : ParentCommand
    {
        public Ragdoll() => LoadGeneratedCommands();
        public override string Command => "ragdoll";
        public override string[] Aliases => new string[] { };
        public override string Description => "Создать труп: ragdoll (id) (count)";
        public override void LoadGeneratedCommands() { }
        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            string userid = (sender as CommandSender).SenderId;
            Player player = Player.Get(userid);

            if (arguments.Count < 1)
            {
                EventHandler.CreateRagdoll(player);
                response = "Вы заспавнили свой труп";
                return true;
            }
            if (arguments.Count != 1)
            {
                response = "Используйте: ragdoll или ragdoll (id)";
                return false;
            }
            Player pl = Player.Get(arguments.At(0));
            if (pl == null)
            {
                response = $"Игрок не найден: {arguments.At(0)}";
                return false;
            }
            EventHandler.CreateRagdoll(pl);

            response = $"Вы заспавнили туп игрока {pl.Nickname}";
            return true;
        }
    }
}
