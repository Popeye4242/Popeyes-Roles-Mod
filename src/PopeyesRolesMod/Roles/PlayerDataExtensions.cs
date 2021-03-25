using PopeyesRolesMod.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace PopeyesRolesMod
{
    public static class PlayerDataExtensions
    {
        public static bool HasPlayerRole(this GameData.PlayerInfo playerInfo, Role role)
        {
            return PlayerDataManager.Instance?.PlayerData[playerInfo.PlayerId].Role == role;
        }
        public static bool HasPlayerRole(this PlayerControl playerInfo, Role role)
        {
            return PlayerDataManager.Instance?.PlayerData[playerInfo.PlayerId].Role == role;
        }

        public static bool HasShield(this PlayerControl player)
        {
            return PlayerDataManager.Instance?.ShieldedPlayer && PlayerDataManager.Instance.ShieldedPlayer.PlayerId == player.PlayerId;
        }


        public static PlayerData GetPlayerData(this PlayerControl player)
        {
            return PlayerDataManager.Instance?.PlayerData[player.PlayerId];
        }
        public static PlayerControl FindClosestPlayer(this PlayerControl player)
        {
            PlayerControl result = null;
            float num = GameOptionsData.KillDistances[Mathf.Clamp(PlayerControl.GameOptions.KillDistance, 0, 2)];
            if (!ShipStatus.Instance)
            {
                return null;
            }
            Vector2 truePosition = player.GetTruePosition();
            List<GameData.PlayerInfo> allPlayers = GameData.Instance.AllPlayers.ToArray().ToList();
            for (int i = 0; i < allPlayers.Count; i++)
            {
                GameData.PlayerInfo playerInfo = allPlayers[i];
                if (!playerInfo.Disconnected && playerInfo.PlayerId != player.PlayerId && !playerInfo.IsDead)
                {
                    PlayerControl @object = playerInfo.Object;
                    if (@object)
                    {
                        Vector2 vector = @object.GetTruePosition() - truePosition;
                        float magnitude = vector.magnitude;
                        if (magnitude <= num && !PhysicsHelpers.AnyNonTriggersBetween(truePosition, vector.normalized, magnitude, Constants.ShipAndObjectsMask))
                        {
                            result = @object;
                            num = magnitude;
                        }
                    }
                }
            }
            return result;
        }
    }
}
