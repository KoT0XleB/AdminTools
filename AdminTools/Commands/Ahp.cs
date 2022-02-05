using System;
using CommandSystem;
using Qurre.API;

namespace AdminTools.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(GameConsoleCommandHandler))]
    public class Ahp : ParentCommand
    {
        public Ahp() => LoadGeneratedCommands();
        public override string Command => "ahp";
        public override string[] Aliases => new string[] { };
        public override string Description => "Дать броню кому-либо: ahp (id) (value)";
        public override void LoadGeneratedCommands() { }
        protected override bool ExecuteParent(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count < 1)
            {
                response = "Нужно хотя бы одну переменную ввести";
                return false;
            }
            if (arguments.Count != 2)
            {
                response = "Используйте: ahp (id) (value)";
                return false;
            }
            if (!int.TryParse(arguments.At(1), out int value))
            {
                response = $"Неверное значение value: {arguments.At(1)}";
                return false;
            }
            Player pl = Player.Get(arguments.At(0));
            if (pl == null)
            {
                response = $"Игрок не найден: {arguments.At(0)}";
                return false;
            }

            EventHandler.GivePlayerAhp(pl, value);

            response = $"Вы дали игроку {pl.Nickname} {value} брони";
            return true;
        }
    }
}
