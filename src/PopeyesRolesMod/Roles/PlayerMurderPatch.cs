using HarmonyLib;
using PopeyesRolesMod.Roles.Medic;
using System;
using System.Collections.Generic;
using System.Text;

namespace PopeyesRolesMod.Roles
{

    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.MurderPlayer))]
    public static class PlayerMurderPatch
    {
        public static bool Prefix(PlayerControl __instance, PlayerControl __0)
        {
            if (__0.HasShield())
            {
                if (__0.AmOwner)
                {
                    System.Console.WriteLine("Making it glow");
                    var comp = __0.GetComponent<ShieldBehaviour>();
                    comp.GlowShield();
                }
                return false;
            }

            __instance.Data.IsImpostor = true;
            return true;
        }

        public static void Postfix(PlayerControl __instance, PlayerControl __0)
        {
            if (!__instance.HasPlayerRole(Role.Impostor) && !__instance.HasPlayerRole(Role.Morphling))
            {
                __instance.Data.IsImpostor = false;
            }
        }
    }
}
