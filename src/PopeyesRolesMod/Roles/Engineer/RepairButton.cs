using Essentials.UI;
using System;
using System.Linq;
using UnityEngine;

namespace PopeyesRolesMod.Roles.Engineer
{
    public static class RepairButton
    {
        public static CooldownButton Button { get; private set; }

        public static void CreateButton()
        {
            Button = new CooldownButton(PopeyesRolesModPlugin.Assets.EngineerRepairButton, new Vector2(PopeyesRolesModPlugin.KillButtonPosition, 0F));
            Button.OnClick += Button_OnClick; ;
            Button.OnUpdate += Button_OnUpdate;
        }

        private static void Button_OnUpdate(object sender, EventArgs e)
        {
            Button.Visible = PlayerDataManager.RoundStarted && PlayerControl.LocalPlayer.HasPlayerRole(Role.Engineer);
            if (!Button.Visible)
                return;
        }

        private static void Button_OnClick(object sender, System.ComponentModel.CancelEventArgs e)
        {

            DestroyableSingleton<HudManager>.Instance.ShowMap((Action<MapBehaviour>)delegate (MapBehaviour map)
            {
                map.ShowInfectedMap();
                map.ColorControl.baseColor = Color.gray;
            });
        }
    }
}
