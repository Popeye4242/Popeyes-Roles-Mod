using System;
using System.Collections.Generic;
using System.Text;

namespace PopeyesRolesMod.Roles.Morphling
{
    [Serializable]
    public class MorphData
    {
        public bool Morph { get; set; }
        public byte SampledPlayer { get; set; }
        public byte Morphling { get; set; }
    }
}
