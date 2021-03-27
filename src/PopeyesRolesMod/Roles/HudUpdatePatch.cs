﻿using HarmonyLib;
using InnerNet;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PopeyesRolesMod.Roles
{
    [HarmonyPatch(typeof(HudManager), nameof(HudManager.Update))]
    public static class HudUpdatePatch
    {
        public static void Postfix()
        {
            if (!ShipStatus.Instance || AmongUsClient.Instance.GameState != InnerNetClient.GameStates.Started)
                return;

            UpdateJesterTasks();
        }

        private static void UpdateJesterTasks()
        {
            var roles = new Dictionary<Role, Color>()
            { 
                { Role.Engineer, Colors.EngineerColor },
                { Role.Detective, Colors.DetectiveColor },
                { Role.Jester, Colors.JesterColor },
                { Role.Hunter, Colors.HunterColor}
            };
            foreach (var player in PlayerControl.AllPlayerControls)
            {
                var canSeeColor = player.AmOwner || PlayerControl.LocalPlayer.Data.IsDead;
                if (canSeeColor && roles.TryGetValue(player.GetPlayerData().Role, out var color))
                {
                    player.nameText.Color = color;
                    if (MeetingHud.Instance != null)
                    {
                        foreach (PlayerVoteArea playerVoteArea in MeetingHud.Instance.playerStates)
                        {
                            if (playerVoteArea.NameText != null && player.PlayerId == playerVoteArea.TargetPlayerId)
                            {
                                playerVoteArea.NameText.Color = color;
                            }
                        }
                    }
                }
            }
        }

    }
}
