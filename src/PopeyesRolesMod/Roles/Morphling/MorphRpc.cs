using Hazel;
using PopeyesRolesMod.Utility;
using Reactor;
using System;
using System.Collections.Generic;
using System.Text;

namespace PopeyesRolesMod.Roles.Morphling
{
    [RegisterCustomRpc]
    public class MorphRpc : PlayerCustomRpc<PopeyesRolesModPlugin, MorphData>
    {
        public MorphRpc(PopeyesRolesModPlugin plugin) : base(plugin)
        {
        }

        public override RpcLocalHandling LocalHandling => RpcLocalHandling.Before;

        public override void Handle(PlayerControl innerNetObject, MorphData data)
        {
            if (data.Morph)
            {
                var morphling = PlayerDataManager.GetPlayerById(data.Morphling);
                var behaviour = morphling.gameObject.AddComponent<MorphBehaviour>();
                behaviour.Player = morphling;
                behaviour.SampledPlayer = PlayerDataManager.GetPlayerById(data.SampledPlayer);
            }
            else
            {
                var morphling = PlayerDataManager.GetPlayerById(data.Morphling);
                var behaviour = morphling.gameObject.GetComponent<MorphBehaviour>();
                behaviour.Stop();
            }
        }

        public override MorphData Read(MessageReader reader)
        {
            return BinarySerializer.Deserialize<MorphData>(reader.ReadBytesAndSize());
        }

        public override void Write(MessageWriter writer, MorphData data)
        {
            writer.WriteBytesAndSize(BinarySerializer.Serialize(data));
        }
    }
}
