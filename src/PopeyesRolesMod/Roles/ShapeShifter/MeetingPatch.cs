using HarmonyLib;

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
