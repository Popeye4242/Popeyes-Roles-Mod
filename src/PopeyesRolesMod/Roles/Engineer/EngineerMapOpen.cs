using HarmonyLib;
using UnityEngine;

namespace PopeyesRolesMod.Roles.Engineer
{

    [HarmonyPatch(typeof(MapBehaviour), nameof(MapBehaviour.FixedUpdate))]
    class EngineerMapUpdate
    {
        static void Postfix(MapBehaviour __instance)
        {
            if (!PlayerControl.LocalPlayer.HasPlayerRole(Role.Engineer))
                return;
            if (!__instance.IsOpen || !__instance.infectedOverlay.gameObject.active)
                return;
            __instance.ColorControl.baseColor =
                !PlayerDataManager.IsSabotageActive ? Color.gray : Colors.EngineerColor;

            var playerData = PlayerControl.LocalPlayer.GetPlayerData();
            var perc = playerData.UsedAbility ? 1f : 0f;

            foreach (var room in __instance.infectedOverlay.rooms)
            {
                if (room.special == null)
                    continue;
                room.special.material.SetFloat("_Desat", !PlayerDataManager.IsSabotageActive ? 1f : 0f);

                room.special.enabled = true;
                room.special.gameObject.SetActive(true);
                room.special.gameObject.active = true;
                room.special.material.SetFloat("_Percent", !PlayerControl.LocalPlayer.Data.IsDead ? perc : 1f);
            }
        }
    }

}
