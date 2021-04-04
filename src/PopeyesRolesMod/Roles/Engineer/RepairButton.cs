using Essentials.UI;
using HarmonyLib;
using Reactor;
using Reactor.Networking;
using System;
using System.Linq;
using UnityEngine;

namespace PopeyesRolesMod.Roles.Engineer
{
    public static class RepairButton
    {
        public static GameplayButton Button { get; private set; }
        static bool lastQ = false;

        public static void CreateButton()
        {
            if (Button != null)
                return;
            Button = new GameplayButton(PopeyesRolesModPlugin.Assets.Settings, new HudPosition(GameplayButton.OffsetX, 0, HudAlignment.BottomRight));
            Button.OnClick += Button_OnClick; ;
            Button.OnUpdate += Button_OnUpdate;

            lastQ = Input.GetKeyUp(KeyCode.Q);

            if (Input.GetKeyDown(KeyCode.Q) && !lastQ && Button.IsUsable)
                Button.PerformClick();
        }

        private static void Button_OnUpdate(object sender, EventArgs e)
        {
            var playerData = PlayerControl.LocalPlayer.GetPlayerData();
            Button.Visible = playerData.Role == Role.Engineer && !playerData.UsedAbility && !PlayerControl.LocalPlayer.Data.IsDead;
            Button.Clickable = PlayerDataManager.Instance?.CurrentSabotage != null;
        }

        private static void Button_OnClick(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (PlayerDataManager.Instance.CurrentSabotage == null)
                return;

            switch (PlayerDataManager.Instance.CurrentSabotage.TaskType)
            {
                case TaskTypes.FixComms:
                    ShipStatus.Instance.RpcRepairSystem(SystemTypes.Comms, 16 | 0);
                    ShipStatus.Instance.RpcRepairSystem(SystemTypes.Comms, 16 | 1);
                    break;
                case TaskTypes.FixLights:
                    Rpc<FixLightsRpc>.Instance.Send(data: true, immediately: true);
                    break;
                case TaskTypes.RestoreOxy:
                    ShipStatus.Instance.RpcRepairSystem(SystemTypes.LifeSupp, 0 | 64);
                    ShipStatus.Instance.RpcRepairSystem(SystemTypes.LifeSupp, 1 | 64);
                    break;
                case TaskTypes.StopCharles:
                    ShipStatus.Instance.RpcRepairSystem(SystemTypes.Reactor, 16);
                    ShipStatus.Instance.RpcRepairSystem(SystemTypes.Reactor, 17);
                    break;
                case TaskTypes.ResetReactor:
                    ShipStatus.Instance.RpcRepairSystem(SystemTypes.Reactor, 16);
                    break;

                case TaskTypes.ResetSeismic:
                    ShipStatus.Instance.RpcRepairSystem(SystemTypes.Laboratory, 16);
                    break;
                default:
                    return;
            }
            PlayerControl.LocalPlayer.GetPlayerData().UsedAbility = true;
            SoundManager.Instance.PlaySound(PopeyesRolesModPlugin.Assets.ElectricScrewDriver, false, 200f);
        }
    }
    [HarmonyPatch(typeof(ShipStatus), nameof(ShipStatus.RpcRepairSystem))]
    public class SabotagePatch
    {
        public static void Postfix(SystemTypes __0, int __1)
        {
            System.Console.WriteLine(__0.ToString() + __1);
        }
    }
}
