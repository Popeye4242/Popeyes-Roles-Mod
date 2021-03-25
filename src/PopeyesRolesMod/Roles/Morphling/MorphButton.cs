using Essentials.UI;
using Reactor;
using System;
using System.Linq;
using UnityEngine;

namespace PopeyesRolesMod.Roles.Morphling
{
    public static class MorphButton
    {
        private static bool lastF = false;
        public static CooldownButton Button { get; private set; }

        public static void CreateButton()
        {
            Button = new CooldownButton(PopeyesRolesModPlugin.Assets.MorphlingMorphButton, new HudPosition(GameplayButton.OffsetX, 1.5f, HudAlignment.BottomRight), 20f, 10f, 10f);
            Button.EffectStarted += Button_EffectStarted_Morph;
            Button.EffectEnded += Button_EffectEnded_Morph;
            Button.OnUpdate += Button_OnUpdate;
        }

        private static void Button_EffectEnded_Morph(object sender, EventArgs e)
        {
            Rpc<MorphRpc>.Instance.Send(new MorphData
            {
                Morph = false,
                Morphling = PlayerControl.LocalPlayer.PlayerId
            });
        }

        private static void Button_EffectStarted_Morph(object sender, EventArgs e)
        {
            Rpc<MorphRpc>.Instance.Send(new MorphData
            {
                Morph = true,
                Morphling = PlayerControl.LocalPlayer.PlayerId,
                SampledPlayer = PlayerControl.LocalPlayer.GetPlayerData().SampledPlayer.PlayerId
            });
        }

        private static void Button_OnUpdate(object sender, EventArgs e)
        {
            Button.Visible = PlayerControl.LocalPlayer.HasPlayerRole(Role.Morphling) && PlayerControl.LocalPlayer.GetPlayerData().SampledPlayer;
            Button.Clickable = true;

            lastF = Input.GetKeyUp(KeyCode.F);

            if (Input.GetKeyDown(KeyCode.F) && !lastF && Button.IsUsable)
                Button.PerformClick();
        }

    }
}
