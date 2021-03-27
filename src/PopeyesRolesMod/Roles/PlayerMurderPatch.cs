using HarmonyLib;
using PopeyesRolesMod.Roles.Detective;
using PopeyesRolesMod.Roles.ShapeShifter;
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
                    var comp = __0.GetComponent<ShieldBehaviour>();
                    comp.GlowShield();
                }
                return false;
            }

            __instance.Data.IsImpostor = true;
            return true;
        }

        public static void Postfix(PlayerControl __instance, [HarmonyArgument(0)] PlayerControl target)
        {
            var murderer = __instance;
            if (!murderer.HasPlayerRole(Role.Impostor) && !murderer.HasPlayerRole(Role.ShapeShifter))
            {
                murderer.Data.IsImpostor = false;
            }
            if (PlayerControl.LocalPlayer.HasPlayerRole(Role.Detective))
            {
                var playerData = PlayerControl.LocalPlayer.GetPlayerData();
                var deadPlayer = new DeadPlayer();
                deadPlayer.Player = target;
                deadPlayer.Murderer = murderer;
                playerData.DeadPlayers.Add(deadPlayer);

                if (murderer.PlayerId == target.PlayerId)
                {
                    // suicide
                    deadPlayer.Suicide = true;
                }
                else
                {
                    // normal murder
                    if (murderer.GetComponent<MorphBehaviour>())
                    {
                        deadPlayer.WasKilledByShapeShifter = true;
                    }
                }
                System.Console.WriteLine(murderer.name + " murdered " + target.name);
            }
        }
    }
}
