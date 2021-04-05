using HarmonyLib;

namespace PopeyesRolesMod.Roles
{
    [HarmonyPatch(typeof(IntroCutscene._CoBegin_d__11), nameof(IntroCutscene._CoBegin_d__11.MoveNext))]
    public static class IntroScenePatch
    {
        static void Prefix(IntroCutscene._CoBegin_d__11 __instance)
        {
            if (!PlayerControl.LocalPlayer.HasPlayerRole(Role.Jester))
                return;

            var jokerTeam = new Il2CppSystem.Collections.Generic.List<PlayerControl>();
            jokerTeam.Add(PlayerControl.LocalPlayer);
            __instance.yourTeam = jokerTeam;
        }



        static void Postfix(IntroCutscene._CoBegin_d__11 __instance)
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
