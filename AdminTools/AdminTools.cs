using System;
using Qurre;
using Qurre.API;
using Qurre.API.Controllers;
using Qurre.API.Controllers.Items;
using Qurre.API.Events;
using HarmonyLib;
using System.Reflection;
using CommandSystem;
using UnityEngine;
using System.Linq;
using Qurre.API.Objects;
using PlayerStatsSystem;
using Mirror;
using Interactables.Interobjects.DoorUtils;
using System.Collections.Generic;

using Object = UnityEngine.Object;
using Version = System.Version;

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
