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
        private static Random s_rand = new Random();

        public static void Postfix()
        {
            Dictionary<Role, (Team Team, int SpawnChance)> roles = new Dictionary<Role, (Team Team, int SpawnChance)>()
            {
                { Role.Sheriff, (Team.Crewmate, SpawnChance: 100) },
                { Role.Jester, (Team.Neutral, SpawnChance: 100) },
                { Role.Medic, (Team.Crewmate, SpawnChance: 0)},
                { Role.Engineer, (Team.Crewmate, SpawnChance: 0) },
                { Role.Morphling, (Team.Impostor, SpawnChance: 0)}
             };

            var data = new InitializeRoundData();

            for (int i = 0; i < roles.Count; i++)
            {
                var role = roles.ElementAt(s_rand.Next(0, roles.Count - i));
                roles.Remove(role.Key);
                
                if (s_rand.Next(0, 100) > role.Value.SpawnChance)
                    continue;

                if (role.Value.Team == Team.Impostor)
                {
                    var impostors = PlayerDataManager.GetImpostors().Where(x => !data.Roles.ContainsKey(x.PlayerId));
                    if (impostors.Any())
                    {
                        var winner = impostors.ElementAt(s_rand.Next(0, impostors.Count()));
                        data.Roles[winner.PlayerId] = role.Key;
                    }
                }
                else
                {
                    var crewmates = PlayerDataManager.GetCrewmates().Where(x => !data.Roles.ContainsKey(x.PlayerId));
                    if (crewmates.Any())
                    {
                        var winner = crewmates.ElementAt(s_rand.Next(0, crewmates.Count()));
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
