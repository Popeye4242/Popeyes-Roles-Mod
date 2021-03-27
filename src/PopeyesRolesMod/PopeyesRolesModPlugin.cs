using BepInEx;
using BepInEx.IL2CPP;
using Essentials;
using Essentials.Options;
using HarmonyLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Reactor;
using Reactor.Patches;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PopeyesRolesMod
{
    [BepInPlugin(Id)]
    [BepInProcess("Among Us.exe")]
    [BepInDependency(EssentialsPlugin.Id)]
    public partial class PopeyesRolesModPlugin : BasePlugin
    {
        public static Random Random = new Random();
        public const string Id = "net.popmod";
        private const string latestRelease = "https://api.github.com/repos/popeye4242/popeyes-roles-mod/releases/latest";

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
                var version = "v" + Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
                renderer.Text += Environment.NewLine + "Popeyes Roles Mod " + version;
                Coroutines.Start(CheckForUpdates(renderer, version));
            };

            CustomOption.ShamelessPlug = false;
            RegisterInIl2CppAttribute.Register();
            RegisterCustomRpcAttribute.Register(this);

            Harmony.PatchAll();
        }

        private IEnumerator CheckForUpdates(TextRenderer renderer, string version)
        {
            try
            {

                using (WebClient client = new WebClient())
                {
                    client.Headers.Add("user-agent", "Popeye4242/Popeyes-Roles-Mod");
                    string build = client.DownloadString(latestRelease);
                    var parsedVersion = JObject.Parse(build);
                    var tagName = parsedVersion["tag_name"];
                    if (!string.Equals(version, tagName.ToString()))
                    {
                        renderer.Text += " ([FF1111FF]Update available[])";
                    }
                }
            }
            catch (WebException ex)
            {
                Log.LogError(string.Format("Failed to retrieve current mod version: {0}", ex.Message));
            }
            yield return null;
        }
    }
}
