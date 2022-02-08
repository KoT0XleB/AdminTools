using System;
using CommandSystem;
using Qurre.API;

namespace AdminTools.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    [CommandHandler(typeof(ClientCommandHandler))]
    public class Cleanup : ICommand
    {
        public string Command => "cleanup";
        public string[] Aliases => new string[] { };
        public string Description => "Очистить комплекс: cleanup (items / ragdolls / all)";
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count != 1)
            {
                response = "Очистить комплекс: cleanup (items / ragdolls / all)";
                return false;
            }
            switch (arguments.At(0))
            {
                case "items":
                    {
                        EventHandler.CleanUpObjects("items");
                        response = $"Все предметы на карте были очищены";
                        return true;
                    }
                case "ragdolls":
                    {
                        EventHandler.CleanUpObjects("ragdolls");
                        response = $"Все трупы на карте были очищены";
                        return true;
                    }
                case "all":
                    {
                        EventHandler.CleanUpObjects("items");
                        EventHandler.CleanUpObjects("ragdolls");
                        response = $"Все предметы и трупы на карте были очищены";
                        return true;
                    }
                default:
                    {
                        response = $"Укажите items / ragdolls для очистки";
                        return false;
                    }
            }
        }
    }
}
