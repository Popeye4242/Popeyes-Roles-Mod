using System;
using System.Collections.Generic;

namespace PopeyesRolesMod.Roles.Detective
{
    public class DeadPlayer
    {
        public PlayerControl Player { get; set; }
        public PlayerControl Murderer { get; set; }
        public List<string> DeathDetails { get; } = new List<string>();
        public DateTime Timestamp { get; } = DateTime.UtcNow;
    }
}
