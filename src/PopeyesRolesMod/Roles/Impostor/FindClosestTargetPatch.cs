using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PopeyesRolesMod.Roles.Impostor
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.FindClosestTarget))]
    public class FindClosestTargetPatch
    {
        public static void Postfix(PlayerControl __instance, ref PlayerControl __result)
        {
            var player = __instance;
            PlayerControl result = null;
            float num = GameOptionsData.KillDistances[Mathf.Clamp(PlayerControl.GameOptions.KillDistance, 0, 2)];
            if (!ShipStatus.Instance)
            {
                __result = null;
                return;
            }
            Vector2 truePosition = player.GetTruePosition();
            List<GameData.PlayerInfo> allPlayers = GameData.Instance.AllPlayers.ToArray().ToList();
            for (int i = 0; i < allPlayers.Count; i++)
            {
                GameData.PlayerInfo playerInfo = allPlayers[i];
                if (playerInfo.Disconnected || playerInfo.PlayerId == player.PlayerId || playerInfo.IsDead || playerInfo.Object.inVent)
                    continue;

                if (player.Data.IsImpostor && playerInfo.IsImpostor)
                    continue;

                PlayerControl @object = playerInfo.Object;
                if (@object)
                {
                    Vector2 vector = @object.GetTruePosition() - truePosition;
                    float magnitude = vector.magnitude;
                    if (magnitude <= num && !PhysicsHelpers.AnyNonTriggersBetween(truePosition, vector.normalized, magnitude, Constants.ShipAndObjectsMask))
                    {
                        result = @object;
                        num = magnitude;
                    }
                }
            }
            __result = result;
        }
    }
}
