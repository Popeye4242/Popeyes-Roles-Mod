using HarmonyLib;

namespace PopeyesRolesMod.Roles.Engineer
{
    [HarmonyPatch(typeof(MapBehaviour), nameof(MapBehaviour.ShowInfectedMap))]
    class EngineerMapOpen
    {
        static void Postfix(MapBehaviour __instance)
        {
            if (!PlayerControl.LocalPlayer.HasPlayerRole(Role.Engineer))
                return;
            if (!__instance.IsOpen)
                return;

            __instance.ColorControl.baseColor = Colors.EngineerColor;
            foreach (var room in __instance.infectedOverlay.rooms)
            {
                if (room.door == null)
                    continue;

                room.door.enabled = false;
                room.door.gameObject.SetActive(false);
                room.door.gameObject.active = false;
            }
        }
    }
}
