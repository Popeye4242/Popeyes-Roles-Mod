using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace PopeyesRolesMod.Roles
{
    [HarmonyPatch(typeof(IntroCutscene.CoBegin__d), nameof(IntroCutscene.CoBegin__d.MoveNext))]
    public static class IntroScenePatch
    {
        static void Prefix(IntroCutscene.CoBegin__d __instance)
        {
            if (!PlayerControl.LocalPlayer.HasPlayerRole(Role.Jester))
                return;

            var jokerTeam = new Il2CppSystem.Collections.Generic.List<PlayerControl>();
            jokerTeam.Add(PlayerControl.LocalPlayer);
            __instance.yourTeam = jokerTeam;
        }



        static void Postfix(IntroCutscene.CoBegin__d __instance)
        {
            if (PlayerControl.LocalPlayer.HasPlayerRole(Role.ShapeShifter))
            {
                __instance.__this.Title.Text = Properties.Resources.ShapeShifterRoleName;
                __instance.__this.Title.scale = 2;
                return;
            }
            if (PlayerControl.LocalPlayer.HasPlayerRole(Role.Detective))
            {
                __instance.__this.Title.Text = Properties.Resources.DetectiveRoleName;
                __instance.__this.Title.Color = Colors.DetectiveColor ;
                __instance.__this.ImpostorText.Text = Properties.Resources.DetectiveImpostorText;
                __instance.__this.BackgroundBar.material.color = Colors.DetectiveColor;
                return;
            }
            if (PlayerControl.LocalPlayer.HasPlayerRole(Role.Hunter))
            {
                __instance.__this.Title.Text = Properties.Resources.HunterRoleName;
                __instance.__this.Title.Color = Colors.HunterColor;
                __instance.__this.ImpostorText.Text = Properties.Resources.HunterImpostorText;
                __instance.__this.BackgroundBar.material.color = Colors.HunterColor;
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
