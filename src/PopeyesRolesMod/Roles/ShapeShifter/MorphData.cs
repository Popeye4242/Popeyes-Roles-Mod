using System;
using System.Collections.Generic;
using System.Text;

namespace PopeyesRolesMod.Roles.ShapeShifter
{
    [Serializable]
    public class MorphData
    {
        public bool Morph { get; set; }
        public byte SampledPlayer { get; set; }
        public byte ShapeShifter { get; set; }
    }
}
