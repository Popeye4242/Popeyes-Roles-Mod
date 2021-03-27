using Hazel;
using PopeyesRolesMod.Utility;
using Reactor;
using System;
using System.Collections.Generic;
using System.Text;

namespace PopeyesRolesMod.Roles.Rpc
{
    [RegisterCustomRpc]
    public class InitializeRoundRpc : PlayerCustomRpc<PopeyesRolesModPlugin, InitializeRoundData>
    {
        public InitializeRoundRpc(PopeyesRolesModPlugin plugin) : base(plugin)
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
                PlayerDataManager.SetPlayerRole(role.Key, role.Value);
            }
        }

        public override InitializeRoundData Read(MessageReader reader)
        {
            return BinarySerializer.Deserialize<InitializeRoundData>(reader.ReadBytesAndSize());
        }

        public override void Write(MessageWriter writer, InitializeRoundData data)
        {
            writer.WriteBytesAndSize(BinarySerializer.Serialize(data));
        }
    }
}
