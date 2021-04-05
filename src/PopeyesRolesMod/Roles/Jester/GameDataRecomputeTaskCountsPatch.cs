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
			for (int i = 0; i < gameData.AllPlayers.Count; i++)
            {
                GameData.PlayerInfo playerInfo = gameData.AllPlayers[i];
                if (playerInfo.Disconnected || playerInfo.Tasks == null || !playerInfo.Object || !PlayerControl.GameOptions.GhostsDoTasks && playerInfo.IsDead)
                {
                    continue;
                }

                if (playerInfo.IsImpostor || playerInfo.HasPlayerRole(Role.Jester))
                {
                    continue;
                }

                for (int j = 0; j < playerInfo.Tasks.Count; j++)
                {
                    gameData.TotalTasks++;
                    if (playerInfo.Tasks[j].Complete)
                    {
                        gameData.CompletedTasks++;
                    }
                }
            }
        }
    }
}
