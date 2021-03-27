using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PopeyesRolesMod.Roles.Jester
{
    [HarmonyPatch(typeof(EndGameManager), nameof(EndGameManager.SetEverythingUp))]
    public static class EndGamePatch
    {
        public static bool Prefix(EndGameManager __instance)
        {
            if (!TempData.DidHumansWin(TempData.EndReason))
                return true;

            var winners = TempData.winners.ToArray();
            var jester = GameData.Instance.AllPlayers.ToArray().FirstOrDefault(x => x.HasPlayerRole(Role.Jester));
            if (jester != default)
            {
                var jesterWinner = winners.FirstOrDefault(x => x.Name == jester.PlayerName);
                TempData.winners.Remove(jesterWinner);
            }

            return true;
        }

        public static void Postfix(EndGameManager __instance)
        {
            System.Console.WriteLine(PlayerControl.LocalPlayer.GetPlayerData().Role);
            if (!TempData.DidHumansWin(TempData.EndReason))
                return;

            var flag = PlayerControl.LocalPlayer.HasPlayerRole(Role.Jester);
            System.Console.WriteLine(flag);
            if (!flag)
                return;

            __instance.WinText.Text = "Defeat";
            __instance.WinText.Color = Palette.ImpostorRed;
            __instance.BackgroundBar.material.color = new Color32(255, 0, 0, 255);
        }
    }
}
