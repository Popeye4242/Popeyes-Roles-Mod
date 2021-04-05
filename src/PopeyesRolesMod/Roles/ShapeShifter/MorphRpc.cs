using Hazel;
using Newtonsoft.Json;
using Reactor;
using Reactor.Networking;

namespace PopeyesRolesMod.Roles.ShapeShifter
{
    [RegisterCustomRpc(73)]
    public class MorphRpc : PlayerCustomRpc<PopeyesRolesModPlugin, MorphData>
    {
        public MorphRpc(PopeyesRolesModPlugin plugin, uint id) : base(plugin, id)
        {
        }

        public override RpcLocalHandling LocalHandling => RpcLocalHandling.Before;

        public override void Handle(PlayerControl innerNetObject, MorphData data)
        {
            if (data.Morph)
            {
                var morphling = PlayerDataManager.GetPlayerById(data.ShapeShifter);
                var behaviour = morphling.gameObject.AddComponent<MorphBehaviour>();
                behaviour.Player = morphling;
                behaviour.SampledPlayer = PlayerDataManager.GetPlayerById(data.SampledPlayer); 
            }
            else
            {
                var morphling = PlayerDataManager.GetPlayerById(data.ShapeShifter);
                var behaviour = morphling.gameObject.GetComponent<MorphBehaviour>();
                behaviour.Stop();
            }
        }

        public override MorphData Read(MessageReader reader)
        {
            return JsonConvert.DeserializeObject<MorphData>(reader.ReadString());
        }

        public override void Write(MessageWriter writer, MorphData data)
        {
            writer.Write(JsonConvert.SerializeObject(data));
        }
    }
}
