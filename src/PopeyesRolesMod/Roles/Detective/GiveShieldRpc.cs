using Hazel;
using Reactor;
using Reactor.Networking;

namespace PopeyesRolesMod.Roles.Detective
{
    [RegisterCustomRpc(70)]
    public class GiveShieldRpc : PlayerCustomRpc<PopeyesRolesModPlugin, byte>
    {
        public GiveShieldRpc(PopeyesRolesModPlugin plugin, uint id) : base(plugin, id)
        {

        }

        public override RpcLocalHandling LocalHandling => RpcLocalHandling.Before;

        public override void Handle(PlayerControl innerNetObject, byte protectedId)
        {
            var shieldedPlayer = PlayerDataManager.GetPlayerById(protectedId);
            PlayerDataManager.Instance.ShieldedPlayer = shieldedPlayer;
            if ((shieldedPlayer.AmOwner && PlayerDataManager.Instance.Config.DetectiveShieldedPlayerSeesShield) || PlayerControl.LocalPlayer.HasPlayerRole(Role.Detective))
            {
                shieldedPlayer.gameObject.AddComponent<ShieldBehaviour>();
            }
        }

        public override byte Read(MessageReader reader)
        {
            return reader.ReadByte();
        }

        public override void Write(MessageWriter writer, byte data)
        {
            writer.Write(data);
        }
    }
}
