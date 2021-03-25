using HarmonyLib;
using System;
using System.Collections.Generic;

namespace PopeyesRolesMod.Roles
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public static class HudUpdatePatch
    {
        public static void Postfix()
        {
            if (!ShipStatus.Instance)
                return;
            
            
        }


    }
}
