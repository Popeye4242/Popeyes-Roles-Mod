using Reactor.Extensions;
using UnityEngine;

namespace PopeyesRolesMod
{
    public partial class PopeyesRolesModPlugin
    {
        private const string TOOLS = "tools";
        private const string MEDIC_SHIELD = "medic-shield";
        private const string DISGUISE = "disguise";
        private const string SAMPLING = "sampling";
        private const string GUN = "gun";
        private const string PLACEHOLDER = "placeholder_text";
        private const string SETTINGS = "settings_text";
        private const string TROPHY = "trophy_text";
        private const string HEART = "heart_text";
        private const string GAME_OVER = "game-over";
        private const string SHIELD_DISARM = "322148__liamg-sfx__shield-disarm-1";
        private const string SHIELD_GUARD = "370203__nekoninja__shield-guard";
        private const string ELECTRIC_SCREW_DRIVER = "81098__lg__electric-screwdriver-04";
        private const string SUCK_POP = "81152__joedeshon__suck-pop-03";
        private const string SWOOSH = "444629__audiopapkin__swoosh-18";

        public static Assets Assets { get; set; }

        private void LoadAssets()
        {
            Assets = new Assets();
            Assets.AssetBundle = AssetBundle.LoadFromMemory(Properties.Resources.popeyes_roles_mod);
            Assets.Tools = Assets.AssetBundle.LoadAsset<Sprite>(TOOLS).DontUnload();
            Assets.MedicShield = Assets.AssetBundle.LoadAsset<Sprite>(MEDIC_SHIELD).DontUnload();
            Assets.Disguise = Assets.AssetBundle.LoadAsset<Sprite>(DISGUISE).DontUnload();
            Assets.Sampling = Assets.AssetBundle.LoadAsset<Sprite>(SAMPLING).DontUnload();
            Assets.Gun = Assets.AssetBundle.LoadAsset<Sprite>(GUN).DontUnload();
            Assets.PlaceHolder = Assets.AssetBundle.LoadAsset<Sprite>(PLACEHOLDER).DontUnload();
            Assets.Settings = Assets.AssetBundle.LoadAsset<Sprite>(SETTINGS).DontUnload();
            Assets.Trophy = Assets.AssetBundle.LoadAsset<Sprite>(TROPHY).DontUnload();
            Assets.Heart = Assets.AssetBundle.LoadAsset<Sprite>(HEART).DontUnload();
            Assets.GameOver = Assets.AssetBundle.LoadAsset<Sprite>(GAME_OVER).DontUnload();
            Assets.ShieldDisarm = Assets.AssetBundle.LoadAsset<AudioClip>(SHIELD_DISARM).DontUnload();
            Assets.ShieldGuard = Assets.AssetBundle.LoadAsset<AudioClip>(SHIELD_GUARD).DontUnload();
            Assets.ElectricScrewDriver = Assets.AssetBundle.LoadAsset<AudioClip>(ELECTRIC_SCREW_DRIVER).DontUnload();
            Assets.SuckPop = Assets.AssetBundle.LoadAsset<AudioClip>(SUCK_POP).DontUnload();
            Assets.Swoosh = Assets.AssetBundle.LoadAsset<AudioClip>(SWOOSH).DontUnload();
        }
    }
}
