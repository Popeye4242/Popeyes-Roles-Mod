using HarmonyLib;
using Reactor;

namespace PopeyesRolesMod.Roles.Engineer
{

    [HarmonyPatch(typeof(MapRoom), nameof(MapRoom.SabotageLights))]
    class SabotageLightsPatch
    {
        static bool Prefix(MapRoom __instance)
        {
            if (!PlayerControl.LocalPlayer.HasPlayerRole(Role.Engineer))
                return true;
            var playerData = PlayerControl.LocalPlayer.GetPlayerData();
            if (!playerData.CanUseRepair)
                return false;

            playerData.UsedAbility = true;
            Rpc<FixLightsRpc>.Instance.Send(data: true, immediately: true);

            return false;
        }
    }

}
