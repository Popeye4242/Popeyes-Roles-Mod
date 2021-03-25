using Essentials.UI;
using Reactor;
using System;
using System.Linq;
using UnityEngine;

namespace PopeyesRolesMod.Roles.Medic
{
    public static class ShieldButton
    {
        public static GameplayButton Button { get; private set; }
        static bool lastQ = false;

        public static void CreateButton()
        {
            Button = new GameplayButton(PopeyesRolesModPlugin.Assets.MedicShieldButton, new HudPosition(GameplayButton.OffsetX, 0, HudAlignment.BottomRight));
            Button.OnClick += Button_OnClick;
            Button.OnUpdate += Button_OnUpdate;
        }

        private static void Button_OnUpdate(object sender, EventArgs e)
        {
            Button.Visible=PlayerControl.LocalPlayer.HasPlayerRole(Role.Medic);
            Button.Clickable = PlayerControl.LocalPlayer.FindClosestPlayer();

            lastQ = Input.GetKeyUp(KeyCode.Q);

            if (Input.GetKeyDown(KeyCode.Q) && !lastQ && Button.IsUsable)
                Button.PerformClick();
        }


        private static void Button_OnClick(object sender, System.ComponentModel.CancelEventArgs e)
        {
            PlayerControl target = PlayerControl.LocalPlayer.FindClosestPlayer();
            if (!target)
                return;
            Rpc<GiveShieldRpc>.Instance.Send(target.PlayerId, immediately: true);
        }
    }
}
