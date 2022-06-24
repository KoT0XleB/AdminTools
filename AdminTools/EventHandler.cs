using Qurre;
using Qurre.API;
using Qurre.API.Events;
using UnityEngine;
using PlayerStatsSystem;
using Qurre.API.Objects;
using Qurre.API.Controllers;
using Qurre.API.Controllers.Items;
using InventorySystem.Items.ThrowableProjectiles;

using Map = Qurre.API.Map;
using Player = Qurre.API.Player;
using Object = UnityEngine.Object;
using Mirror;
using Qurre.API.Addons.Models;

namespace AdminTools
{
    public class EventHandler
    {
        public static Model Model = new Model("door", new Vector3(0, 0, 0));
        public static void SetPlayerSize(Player player, float x, float y, float z)
        {
            player.Scale = new Vector3(x, y, z);
        }
        public static void SetPlayerScale(Player player, float value)
        {
            player.Scale = new Vector3(value, value, value);
        }
        public static void TpPlayerPosition(Player player, float x, float y, float z)
        {
            player.Position = new Vector3(x, y, z);
        }
        public static void TpPlayerRandom(Player player)
        {
            player.TeleportToRandomRoom();
        }
        public static void KickPlayer(Player player, string reason, string admin)
        {
            player.Kick(reason, admin);
        }
        public static void ChangeRoomColor(Player player, Color color)
        {
            player.Room.LightColor = color;
        }
        public static void OverchargePlayerRoom(Player player, float duration)
        {
            player.Room.LightsOff(duration);
        }
        public static void DoorCreate(Player player)
        {
            var doorCount = Model.Doors.Count;
            foreach (var door in player.Room.Doors)
            {
                switch(door.Type)
                {
                    case DoorType.LCZ_Door:
                        {
                            Model.AddPart(new ModelDoor(Model, DoorPrefabs.DoorLCZ, player.Position + new Vector3(0, -1.2f, 0), new Vector3(0, player.Rotation.y, 0), player.Scale, null));
                        }
                        break;
                    case DoorType.HCZ_Door:
                        {
                            Model.AddPart(new ModelDoor(Model, DoorPrefabs.DoorHCZ, player.Position + new Vector3(0, -1.2f, 0), new Vector3(0, player.Rotation.y, 0), player.Scale, null));
                        }
                        break;
                    case DoorType.EZ_Door:
                        {
                            Model.AddPart(new ModelDoor(Model, DoorPrefabs.DoorEZ, player.Position + new Vector3(0, -1.2f, 0), new Vector3(0, player.Rotation.y, 0), player.Scale, null));
                        }
                        break;
                }
                if (Model.Doors.Count > doorCount) break;
            }
        }
        public static void DoorDestroy()
        {
            foreach (var door in Model.Doors) door.Door.Destroyed = true;
        }
        public static void PrimitiveCreate(Player player, PrimitiveType prim)
        {
            Model.AddPart(new ModelPrimitive(Model, prim, Color.white, player.Position, player.Rotation, player.Scale));
        }
        public static void PrimitiveDestroy()
        {
            foreach (var prim in Model.Primitives) GameObject.Destroy(prim.GameObject);
        }
        public static void ItemSpawn(Player player, ItemType item)
        {
            Pickup pickup = new Item(item).Spawn(player.Position);
        }
        public static void ItemSpawn(Player player, ItemType item, float x, float y, float z)
        {
            Pickup pickup = new Item(item).Spawn(new Vector3(x, y, z));
        }
        public static void ExplodePlayer(Player player, string reason)
        {
            GrenadeFrag grenade = new GrenadeFrag(ItemType.GrenadeHE);
            grenade.FuseTime = 0.5f;
            grenade.Base.transform.localScale = new Vector3(0, 0, 0);
            grenade.MaxRadius = 3f;
            grenade.Spawn(player.Position);
            player.Kill(reason);
        }
        public static void CleanUpObjects(string ClearObject)
        {
            if (ClearObject == "items")
            {
                Log.Info("Cleanup items");
                foreach (Pickup pickup in Map.Pickups)
                {
                    pickup.Base.DestroySelf();
                }
                return;
            }
            if (ClearObject == "ragdolls")
            {
                Log.Info("Cleanup ragdolls");
                foreach (Ragdoll ragdoll in Object.FindObjectsOfType<Ragdoll>())
                {
                    Object.Destroy(ragdoll.gameObject);
                }
                return;
            }
        }
        public static void GivePlayerAhp(Player player, int value)
        {
            if (player.MaxAhp < value) player.MaxAhp = value;
            player.PlayerStats.GetModule<AhpStat>().ServerAddProcess(value);
        }
        public static void ThrowBallOnPlayer(Player player)
        {
            GrenadeFrag grenade = new GrenadeFrag(ItemType.SCP018, player);
            grenade.FuseTime = 10f;
            grenade.MaxRadius = 5f;
            grenade.Throw(false);
        }
        public static void BotCreate(Player player, Player admin)
        {
            Bot.Create(admin.Position, admin.Rotation, player.Role, player.Nickname, player.RoleName, player.RoleColor);
        }
        public static void BotDestroy()
        {
            foreach (var bot in Map.Bots) bot.Destroy();
        }
        public static void CreateRagdoll(Player player)
        {
            var role = player.Role;
            var pos = player.Position;
            var rot = Quaternion.Euler(player.Rotation);
            var text = new CustomReasonDamageHandler("Лежу и отдыхаю");
            Qurre.API.Controllers.Ragdoll.Create(role, pos, rot, text, player.Nickname, player.Id);
        }
        public static void WorkbenchCreate(Player player)
        {
            WorkStation.Create(player.Position, player.Rotation, new Vector3(1, 1, 1));
        }
        public static void WorkbenchDestroy()
        {
            foreach (var work in Map.WorkStations) GameObject.Destroy(work.GameObject);
        }
        public static void DropPlayerItems(Player player)
        {
            player.DropItems();
        }
        public static void DropPlayerItems(Player player, Item item)
        {
            player.DropItem(item);
        }
    }
}
