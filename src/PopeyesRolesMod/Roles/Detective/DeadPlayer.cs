using System;
using System.Collections.Generic;
using System.Text;

namespace PopeyesRolesMod.Roles.Detective
{
    public class DeadPlayer
    {
        public PlayerControl Player { get; set; }
        public bool Suicide { get; set; }
        public bool WasKilledByShapeShifter { get; set; }
        public bool DidMurdererVent { get; set; }
        public PlayerControl Murderer { get; internal set; }
    }
}
