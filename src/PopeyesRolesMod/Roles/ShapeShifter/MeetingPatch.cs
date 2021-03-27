using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace PopeyesRolesMod.Roles.ShapeShifter
{
    [HarmonyPatch(typeof(MeetingHud), nameof(MeetingHud.Start))]
    public class MeetingPatch
    {
        public static void Postfix()
        {
            MorphButton.Button.EndEffect();
        }
    }
}
