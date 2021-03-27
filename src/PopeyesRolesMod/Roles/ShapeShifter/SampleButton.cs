using Essentials.UI;
using Reactor;
using System;
using System.Linq;
using UnityEngine;

namespace PopeyesRolesMod.Roles.ShapeShifter
{
    public static class SampleButton
    {
        private static bool lastF = false;
        public static GameplayButton Button { get; private set; }

        public static void CreateButton()
        {
            if (Button != null)
            {
                Button.Dispose();
            }
            Button = new CooldownButton(PopeyesRolesModPlugin.Assets.ShapeShifterSampleButton, new HudPosition(GameplayButton.OffsetX, 1.5f, HudAlignment.BottomRight), PlayerDataManager.Instance.Config.ShapeShifterSampleCooldown, 0f, 0f);
            Button.OnClick += Button_OnClick_Sample;
            Button.OnUpdate += Button_OnUpdate;
        }

        private static void Button_OnUpdate(object sender, EventArgs e)
        {
            Button.Visible = PlayerControl.LocalPlayer.HasPlayerRole(Role.ShapeShifter) && !(PlayerControl.LocalPlayer.GetPlayerData()?.SampledPlayer);
            Button.Clickable = PlayerControl.LocalPlayer.FindClosestTarget();

            lastF = Input.GetKeyUp(KeyCode.F);

            if (Input.GetKeyDown(KeyCode.F) && !lastF && Button.IsUsable)
                Button.PerformClick();
        }

        private static void Button_OnClick_Sample(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var sampledPlayer = PlayerControl.LocalPlayer.FindClosestTarget();
            PlayerControl.LocalPlayer.GetPlayerData().SampledPlayer = sampledPlayer;
        }
    }
}
