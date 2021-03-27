using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace PopeyesRolesMod.Roles.Jester
{

    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.SetTasks))]
    public class PlayerControlSetTasksPatch
    {
        public static void Postfix(PlayerControl __instance)
        {
            var player = __instance;
            if (player.HasPlayerRole(Role.Jester))
            {
                ImportantTextTask importantTextTask = new GameObject("_Player").AddComponent<ImportantTextTask>();
                importantTextTask.transform.SetParent(PlayerControl.LocalPlayer.transform, false);
                importantTextTask.Text = Properties.Resources.JesterFakeTasks;
                __instance.myTasks.Insert(0, importantTextTask);
            }
        }
    }
}
