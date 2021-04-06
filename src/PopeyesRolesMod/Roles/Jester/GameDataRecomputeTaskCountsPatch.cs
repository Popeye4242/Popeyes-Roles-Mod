using System.Collections.Generic;
using HarmonyLib;

namespace PopeyesRolesMod.Roles.Jester
{
    [HarmonyPatch(typeof(GameData), nameof(GameData.RecomputeTaskCounts))]
    public class GameDataRecomputeTaskCountsPatch
    {
        public static void Postfix(GameData __instance)
        {
			var gameData = __instance;
			gameData.TotalTasks = 0;
			gameData.CompletedTasks = 0;
            foreach (var playerInfo in gameData.AllPlayers)
            {
                if (playerInfo.Disconnected || playerInfo.Tasks == null || !playerInfo.Object || !PlayerControl.GameOptions.GhostsDoTasks && playerInfo.IsDead)
                {
                    continue;
                }

                if (playerInfo.IsImpostor || playerInfo.HasPlayerRole(Role.Jester))
                {
                    continue;
                }

                foreach (var taskInfo in playerInfo.Tasks)
                {
                    gameData.TotalTasks++;
                    if (taskInfo.Complete)
                    {
                        gameData.CompletedTasks++;
                    }
                }
            }
        }
    }
}
