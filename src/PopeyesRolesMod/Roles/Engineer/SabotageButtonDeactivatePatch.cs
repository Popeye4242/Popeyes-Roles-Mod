using HarmonyLib;

namespace PopeyesRolesMod.Roles.Engineer
{
    [HarmonyPatch(typeof(MapRoom), nameof(MapRoom.Method_0))]
    class SabotageButtonDeactivatePatch
    {
        static bool Prefix()
        {
            return !PlayerControl.LocalPlayer.HasPlayerRole(Role.Engineer);
        }
    }
}
