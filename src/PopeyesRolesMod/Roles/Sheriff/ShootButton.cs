using Essentials.UI;
using Reactor;
using System;
using System.Linq;
using UnityEngine;

namespace PopeyesRolesMod.Roles.Sheriff
{
    public static class ShootButton
    {
        public static CooldownButton Button { get; private set; }

        public static void CreateButton()
        {
            Button = new CooldownButton(PopeyesRolesModPlugin.Assets.SheriffKillButton, new Vector2(PopeyesRolesModPlugin.KillButtonPosition, 0F));
            Button.OnClick += Button_OnClick;
            Button.OnUpdate += Button_OnUpdate;
        }

        private static void Button_OnUpdate(object sender, EventArgs e)
        {
            Button.Visible = PlayerDataManager.RoundStarted && PlayerControl.LocalPlayer.HasPlayerRole(Role.Sheriff);
            if (!Button.Visible)
                return;

            Button.Clickable = PlayerControl.LocalPlayer.FindClosestPlayer();
        }


        private static void Button_OnClick(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var target = PlayerControl.LocalPlayer.FindClosestPlayer();
            if (!target)
                return;


            Rpc<SheriffKillRpc>.Instance.Send(new SheriffKillRpc.OfficerKillData(PlayerControl.LocalPlayer.PlayerId, target.PlayerId), immediately: true);
         
            if (!target.Data.IsImpostor || target.HasShield())
            {
                target = PlayerControl.LocalPlayer;
                Rpc<SheriffKillRpc>.Instance.Send(new SheriffKillRpc.OfficerKillData(PlayerControl.LocalPlayer.PlayerId, target.PlayerId), immediately: true);
            }
        }
    }
}
