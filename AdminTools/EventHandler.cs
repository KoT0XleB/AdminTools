using Qurre;
using Qurre.API;
using Qurre.API.Events;
using UnityEngine;
using PlayerStatsSystem;
using Qurre.API.Objects;
using Qurre.API.Controllers;
using Qurre.API.Controllers.Items;

using Map = Qurre.API.Map;
using Player = Qurre.API.Player;
using Object = UnityEngine.Object;

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
        public static void ExplodePlayer(Player player, string reason)
        {
            var grenade = new ExplosiveGrenade(ItemType.GrenadeHE, player);
            grenade.FuseTime = 0.1f;
            grenade.Base.transform.localScale = new Vector3(0, 0, 0);
            grenade.MaxRadius = 3f;
            grenade.Throw();
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
            var grenade = new ExplosiveGrenade(ItemType.SCP018, player);
            grenade.FuseTime = 10f;
            grenade.MaxRadius = 5f;
            grenade.Throw();
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
        public static void CreateDoor(Player player)
        {
            //door.transform.position = new Vector3(49.4f, 1019.4f, -43.8f);
            //IEnumerator<Door> door = (IEnumerator<Door>)(from x in Map.Doors where x.Type == DoorType.LCZ_330 select x);
            //if (door.Current<Door> == 0)
            //{
            //    Door newdoor = door.Fist
            //}
        }
        public static void CreateWorkbench(Player player)
        {
            WorkStation.Create(player.Position, player.Rotation, new Vector3(1, 1, 1));
        }
    }
}
