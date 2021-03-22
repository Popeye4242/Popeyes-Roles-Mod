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

            if (!(ExileController.Instance?.exiled?._object.HasPlayerRole(Role.Jester) ?? false))
                return;
            System.Console.WriteLine("Joker won");
            Rpc<JesterWinRpc>.Instance.Send(data: true, immediately: true);

        }
    }
}
