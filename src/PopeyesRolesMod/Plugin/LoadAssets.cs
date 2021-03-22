using Reactor.Extensions;
using UnityEngine;

namespace PopeyesRolesMod
{
    public partial class PopeyesRolesModPlugin
    {
        public static Assets Assets { get; set; }

        private void LoadAssets()
        {
            Assets = new Assets();
            Assets.AssetBundle = AssetBundle.LoadFromMemory(Properties.Resources.popeyes_roles_mod);
            Assets.EngineerRepairButton = Assets.AssetBundle.LoadAsset<Sprite>("tools").DontUnload();
            Assets.MedicShieldButton = Assets.AssetBundle.LoadAsset<Sprite>("medic-shield").DontUnload();
            Assets.MorphlingMorphButton = Assets.AssetBundle.LoadAsset<Sprite>("disguise").DontUnload();
            Assets.MorphlingSampleButton = Assets.AssetBundle.LoadAsset<Sprite>("sampling").DontUnload();
            Assets.SheriffKillButton= Assets.AssetBundle.LoadAsset<Sprite>("gun").DontUnload();
        }
    }
}
