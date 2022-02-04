using System;
using System.Linq;
using Qurre;
using Qurre.API;
using Qurre.API.Events;
using UnityEngine;
using Qurre.API.Controllers;
using Qurre.API.Controllers.Items;

using Player = Qurre.API.Player;

namespace AdminTools
{
    public class EventHandler
    {
        public static void SetPlayerSize(Player player, float x, float y, float z)
        {
            player.Scale = new Vector3(x, y, z);
        }
        public static void SetPlayerScale(Player player, float value)
        {
            player.Scale = new Vector3(value, value, value);
        }
        public static void ExplodePlayer(Player player)
        {
            /*
            var grenade = new ExplosiveGrenade(ItemType.GrenadeHE, player);
            grenade.FuseTime = 0.1f;
            grenade.Base.transform.localScale = new Vector3(0, 0, 0);
            grenade.MaxRadius = 5f;

            grenade.Throw();
            player.Kill();
            */
        }
        public static void CleanUpObjects()
        {
            /*
            if (ev.Name == "cleanup")
            {
                if (ev.Args[0] == "items")
                {
                    Log.Info("Cleanup items");
                    foreach (Pickup pickup in Map.Pickups)
                    {
                        pickup.Base.DestroySelf();
                    }
                    return;
                }
                if (ev.Args[0] == "ragdolls")
                {
                    Log.Info("Cleanup ragdolls");
                    foreach (Ragdoll ragdoll in Object.FindObjectsOfType<Ragdoll>())
                    {
                        Object.Destroy(ragdoll.gameObject);
                    }
                    return;
                }
            }
            */
        }
        
        public static void CreateBot()
        {
            /*
            Bot.Create(ev.Player.Position, ev.Player.Rotation);
            */
        }
    }
}
