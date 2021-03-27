using HarmonyLib;
using Reactor;

namespace PopeyesRolesMod.Roles.Jester
{
    [HarmonyPatch(typeof(UnityEngine.Object), nameof(UnityEngine.Object.Destroy),
      new[] { typeof(UnityEngine.Object) })]
    class MeetingExiledEnd
    {
        static void Prefix(UnityEngine.Object obj)
        {

            if (ExileController.Instance == null || obj != ExileController.Instance.gameObject)
                return;

            if (PlayerControl.LocalPlayer.HasPlayerRole(Role.ShapeShifter))
            {
                PlayerControl.LocalPlayer.GetPlayerData().SampledPlayer = null;
            }

            if (!(ExileController.Instance?.exiled?._object.HasPlayerRole(Role.Jester) ?? false))
                return;
            Rpc<JesterWinRpc>.Instance.Send(data: true, immediately: true);

        }
    }
}
