﻿using Essentials.UI;
using Reactor;
using System;
using System.Linq;
using UnityEngine;

namespace PopeyesRolesMod.Roles.Medic
{
    public static class ShieldButton
    {
        public static CooldownButton Button { get; private set; }

        public static void CreateButton()
        {
            Button = new CooldownButton(PopeyesRolesModPlugin.Assets.MedicShieldButton, new Vector2(PopeyesRolesModPlugin.KillButtonPosition, 0F));
            Button.OnClick += Button_OnClick;
            Button.OnUpdate += Button_OnUpdate;
        }

        private static void Button_OnUpdate(object sender, EventArgs e)
        {
            Button.Visible = PlayerDataManager.RoundStarted && PlayerControl.LocalPlayer.HasPlayerRole(Role.Medic);
            if (!Button.Visible)
                return;

            Button.Clickable = PlayerControl.LocalPlayer.FindClosestPlayer();
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