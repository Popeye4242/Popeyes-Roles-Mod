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
    }
}
