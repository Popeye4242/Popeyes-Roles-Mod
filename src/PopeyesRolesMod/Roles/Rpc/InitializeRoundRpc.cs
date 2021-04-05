using Hazel;
using Newtonsoft.Json;
using Reactor;
using Reactor.Networking;
using System;
using System.Collections.Generic;
using System.Text;

namespace PopeyesRolesMod.Roles.Rpc
{
    [RegisterCustomRpc(75)]
    public class InitializeRoundRpc : PlayerCustomRpc<PopeyesRolesModPlugin, InitializeRoundData>
    {
        public InitializeRoundRpc(PopeyesRolesModPlugin plugin, uint id) : base(plugin, id)
        {

        }

        public override RpcLocalHandling LocalHandling => RpcLocalHandling.Before;

        public override void Handle(PlayerControl innerNetObject, InitializeRoundData data)
        {
            PlayerDataManager.Instance = new PlayerDataManager();
            PlayerDataManager.Instance.Config = PluginSingleton<PopeyesRolesModPlugin>.Instance.CreateConfig();

            Engineer.RepairButton.CreateButton();
            Detective.ShieldButton.CreateButton();
            ShapeShifter.SampleButton.CreateButton();
            ShapeShifter.MorphButton.CreateButton();
            Hunter.ShootButton.CreateButton();

            foreach (var role in data.Roles)
            {
                System.Console.WriteLine("Assigned {0} for {1}", role.Value, PlayerDataManager.GetPlayerById(role.Key).name);
                PlayerDataManager.SetPlayerRole(role.Key, role.Value);
            }
        }

        public override InitializeRoundData Read(MessageReader reader)
        {
            return JsonConvert.DeserializeObject<InitializeRoundData>(reader.ReadString());
        }

        public override void Write(MessageWriter writer, InitializeRoundData data)
        {
            writer.Write(JsonConvert.SerializeObject(data));
        }
    }
}
