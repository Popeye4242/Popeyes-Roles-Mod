using HarmonyLib;
using System;
using System.Reflection;

namespace PopeyesRolesMod.Plugin
{

    [HarmonyPatch(typeof(PingTracker), nameof(PingTracker.Update))]
    public static class PingPatch
    {
        public static void Postfix(PingTracker __instance)
        {
            __instance.text.Text += Environment.NewLine + "Popeyes Roles Mod " + Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
        }
    }
}
