using HarmonyLib;
using PopeyesRolesMod.Roles.Rpc;
using Reactor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PopeyesRolesMod.Roles
{
    [HarmonyPatch(typeof(PlayerControl), nameof(PlayerControl.RpcSetInfected))]
    public static class SetInfectedPatch
    {

        public static void Postfix()
        {
            var cfg = PluginSingleton<PopeyesRolesModPlugin>.Instance.CreateConfig();
            Dictionary<Role, (Team Team, float SpawnChance)> roles = new Dictionary<Role, (Team Team, float SpawnChance)>()
            {
                { Role.Hunter, (Team.Crewmate, SpawnChance: cfg.HunterSpawnChance) },
                { Role.Jester, (Team.Neutral, SpawnChance: cfg.JesterSpawnChance) },
                { Role.Detective, (Team.Crewmate, SpawnChance: cfg.DetectiveSpawnChance)},
                { Role.Engineer, (Team.Crewmate, SpawnChance: cfg.EngineerSpawnChance) },
                { Role.ShapeShifter, (Team.Impostor, SpawnChance: cfg.ShapeShifterSpawnChance)}
             };

            var data = new InitializeRoundData();

            var rc = roles.Count;
            for (int i = 0; i < rc; i++)
            {
                var role = roles.ElementAt(PopeyesRolesModPlugin.Random.Next(0, rc - i));
                roles.Remove(role.Key);


                if ((PopeyesRolesModPlugin.Random.Next(0, 1000) / 10) > role.Value.SpawnChance)
                    continue;

                if (role.Value.Team == Team.Impostor)
                {
                    var impostors = PlayerDataManager.GetImpostors().Where(x => !data.Roles.ContainsKey(x.PlayerId));
                    if (impostors.Any())
                    {
                        var winner = impostors.ElementAt(PopeyesRolesModPlugin.Random.Next(0, impostors.Count()));
                        data.Roles[winner.PlayerId] = role.Key;
                    }
                }
                else
                {
                    var crewmates = PlayerDataManager.GetCrewmates().Where(x => !data.Roles.ContainsKey(x.PlayerId));
                    if (crewmates.Any())
                    {
                        var winner = crewmates.ElementAt(PopeyesRolesModPlugin.Random.Next(0, crewmates.Count()));
                        data.Roles[winner.PlayerId] = role.Key;
                    }
                }
            }
            foreach (var player in PlayerControl.AllPlayerControls.ToArray().Where(x => !data.Roles.ContainsKey(x.PlayerId)))
            {
                if (player.Data.IsImpostor)
                {
                    data.Roles[player.PlayerId] = Role.Impostor;
                }
                else
                {
                    data.Roles[player.PlayerId] = Role.Crewmate;
                }
            }
            Rpc<InitializeRoundRpc>.Instance.Send(data, immediately: true);

        }
    }
}
