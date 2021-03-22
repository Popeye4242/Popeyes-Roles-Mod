using Essentials.UI;
using System;
using System.Linq;
using UnityEngine;

namespace PopeyesRolesMod.Roles.Morphling
{
    public static class SampleButton
    {
        public static CooldownButton Button { get; private set; }

        public static void CreateButton()
        {
            Button = new CooldownButton(PopeyesRolesModPlugin.Assets.MorphlingSampleButton, new Vector2(PopeyesRolesModPlugin.KillButtonPosition, 1.3f));
            Button.OnClick += Button_OnClick;
            Button.OnUpdate += Button_OnUpdate;
        }

        private static void Button_OnUpdate(object sender, EventArgs e)
        {
            if (!PlayerControl.LocalPlayer.infectedSet)
            {
                Button.Visible = false;
                return;
            }
            Button.Visible = PlayerControl.LocalPlayer.HasPlayerRole(Role.Morphling);
        }

        private static void Button_OnClick(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Button.UpdateSprite(PopeyesRolesModPlugin.Assets.MorphlingMorphButton);
        }
    }
}
