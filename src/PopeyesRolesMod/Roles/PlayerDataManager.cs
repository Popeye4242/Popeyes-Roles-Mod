using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PopeyesRolesMod.Roles
{
    public class PlayerDataManager
    {
        internal Dictionary<byte, PlayerData> PlayerData = new Dictionary<byte, PlayerData>();
        public bool IsSabotageActive { get; internal set; } = false;
        public PlayerControl ShieldedPlayer { get; set; }
        public static PlayerDataManager Instance { get; internal set; }
        public static bool RoundStarted => Instance != null;
        public static void SetPlayerRole(byte playerId, Role role)
        {
            var playerData = Instance.PlayerData[playerId] = new PlayerData();
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
