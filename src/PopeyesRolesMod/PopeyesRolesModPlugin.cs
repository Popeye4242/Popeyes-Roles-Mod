using BepInEx;
using BepInEx.Configuration;
using BepInEx.IL2CPP;
using Essentials;
using Essentials.Options;
using HarmonyLib;
using Newtonsoft.Json;
using Reactor;
using Reactor.Patches;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;

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

        public ConfigEntry<bool> Enabled { get; set; }
        public ConfigEntry<string> Ip { get; set; }
        public ConfigEntry<ushort> Port { get; set; }

        public Harmony Harmony { get; }

        public PopeyesRolesModPlugin()
        {
            Harmony = new Harmony(Id);
        }


        public override void Load()
        {
            Enabled = Config.Bind("Server", "Enable custom Server", true);
            Ip = Config.Bind("Server", "Ipv4 or Hostname", "31.3.2021.popmod.net");
            Port = Config.Bind("Server", "Port", (ushort)22023);

            LoadAssets();
            CreateOptions();



            ReactorVersionShower.TextUpdated += (TextRenderer renderer) =>
            {
                var version = Assembly.GetExecutingAssembly().GetName().Version.ToString(3);
                renderer.Text += Environment.NewLine + "Popeyes Roles Mod v" + version;
                Coroutines.Start(CheckForUpdates(renderer, version));
            };

            CustomOption.ShamelessPlug = false;
            RegisterInIl2CppAttribute.Register();
            RegisterCustomRpcAttribute.Register(this);

            if (Enabled.Value) 
                AddCustomRegion();

            Harmony.PatchAll();
        }

        private void AddCustomRegion()
        {
            ServerManager serverManager = DestroyableSingleton<ServerManager>.Instance;

            var regions = new List<IRegionInfo>(ServerManager.DefaultRegions);
            // regions.Add(
            //     new DnsRegionInfo(
            //             Ip.Value, "Popeyes Server", StringNames.NoTranslation, Ip.Value, Port.Value)
            //         .Cast<IRegionInfo>());

            ServerManager.DefaultRegions = regions.ToArray();
            serverManager.AvailableRegions = regions.ToArray();
            serverManager.SaveServers();
        }

        private IEnumerator CheckForUpdates(TextRenderer renderer, string version)
        {
            try
            {

                using WebClient client = new WebClient();
                client.Headers.Add("user-agent", "Popeye4242/Popeyes-Roles-Mod");
                string build = client.DownloadString(latestRelease);
                var githubRelease = JsonConvert.DeserializeObject<GithubRelease>(build);
                var tagName = githubRelease.Tag_name;
                string githubVersion = Regex.Match(tagName, "(\\d+\\.\\d+\\.\\d+)").Value;
                var parsedGithubVersionVersion = Version.Parse(githubVersion);
                var parsedAssemblyVersion = Version.Parse(version);
                if (parsedGithubVersionVersion > parsedAssemblyVersion)
                {
                    renderer.Text += " ([FF1111FF]Update available[])";
                }
                else if (parsedAssemblyVersion > parsedGithubVersionVersion)
                {
                    renderer.Text += " ([FF1111FF]DEV BUILD[])";
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
