using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PopeyesRolesMod.Roles
{
    public class PlayerDataManager
    {
        private List<TaskTypes> sabotageTasks = new List<TaskTypes>
        {
            TaskTypes.FixComms,
            TaskTypes.FixLights,
            TaskTypes.ResetReactor,
            TaskTypes.ResetSeismic,
            TaskTypes.RestoreOxy
        };

        internal Dictionary<byte, PlayerData> PlayerData = new Dictionary<byte, PlayerData>();
        public LobbyConfig Config { get; set; }
        public PlayerTask CurrentSabotage
        {
            get
            {
                foreach (var task in PlayerControl.LocalPlayer.myTasks)
                    if (sabotageTasks.Contains(task.TaskType))
                        return task;
                return null;
            }

        }
        public PlayerControl ShieldedPlayer { get; set; }
        public static PlayerDataManager Instance { get; internal set; }
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
