using HarmonyLib;
using System.Linq;
using UnityEngine;

namespace PopeyesRolesMod.Roles.Jester
{
    [HarmonyPatch(typeof(EndGameManager), nameof(EndGameManager.SetEverythingUp))]
    public static class EndGamePatch
    {
        public static void Prefix(EndGameManager __instance)
        {
            if (!TempData.DidHumansWin(TempData.EndReason))
                return;

            var winners = TempData.winners.ToArray();
            var jester = GameData.Instance.AllPlayers.ToArray().FirstOrDefault(x => x.HasPlayerRole(Role.Jester));
            if (jester != default)
            {
                var jesterWinner = winners.FirstOrDefault(x => x.Name == jester.PlayerName);
                TempData.winners.Remove(jesterWinner);
            }

            return;
        }

        public static void Postfix(EndGameManager __instance)
        {
            if (!TempData.DidHumansWin(TempData.EndReason))
                return;

            var flag = PlayerControl.LocalPlayer.HasPlayerRole(Role.Jester);
            if (!flag)
                return;

            __instance.WinText.Text = "Defeat";
            __instance.WinText.Color = Palette.ImpostorRed;
            __instance.BackgroundBar.material.color = new Color32(255, 0, 0, 255);
        }
    }
}
