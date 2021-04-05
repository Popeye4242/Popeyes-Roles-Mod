using Essentials.UI;
using System;
using UnityEngine;

namespace PopeyesRolesMod.Roles.ShapeShifter
{
    public static class SampleButton
    {
        private static bool lastF = false;
        public static CooldownButton Button { get; private set; }

        public static void CreateButton()
        {
            if (Button != null)
            {
                Button.CooldownDuration = PlayerDataManager.Instance.Config.ShapeShifterSampleCooldown;
                return;
            }
            Button = new CooldownButton(PopeyesRolesModPlugin.Assets.PlaceHolder, new HudPosition(GameplayButton.OffsetX, 1.2f, HudAlignment.BottomRight), PlayerDataManager.Instance.Config.ShapeShifterSampleCooldown, 0f, 0f);
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
            SoundManager.Instance.PlaySound(PopeyesRolesModPlugin.Assets.SuckPop, false, 100f);
        }
    }
}
