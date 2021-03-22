using HarmonyLib;

namespace PopeyesRolesMod.Roles.Engineer
{
    [HarmonyPatch(typeof(MapRoom), nameof(MapRoom.SabotageComms))]
    class SabotageCommsPatch
    {
        static bool Prefix(MapRoom __instance)
        {
            if (!PlayerControl.LocalPlayer.HasPlayerRole(Role.Engineer))
                return true;
            var playerData = PlayerControl.LocalPlayer.GetPlayerData();
            if (!playerData.CanUseRepair)
                return false;

            playerData.UsedAbility = true;
            ShipStatus.Instance.RpcRepairSystem(SystemTypes.Comms, 16 | 0);
            ShipStatus.Instance.RpcRepairSystem(SystemTypes.Comms, 16 | 1);

            return false;
        }
    }

}
