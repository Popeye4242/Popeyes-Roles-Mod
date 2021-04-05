﻿using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace PopeyesRolesMod.Plugin
{

    [HarmonyPatch(typeof(PingTracker), nameof(PingTracker.Update))]
    public static class PingPatch
    {
        private static string version = Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
        public static void Postfix(PingTracker __instance)
        {
            __instance.text.Text += "\npopmod.net";
            __instance.text.Text += "\nPop Mod v" + version;
        }
    }
}
