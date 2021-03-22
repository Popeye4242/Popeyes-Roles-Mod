using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace PopeyesRolesMod.Roles
{
    [HarmonyPatch(typeof(IntroCutscene.CoBegin__d), nameof(IntroCutscene.CoBegin__d.MoveNext))]
    public static class IntroScenePatch
    {
        static bool Prefix(IntroCutscene.CoBegin__d __instance)
        {
            if (!PlayerControl.LocalPlayer.HasPlayerRole(Role.Jester))
                return true;

            var jokerTeam = new Il2CppSystem.Collections.Generic.List<PlayerControl>();
            jokerTeam.Add(PlayerControl.LocalPlayer);
            __instance.yourTeam = jokerTeam;
            return true;
        }



        static void Postfix(IntroCutscene.CoBegin__d __instance)
        {
            if (PlayerControl.LocalPlayer.HasPlayerRole(Role.Morphling))
            {
                __instance.__this.Title.Text = Properties.Resources.MorphlingRoleName;
                return;
            }
            if (PlayerControl.LocalPlayer.HasPlayerRole(Role.Medic))
            {
                __instance.__this.Title.Text = Properties.Resources.MedicRoleName;
                __instance.__this.Title.Color = Colors.MedicColor ;
                __instance.__this.ImpostorText.Text = Properties.Resources.MedicImpostorText;
                __instance.__this.BackgroundBar.material.color = Colors.MedicColor;
                return;
            }
            if (PlayerControl.LocalPlayer.HasPlayerRole(Role.Sheriff))
            {
                __instance.__this.Title.Text = Properties.Resources.SheriffRoleName;
                __instance.__this.Title.Color = Colors.SheriffColor;
                __instance.__this.ImpostorText.Text = Properties.Resources.SheriffImpostorText;
                __instance.__this.BackgroundBar.material.color = Colors.SheriffColor;
                return;
            }
            if (PlayerControl.LocalPlayer.HasPlayerRole(Role.Engineer))
            {
                __instance.__this.Title.Text = Properties.Resources.EngineerRoleName;
                __instance.__this.Title.Color = Colors.EngineerColor;
                __instance.__this.ImpostorText.Text = Properties.Resources.EngineerImpostorText;
                __instance.__this.BackgroundBar.material.color = Colors.EngineerColor;
                return;
            }
            if (PlayerControl.LocalPlayer.HasPlayerRole(Role.Jester))
            {
                __instance.__this.Title.Text = Properties.Resources.JesterRoleName;
                __instance.__this.Title.Color = Colors.JesterColor;
                __instance.__this.ImpostorText.Text = Properties.Resources.JesterImpostorText;
                __instance.__this.BackgroundBar.material.color = Colors.JesterColor;
                return;
            }
        }
    }
}
