using Essentials.UI;
using Reactor;
using System;
using System.Linq;
using UnityEngine;

namespace PopeyesRolesMod.Roles.Detective
{
    public static class ShieldButton
    {
        public static GameplayButton Button { get; private set; }
        static bool lastQ = false;

        public static void CreateButton()
        {
            Button = new GameplayButton(PopeyesRolesModPlugin.Assets.DetectiveShieldButton, new HudPosition(GameplayButton.OffsetX, 0, HudAlignment.BottomRight));
            Button.OnClick += Button_OnClick;
            Button.OnUpdate += Button_OnUpdate;
        }

        private static void Button_OnUpdate(object sender, EventArgs e)
        {
            Button.Visible=PlayerControl.LocalPlayer.HasPlayerRole(Role.Detective);
            Button.Clickable = PlayerControl.LocalPlayer.FindClosestTarget();

            lastQ = Input.GetKeyUp(KeyCode.Q);

            if (Input.GetKeyDown(KeyCode.Q) && !lastQ && Button.IsUsable)
                Button.PerformClick();
        }


        private static void Button_OnClick(object sender, System.ComponentModel.CancelEventArgs e)
        {
            PlayerControl target = PlayerControl.LocalPlayer.FindClosestTarget();
            if (!target)
                return;
            Rpc<GiveShieldRpc>.Instance.Send(target.PlayerId, immediately: true);
        }
    }
}
