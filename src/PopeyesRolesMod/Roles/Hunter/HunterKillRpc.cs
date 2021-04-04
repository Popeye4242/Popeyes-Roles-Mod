using Hazel;
using Reactor;
using Reactor.Networking;

namespace PopeyesRolesMod.Roles.Hunter
{

    [RegisterCustomRpc(72)]
    public class HunterKillRpc : PlayerCustomRpc<PopeyesRolesModPlugin, HunterKillRpc.OfficerKillData>
    {
        public HunterKillRpc(PopeyesRolesModPlugin plugin, uint id) : base(plugin, id)
        {

        }

        public override RpcLocalHandling LocalHandling => RpcLocalHandling.Before;

        public override void Handle(PlayerControl innerNetObject, OfficerKillData data)
        {
            var attacker = PlayerDataManager.GetPlayerById(data.Attacker);

            var target = PlayerDataManager.GetPlayerById(data.Target);

            attacker.MurderPlayer(target);
        }

        public override OfficerKillData Read(MessageReader reader)
        {
            return new OfficerKillData(attacker: reader.ReadByte(), target: reader.ReadByte());
        }

        public override void Write(MessageWriter writer, OfficerKillData data)
        {
            writer.Write(data.Attacker);
            writer.Write(data.Target);
        }
        public struct OfficerKillData
        {
            public OfficerKillData(byte attacker, byte target)
            {
                Attacker = attacker;
                Target = target;
            }

            public byte Attacker { get; }
            public byte Target { get; }
            public static implicit operator OfficerKillData((PlayerControl Attacker, PlayerControl Target) data) => new OfficerKillData(data.Attacker.PlayerId, data.Target.PlayerId);
        }
    }
}
