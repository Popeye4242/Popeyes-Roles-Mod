using System;
using System.Collections.Generic;
using System.Text;

namespace PopeyesRolesMod
{
    public class LobbyConfig
    {
        #region Hunter
        public float HunterSpawnChance { get; set; }
        public float HunterKillCooldown { get; set; }
        #endregion

        #region Engineer
        public float EngineerSpawnChance { get; set; }
        #endregion

        #region Shape Shifter
        public float ShapeShifterSpawnChance { get; set; }
        public float ShapeShifterSampleCooldown { get; set; }
        public float ShapeShifterMorphCooldown { get; set; }
        public float ShapeShifterMorphDuration { get; set; }
        #endregion

        #region Jester
        public float JesterSpawnChance { get; set; }
        #endregion

        #region Detective
        public float DetectiveSpawnChance { get; set; }
        public bool DetectiveShieldedPlayerSeesShield { get; set; }
        public float DetectiveDeathReportThreshold { get; set; }
        #endregion
    }
}
