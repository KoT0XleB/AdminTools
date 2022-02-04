using System;
using Qurre;
using Qurre.API;
using Qurre.API.Events;

namespace AdminTools
{
    public class AdminTools : Plugin
    {
        public override string Developer => "KoT0XleB#4663";
        public override string Name => "AdminTools";
        public override Version Version => new Version(1, 0, 0);
        public override void Enable() => RegisterEvents();
        public override void Disable() => UnregisterEvents();

        public void RegisterEvents()
        {
        
        }
        public void UnregisterEvents()
        {
        
        }
    }
}
