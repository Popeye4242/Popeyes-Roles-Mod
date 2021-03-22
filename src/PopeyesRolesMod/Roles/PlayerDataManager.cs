using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PopeyesRolesMod.Roles
{
    public static class PlayerDataManager
    {
        internal static Dictionary<byte, PlayerData> PlayerData = new Dictionary<byte, PlayerData>();
        public static bool RoundStarted { get; set; } = false;
        public static bool IsSabotageActive { get; internal set; } = false;
        public static PlayerControl ShieldedPlayer { get; set; }
        public static void SetPlayerRole(byte playerId, Role role)
        {
            var playerData = PlayerData[playerId] = new PlayerData();
            playerData.Role = role;
        }

        public static IEnumerable<PlayerControl> GetImpostors()
        {
            return PlayerControl.AllPlayerControls.ToArray().Where(x => x.Data.IsImpostor);
        }

        public static IEnumerable<PlayerControl> GetCrewmates()
        {
            return PlayerControl.AllPlayerControls.ToArray().Where(x => !x.Data.IsImpostor);
        }
        public static IEnumerable<PlayerControl> GetAliveImpostors()
        {
            return GetImpostors().Where(x => !x.Data.IsDead);
        }

        public static PlayerControl GetPlayerById(byte id)
        {
            return PlayerControl.AllPlayerControls.ToArray().First(x => x.PlayerId == id);
        }
    } 
}
