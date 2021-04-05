using Essentials.UI;
using Reactor.Networking;
using System;
using UnityEngine;

namespace PopeyesRolesMod.Roles.Detective
{
    public static class ShieldButton
    {
        public static GameplayButton Button { get; private set; }
        static bool lastQ = false;

        public static void CreateButton()
        {
            if (Button != null)
                return;
            Button = new GameplayButton(PopeyesRolesModPlugin.Assets.Heart, new HudPosition(GameplayButton.OffsetX, 0, HudAlignment.BottomRight));
            Button.OnClick += Button_OnClick;
            Button.OnUpdate += Button_OnUpdate;
        }

        private static void Button_OnUpdate(object sender, EventArgs e)
        {
            var playerData = PlayerControl.LocalPlayer.GetPlayerData();
            Button.Visible = playerData.Role == Role.Detective && !playerData.UsedAbility;
            Button.Clickable = PlayerControl.LocalPlayer.FindClosestTarget();

            if (!Button.Visible)
                return;

            HudManager.Instance.KillButton.SetTarget(PlayerControl.LocalPlayer.FindClosestTarget());

            lastQ = Input.GetKeyUp(KeyCode.Q);

            if (Input.GetKeyDown(KeyCode.Q) && !lastQ && Button.IsUsable)
                Button.PerformClick();
        }


        private static void Button_OnClick(object sender, System.ComponentModel.CancelEventArgs e)
        {
            PlayerControl target = PlayerControl.LocalPlayer.FindClosestTarget();
            if (!target)
                return;
            SoundManager.Instance.PlaySound(PopeyesRolesModPlugin.Assets.ShieldGuard, false, 100f);
            PlayerControl.LocalPlayer.GetPlayerData().UsedAbility = true;
            Rpc<GiveShieldRpc>.Instance.Send(target.PlayerId, immediately: true);
        }
    }
}
