using Essentials.UI;
using Reactor;
using System;
using System.Linq;
using UnityEngine;

namespace PopeyesRolesMod.Roles.ShapeShifter
{
    public static class MorphButton
    {
        private static bool lastF = false;
        public static CooldownButton Button { get; private set; }

        public static void CreateButton()
        {
            var cfg = PlayerDataManager.Instance.Config;
            if (Button != null)
            {
                Button.EffectDuration = cfg.ShapeShifterMorphDuration;
                Button.CooldownDuration = cfg.ShapeShifterMorphCooldown;
                return;
            }
            Button = new CooldownButton(PopeyesRolesModPlugin.Assets.Trophy, new HudPosition(GameplayButton.OffsetX, 1.2f, HudAlignment.BottomRight), 0.5f, cfg.ShapeShifterMorphDuration, 0.5f);
            Button.EffectStarted += Button_EffectStarted_Morph;
            Button.EffectEnded += Button_EffectEnded_Morph;
            Button.OnUpdate += Button_OnUpdate;
        }

        private static void Button_EffectEnded_Morph(object sender, EventArgs e)
        {
            Rpc<MorphRpc>.Instance.Send(new MorphData
            {
                Morph = false,
                ShapeShifter = PlayerControl.LocalPlayer.PlayerId
            });
            SoundManager.Instance.PlaySound(PopeyesRolesModPlugin.Assets.Swoosh, false, 100f);
            var cfg = PlayerDataManager.Instance.Config;
            Button.ApplyCooldown(cfg.ShapeShifterMorphCooldown);
        }

        private static void Button_EffectStarted_Morph(object sender, EventArgs e)
        {
            Rpc<MorphRpc>.Instance.Send(new MorphData
            {
                Morph = true,
                ShapeShifter = PlayerControl.LocalPlayer.PlayerId,
                SampledPlayer = PlayerControl.LocalPlayer.GetPlayerData().SampledPlayer.PlayerId
            });
            SoundManager.Instance.PlaySound(PopeyesRolesModPlugin.Assets.Swoosh, false, 100f);
        }

        private static void Button_OnUpdate(object sender, EventArgs e)
        {
            Button.Visible = PlayerControl.LocalPlayer.HasPlayerRole(Role.ShapeShifter) && PlayerControl.LocalPlayer.GetPlayerData().SampledPlayer;
            Button.Clickable = true;

            lastF = Input.GetKeyUp(KeyCode.F);

            if (Input.GetKeyDown(KeyCode.F) && !lastF && Button.IsUsable)
                Button.PerformClick();
        }

    }
}
