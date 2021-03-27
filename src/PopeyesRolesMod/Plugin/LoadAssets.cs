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
            Assets.DetectiveShieldButton = Assets.AssetBundle.LoadAsset<Sprite>("medic-shield").DontUnload();
            Assets.ShapeShifterMorphButton = Assets.AssetBundle.LoadAsset<Sprite>("disguise").DontUnload();
            Assets.ShapeShifterSampleButton = Assets.AssetBundle.LoadAsset<Sprite>("sampling").DontUnload();
            Assets.HunterKillButton= Assets.AssetBundle.LoadAsset<Sprite>("gun").DontUnload();
        }
    }
}
