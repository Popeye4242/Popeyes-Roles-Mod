using Hazel;
using Reactor;
using Reactor.Networking;

namespace PopeyesRolesMod.Roles.Jester
{
    [RegisterCustomRpc(74)]
    public class JesterWinRpc : PlayerCustomRpc<PopeyesRolesModPlugin, bool>
    {
        public JesterWinRpc(PopeyesRolesModPlugin plugin, uint id) : base(plugin, id)
        {

        }

        public override RpcLocalHandling LocalHandling => RpcLocalHandling.Before;

        public override void Handle(PlayerControl innerNetObject, bool data)
        {
            foreach (var player in PlayerControl.AllPlayerControls)
            {
                if (player.HasPlayerRole(Role.Jester))
                {
                    player.Revive();
                    player.Data.IsDead = false;
                    player.Data.IsImpostor = true;
                }
                else
                {

                    player.RemoveInfected();
                    player.Die(DeathReason.Exile);
                    player.Data.IsDead = true;
                    player.Data.IsImpostor = false;
                }
            }
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
