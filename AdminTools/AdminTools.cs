using Qurre;
using Qurre.API;
using Qurre.Events;
using Qurre.API.Events;

using Version = System.Version;

namespace AdminTools
{
    public class AdminTools : Plugin
    {
        public override string Developer => "KoT0XleB#4663";
        public override string Name => "AdminTools";
        public override Version Version => new Version(1, 3, 0);
        public override void Enable() => RegisterEvents();
        public override void Disable() => UnregisterEvents();

        public void RegisterEvents()
        {
            Qurre.Events.Server.SendingRA += OnSendingRA;
            Qurre.Events.Player.InteractDoor += InteractingDoor;
        }
        public void UnregisterEvents()
        {
            Qurre.Events.Server.SendingRA -= OnSendingRA;
            Qurre.Events.Player.InteractDoor -= InteractingDoor;
        }
        public void InteractingDoor(InteractDoorEvent ev)
        {
            //ev.Door.Scale = new Vector3(0.5f, 0.5f, 0.5f);
            //ev.Door.BreakDoor();
            //ev.Door.Position = new Vector3(1, 0, 0) + ev.Door.Position;
        }
        public void OnSendingRA(SendingRAEvent ev) { }
    }
}
