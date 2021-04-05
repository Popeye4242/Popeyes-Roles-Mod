using HarmonyLib;
using System;
using System.Linq;

namespace PopeyesRolesMod.Roles.Detective
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.LocalPlayer.CmdReportDeadBody))]
    class BodyReportPatch
    {
        static void Postfix(PlayerControl __instance, GameData.PlayerInfo __0)
        {
            if (!__instance.HasPlayerRole(Role.Detective) || !__instance.AmOwner)
                return;
            var playerData = PlayerControl.LocalPlayer.GetPlayerData();

            DeadPlayer corpse = playerData.DeadPlayers.Where(x => x.Player.PlayerId == __0.PlayerId).FirstOrDefault();
            if (corpse != null)
            {
                var deathTime = (float)(DateTime.UtcNow - corpse.Timestamp).TotalMilliseconds / 1000;
                if (deathTime < PlayerDataManager.Instance.Config.DetectiveDeathReportThreshold)
                {
                    foreach (var reportMsg in corpse.DeathDetails)
                    {
                        DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, reportMsg);
                    }
                }
                else
                {
                    DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, Properties.Resources.CorpseTooOld);
                }
                DestroyableSingleton<HudManager>.Instance.Chat.AddChat(PlayerControl.LocalPlayer, string.Format(Properties.Resources.CorpseAge, Math.Round(deathTime)));
            }
        }
    }
}
