using HarmonyLib;

namespace PopeyesRolesMod.Roles.Engineer
{

    [HarmonyPatch(typeof(MapRoom), nameof(MapRoom.SabotageOxygen))]
    class SabotageOxyPatch
    {
        static bool Prefix()
        {
            if (!PlayerControl.LocalPlayer.HasPlayerRole(Role.Engineer))
                return true;

            var playerData = PlayerControl.LocalPlayer.GetPlayerData();
            if (playerData.CanUseRepair)
                return false;

            playerData.UsedAbility = true;
            ShipStatus.Instance.RpcRepairSystem(SystemTypes.LifeSupp, 0 | 64);
            ShipStatus.Instance.RpcRepairSystem(SystemTypes.LifeSupp, 1 | 64);

            return false;
        }
    }

}
