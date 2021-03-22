using HarmonyLib;

namespace PopeyesRolesMod.Roles.Engineer
{
    [HarmonyPatch(typeof(MapRoom), nameof(MapRoom.SabotageReactor))]
    class SabotageReactorPatch
    {
        static bool Prefix(MapRoom __instance)
        {
            if (!PlayerControl.LocalPlayer.HasPlayerRole(Role.Engineer))
                return true;

            var playerData = PlayerControl.LocalPlayer.GetPlayerData();
            if (!playerData.CanUseRepair)
                return false;

            playerData.UsedAbility = true;
            ShipStatus.Instance.RpcRepairSystem(SystemTypes.Reactor, 16);

            return false;
        }
    }
}
