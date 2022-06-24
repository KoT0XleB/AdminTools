using System;
using CommandSystem;
using Qurre.API;

namespace AdminTools.Commands
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class Call : ICommand
    {
        public string Command => "call";
        public string[] Aliases => new string[] { "вызов" };
        public string Description => "Calling the MTF Helicopter or Chaos Car/Вызывает вертолет MTF или машину хаоса: call (heli or car)";
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count != 1)
            {
                response = "Using/Использование: call [heli or car]";
                return false;
            }

            var argument = arguments.At(0).ToString().ToLower();

            if (argument == "car" || argument == "машина" || argument == "c")
            {
                response = "Succefully/Успешно!";
                Round.CallCICar();
                return true;
            }
            else if (argument == "heli" || argument == "вертолет" || argument == "h")
            {
                response = "Succefully/Успешно!";
                Round.CallMTFHelicopter();
                return true;
            }

            response = string.Format("Не удалось обработать/Failed to processing {0}!", argument);
            return true;
        }
    }
}
