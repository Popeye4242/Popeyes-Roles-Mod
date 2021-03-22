using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace PopeyesRolesMod.Roles.Engineer
{
    [HarmonyPatch(typeof(Vent), nameof(Vent.CanUse))]
    public class VentCanUsePatch
    {
        public static bool Prefix(Vent __instance, ref float __result, [HarmonyArgument(0)] GameData.PlayerInfo pc, [HarmonyArgument(1)] out bool canUse, [HarmonyArgument(2)] out bool couldUse)
        {

            canUse = false;
            couldUse = false;
            if (pc.HasPlayerRole(Role.Engineer) || pc.IsImpostor)
            {
                var vent = __instance;
                float num = float.MaxValue;
                var player = pc.Object;
                couldUse = !pc.IsDead && (player.CanMove || player.inVent);
                canUse = couldUse;
                if (canUse)
                {
                    Vector2 truePosition = player.GetTruePosition();
                    Vector3 position = vent.transform.position;
                    num = Vector2.Distance(truePosition, position);
                    canUse &= (num <= vent.UsableDistance && !PhysicsHelpers.AnythingBetween(truePosition, position, Constants.ShipAndObjectsMask, false));
                }
                __result = num;
            }

            return false;
        }
    }
}
