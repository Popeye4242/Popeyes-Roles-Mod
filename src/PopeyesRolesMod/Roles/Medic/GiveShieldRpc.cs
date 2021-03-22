using Hazel;
using Reactor;

namespace PopeyesRolesMod.Roles.Medic
{
    [RegisterCustomRpc]
    public class GiveShieldRpc : PlayerCustomRpc<PopeyesRolesModPlugin, byte>
    {
        public GiveShieldRpc(PopeyesRolesModPlugin plugin) : base(plugin)
        {

        }

        public override RpcLocalHandling LocalHandling => RpcLocalHandling.Before;

        public override void Handle(PlayerControl innerNetObject, byte protectedId)
        {
            var shieldedPlayer = PlayerDataManager.GetPlayerById(protectedId);
            PlayerDataManager.ShieldedPlayer = shieldedPlayer;
            if (shieldedPlayer.AmOwner || PlayerControl.LocalPlayer.HasPlayerRole(Role.Medic))
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
