using BepInEx;
using BepInEx.IL2CPP;
using Essentials.Options;
using HarmonyLib;
using Reactor;
using Reactor.Patches;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace PopeyesRolesMod
{
    [BepInPlugin(Id)]
    [BepInProcess("Among Us.exe")]
    [BepInDependency(ReactorPlugin.Id)]
    public partial class PopeyesRolesModPlugin : BasePlugin
    {
        public static Random Random = new Random();
        public const string Id = "dev.kynet.popeyesrolesmod";

        public Harmony Harmony { get; }

        public PopeyesRolesModPlugin()
        {
            Harmony = new Harmony(Id);
        }


        public override void Load()
        {
            LoadAssets();
            CreateOptions();

            ReactorVersionShower.TextUpdated += (TextRenderer renderer) =>
            {
                renderer.Text += Environment.NewLine + "Popeyes Roles Mod v"+Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
            };

            CustomOption.ShamelessPlug = false;
            RegisterInIl2CppAttribute.Register();
            RegisterCustomRpcAttribute.Register(this);
            
            Harmony.PatchAll();
        }
    }
}
