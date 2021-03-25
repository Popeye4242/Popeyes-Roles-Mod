using Essentials.UI;
using System;
using System.Linq;
using UnityEngine;

namespace PopeyesRolesMod.Roles.Morphling
{
    public static class SampleButton
    {
        private static bool lastF = false;
        public static GameplayButton Button { get; private set; }

        public static void CreateButton()
        {
            Button = new GameplayButton(PopeyesRolesModPlugin.Assets.MorphlingSampleButton, new HudPosition(GameplayButton.OffsetX, 1.5f, HudAlignment.BottomRight));
            Button.OnClick += Button_OnClick_Sample;
            Button.OnUpdate += Button_OnUpdate;
        }

        private static void Button_OnUpdate(object sender, EventArgs e)
        {
            Button.Visible = PlayerControl.LocalPlayer.HasPlayerRole(Role.Morphling) && !(PlayerControl.LocalPlayer.GetPlayerData()?.SampledPlayer);
            Button.Clickable = PlayerControl.LocalPlayer.FindClosestPlayer();

            lastF = Input.GetKeyUp(KeyCode.F);

            if (Input.GetKeyDown(KeyCode.F) && !lastF && Button.IsUsable)
                Button.PerformClick();
        }

        private static void Button_OnClick_Sample(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var sampledPlayer = PlayerControl.LocalPlayer.FindClosestPlayer();
            PlayerControl.LocalPlayer.GetPlayerData().SampledPlayer = sampledPlayer;
        }
    }
}
