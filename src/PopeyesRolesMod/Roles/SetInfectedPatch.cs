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
            Dictionary<Role, Team> roles = new Dictionary<Role, Team>()
            {
                { Role.Sheriff, Team.Crewmate },
                { Role.Jester, Team.Neutral },
                { Role.Medic, Team.Crewmate},
                { Role.Engineer, Team.Crewmate },
                //{ Role.Morphling, Team.Impostor}
            };

            var data = new InitializeRoundData();

            foreach (var role in roles)
            {
                if (role.Value == Team.Impostor)
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
