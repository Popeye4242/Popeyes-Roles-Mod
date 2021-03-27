using Essentials.UI;
using Reactor;
using System;
using System.Linq;
using UnityEngine;

namespace PopeyesRolesMod.Roles.Hunter
{
    public static class ShootButton
    {
        public static CooldownButton Button { get; private set; }

        static bool lastQ = false;
        public static void CreateButton()
        {
            if (Button != null)
            {
                Button.Dispose();
            }
            Button = new CooldownButton(PopeyesRolesModPlugin.Assets.HunterKillButton, new HudPosition(GameplayButton.OffsetX, 0, HudAlignment.BottomRight), 30f, 0f, 10f);
            Button.OnClick += Button_OnClick;
            Button.OnUpdate += Button_OnUpdate;
        }

        private static void Button_OnUpdate(object sender, EventArgs e)
        {
            Button.Visible = PlayerControl.LocalPlayer.HasPlayerRole(Role.Hunter);
            Button.Clickable = PlayerControl.LocalPlayer.FindClosestTarget();

            lastQ = Input.GetKeyUp(KeyCode.Q);

            if (Input.GetKeyDown(KeyCode.Q) && !lastQ && Button.IsUsable)
                Button.PerformClick();
        }


        private static void Button_OnClick(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var target = PlayerControl.LocalPlayer.FindClosestTarget();
            if (!target)
                return;


            Rpc<HunterKillRpc>.Instance.Send(new HunterKillRpc.OfficerKillData(PlayerControl.LocalPlayer.PlayerId, target.PlayerId), immediately: true);
         
            if (!target.Data.IsImpostor || target.HasShield())
            {
                target = PlayerControl.LocalPlayer;
                Rpc<HunterKillRpc>.Instance.Send(new HunterKillRpc.OfficerKillData(PlayerControl.LocalPlayer.PlayerId, target.PlayerId), immediately: true);
            }
        }
    }
}
