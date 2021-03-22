using System;
using System.Collections.Generic;
using System.Text;

namespace PopeyesRolesMod.Roles.Rpc
{
    [Serializable]
    public class InitializeRoundData
    {
        public InitializeRoundData()
        {
        }

        public Dictionary<byte, Role> Roles { get; set; } = new Dictionary<byte, Role>();
    }
}
