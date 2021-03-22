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
            
            UpdateIsSabotageActive();
        }


        private static void UpdateIsSabotageActive()
        {
            var sabotageTasks = new List<TaskTypes>()
            {
                TaskTypes.FixComms,
                TaskTypes.FixLights,
                TaskTypes.ResetReactor,
                TaskTypes.ResetSeismic,
                TaskTypes.RestoreOxy
            };

            var sabotageActive = false;
            foreach (var task in PlayerControl.LocalPlayer.myTasks)
                if (sabotageTasks.Contains(task.TaskType))
                    sabotageActive = true;
            PlayerDataManager.Instance.IsSabotageActive = sabotageActive;
        }
    }
}
