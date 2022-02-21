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
            player.Room.LightsOff(duration); // типо он свет отключит на N времени
        }
        public static void PlayerRoom(Player player)
        {
            
        }
        public static void PlayerDoors(Player player)
        {
            //player.Room.Doors
            Physics.Raycast(new Ray(player.ReferenceHub.PlayerCameraReference.transform.position, player.ReferenceHub.PlayerCameraReference.transform.forward), out RaycastHit hit, 100f);
            Log.Info(hit.distance);
            //if (!(Door.Get(hit.transform.gameObject) is Door door))
            //{
            //
            //}
            foreach (Component component in hit.transform.gameObject.GetComponents(typeof(Component)))
                Log.Info(component.ToString());
        }
        public static void ItemSpawn(Player player, ItemType item)
        {
            //Vector3 position = player.CameraTransform.position;
            //Vector3 forward = player.CameraTransform.forward;
            //Physics.Raycast(new Ray(position + forward, forward), out RaycastHit hit, 100f);
            Pickup pickup = new Item(item).Spawn(player.Position); // Vector.zero
            //NetworkServer.UnSpawn(pickup.Base.gameObject);
            //pickup.Base.GetComponent<Rigidbody>().isKinematic = true;
            //pickup.Base.transform.position = hit.transform.position;
            //pickup.Base.transform.rotation = hit.transform.rotation;
            //NetworkServer.Spawn(pickup.Base.gameObject);
        }
        public static void ItemSpawn(Player player, ItemType item, float x, float y, float z)
        {
            //Vector3 position = player.CameraTransform.position;
            //Vector3 forward = player.CameraTransform.forward;
            //Physics.Raycast(new Ray(position + forward, forward), out RaycastHit hit, 100f);
            Pickup pickup = new Item(item).Spawn(new Vector3(x, y, z)); // Vector.zero
            //NetworkServer.UnSpawn(pickup.Base.gameObject);
            //pickup.Base.GetComponent<Rigidbody>().isKinematic = true;
            //pickup.Base.transform.position = hit.transform.position;
            //pickup.Base.transform.rotation = hit.transform.rotation;
            //pickup.Scale = new Vector3(x, y, z);
            //NetworkServer.Spawn(pickup.Base.gameObject);
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
            // Некорректно работает
            if (player.MaxAhp < value) player.MaxAhp = value;
            player.PlayerStats.GetModule<AhpStat>().ServerAddProcess(value);
        }
        public static void ThrowBallOnPlayer(Player player)
        {
            GrenadeFrag grenade = new GrenadeFrag(ItemType.SCP018, player);
            grenade.FuseTime = 10f;
            grenade.MaxRadius = 5f;
            grenade.Throw(false);
            // Говнокод на время
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
        }
        public static void CreateBot(Player player, Player admin)
        {
            // надо запоминать бота и используя команду убирать
            var bot = Bot.Create(admin.Position, admin.Rotation, player.Role, player.Nickname, player.RoleName, player.RoleColor);
        }
        public static void CreateRagdoll(Player player)
        {
            var role = player.Role;
            var pos = player.Position;
            var rot = Quaternion.Euler(player.Rotation);
            var text = new CustomReasonDamageHandler("Лежу и отдыхаю");
            Qurre.API.Controllers.Ragdoll.Create(role, pos, rot, text, player.Nickname, player.Id);
        }
        public static void CreateDoor(Player player, Vector3 pos)
        {
            foreach (Door doors in Map.Doors)
            {
                if (doors.Type == DoorType.Escape_Secondary)
                {
                    doors.Position = pos;
                }
            }
        }
        public static void CreateWorkbench(Player player)
        {
            WorkStation.Create(player.Position, player.Rotation, new Vector3(1, 1, 1));
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
