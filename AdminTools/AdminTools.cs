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

using Object = UnityEngine.Object;
using Version = System.Version;
using Interactables.Interobjects.DoorUtils;
using System.Collections.Generic;

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
        public void OnSendingRA(SendingRAEvent ev)
        {
            if (ev.Name == "kick")
            {
                string Reason = string.Empty;
                Player player = Player.Get(ev.Args[0]);
                player.Kick(Reason, ev.Player.Nickname);

                return;
            }
            if (ev.Name == "tp")
            {
                Player player = Player.Get(ev.Args[0]);
                float x = float.Parse(ev.Args[1]);
                float y = float.Parse(ev.Args[2]);
                float z = float.Parse(ev.Args[3]);

                player.Position = new Vector3(x, y, z);
                return;
            }
            if (ev.Name == "randomtp")
            {
                Player player = Player.Get(ev.Args[0]);
                player.TeleportToRandomRoom();
                return;
            }
            if (ev.Name == "ahp")
            {
                Player player = Player.Get(ev.Args[0]);
                player.Ahp = int.Parse(ev.Args[1]);
                return;
            }
            if (ev.Name == "ball")
            {
                Player player = Player.Get(ev.Args[0]);
                var grenade = new ExplosiveGrenade(ItemType.SCP018, player);
                grenade.FuseTime = 10f;
                grenade.MaxRadius = 5f;
                grenade.Throw();
                // Говнокод пока не понятно как работает
                /*
                foreach (Player pl in Player.List)
                {
                    if (Vector3.Distance(pl.Position, grenade.Base.transform.position) <= grenade.MaxRadius)
                    {
                        if (pl.Hp > 70) player.Hp -= 70;
                        else pl.Kill();
                    }
                }
                */
                return;
            }
            if (ev.Name == "spawn_workbench")
            {
                WorkStation.Create(ev.Player.Position, ev.Player.Rotation, new Vector3(1, 1, 1));
                return;
            }
            if (ev.Name == "ragdoll")
            {
                //Qurre.API.Controllers.Ragdoll.Create(ev.Player.Position, Quaternion.Euler(new Vector2(0, 0)), , ev.Player);
            }
            if (ev.Name == "spawn_door")
            {
                //door.transform.position = new Vector3(49.4f, 1019.4f, -43.8f);
                //IEnumerator<Door> door = (IEnumerator<Door>)(from x in Map.Doors where x.Type == DoorType.LCZ_330 select x);
                //if (door.Current<Door> == 0)
                //{
                //    Door newdoor = door.Fist
                //}

                return;
            }
            if (ev.Name == "room_color")
            {
                ev.Player.Room.LightColor = Color.red;
                return;
            }
        }
    }
}
