using System;
using System.Collections.Generic;
using System.Text;

namespace PopeyesRolesMod.Roles
{
    public class PlayerData
    {
        public Role Role { get; set; }
        public PlayerControl SampledPlayer { get; set; }
        public bool UsedAbility { get; internal set; }
    }
}
