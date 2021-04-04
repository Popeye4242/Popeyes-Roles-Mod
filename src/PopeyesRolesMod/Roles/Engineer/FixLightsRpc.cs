using Hazel;
using Reactor;
using Reactor.Networking;

namespace PopeyesRolesMod.Roles.Engineer
{

    [RegisterCustomRpc(71)]
    public class FixLightsRpc : PlayerCustomRpc<PopeyesRolesModPlugin, bool>
    {
        public FixLightsRpc(PopeyesRolesModPlugin plugin, uint id) : base(plugin, id)
        {

        }

        public override RpcLocalHandling LocalHandling => RpcLocalHandling.Before;

        public override void Handle(PlayerControl innerNetObject, bool data)
        {
            var switchSystem = ShipStatus.Instance.Systems[SystemTypes.Electrical].Cast<SwitchSystem>();
            switchSystem.ActualSwitches = switchSystem.ExpectedSwitches;
        }

        public override bool Read(MessageReader reader)
        {
            return reader.ReadBoolean();
        }

        public override void Write(MessageWriter writer, bool data)
        {
            writer.Write(data);
        }
    }
}
