using HarmonyLib;
using PopeyesRolesMod.Roles.Detective;
using PopeyesRolesMod.Roles.ShapeShifter;
using System;
using System.Collections.Generic;
using System.Text;

namespace PopeyesRolesMod.Roles
{

    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.MurderPlayer))]
    public static class PlayerMurderPatch
    {
        public static bool Prefix(PlayerControl __instance, PlayerControl __0)
        {
            if (__0.HasShield())
            {
                if (__0.AmOwner)
                {
                    var comp = __0.GetComponent<ShieldBehaviour>();
                    comp?.GlowShield();
                }
                return false;
            }

            __instance.Data.IsImpostor = true;
            return true;
        }

        public static void Postfix(PlayerControl __instance, [HarmonyArgument(0)] PlayerControl target)
        {
            var murderer = __instance;
            if (!murderer.HasPlayerRole(Role.Impostor) && !murderer.HasPlayerRole(Role.ShapeShifter))
            {
                murderer.Data.IsImpostor = false;
            }
            if (PlayerControl.LocalPlayer.HasPlayerRole(Role.Detective))
            {
                var playerData = PlayerControl.LocalPlayer.GetPlayerData();
                var deadPlayer = new DeadPlayer();
                deadPlayer.Player = target;
                deadPlayer.Murderer = murderer;
                playerData.DeadPlayers.Add(deadPlayer);

                if (murderer.PlayerId == target.PlayerId)
                {
                    // suicide
                    var deathReasons = Properties.Resources.SuicideDeath;
                    AddDeathReason(deadPlayer, deathReasons);
                }
                else
                {
                    // normal murder
                    if (murderer.GetComponent<MorphBehaviour>())
                    {
                        var deathReasons = Properties.Resources.ShapeShifterDeath;
                        AddDeathReason(deadPlayer, deathReasons);
                    }
                    else if (murderer.HasPlayerRole(Role.Hunter))
                    {
                        var deathReasons = Properties.Resources.HunterDeath;
                        AddDeathReason(deadPlayer, deathReasons);
                    }
                    else if (murderer.Data.IsImpostor)
                    {
                        var deathReasons = Properties.Resources.ImpostorDeath;
                        AddDeathReason(deadPlayer, deathReasons);
                    }
                }
            }
            if (target.HasPlayerRole(Role.Detective))
            {
                PlayerDataManager.Instance.ShieldedPlayer = null;
                foreach (var player in PlayerControl.AllPlayerControls)
                {
                    var shield = player.GetComponent<ShieldBehaviour>();
                    if (shield)
                    {
                        shield.Stop();
                    }
                }
            }
        }

        private static void AddDeathReason(DeadPlayer deadPlayer, string reasons)
        {
            var deathReasons = reasons.Split(";");
            var detail = deathReasons[PopeyesRolesModPlugin.Random.Next(0, deathReasons.Length)];
            deadPlayer.DeathDetails.Add(detail);
        }
    }
}
