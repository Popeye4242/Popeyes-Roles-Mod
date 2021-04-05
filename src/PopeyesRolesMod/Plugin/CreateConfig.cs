using Essentials.Options;

namespace PopeyesRolesMod
{
    public partial class PopeyesRolesModPlugin
    {
        #region Hunter
        public CustomNumberOption HunterSpawnChance { get; private set; }
        public CustomNumberOption HunterKillCooldown { get; private set; }
        #endregion

        #region Engineer
        public CustomNumberOption EngineerSpawnChance { get; private set; }
        #endregion

        #region Shape Shifter
        public CustomNumberOption ShapeShifterSpawnChance { get; private set; }
        public CustomNumberOption ShapeShifterSampleCooldown { get; private set; }
        public CustomNumberOption ShapeShifterMorphCooldown { get; private set; }
        public CustomNumberOption ShapeShifterMorphDuration { get; private set; }
        #endregion

        #region Jester
        public CustomNumberOption JesterSpawnChance { get; private set; }
        #endregion

        #region Detective
        public CustomNumberOption DetectiveSpawnChance { get; private set; }
        public CustomToggleOption DetectiveShieldedPlayerSeesShield { get; private set; }
        public CustomNumberOption DetectiveDeathReportThreshold { get; private set; }
        #endregion


        public void CreateOptions()
        {
            CustomOption.AddHeader(Properties.Resources.HunterOptions);
            HunterSpawnChance = CustomOption.AddNumber("hunter-spawn-chance", Properties.Resources.SpawnChance, saveValue: true, 100, 0, 100, 10);
            HunterKillCooldown = CustomOption.AddNumber("hunter-kill-cooldown", Properties.Resources.HunterKillCooldown, saveValue: true, 30, 10, 60, 2.5f);

            CustomOption.AddHeader(Properties.Resources.EngineerOptions);
            EngineerSpawnChance = CustomOption.AddNumber("engineer-spawn-chance", Properties.Resources.SpawnChance, saveValue: true, 80, 0, 100, 10);

            CustomOption.AddHeader(Properties.Resources.ShapeShifterOptions);
            ShapeShifterSpawnChance = CustomOption.AddNumber("shape-shifter-spawn-chance", Properties.Resources.SpawnChance, saveValue: true, 80, 0, 100, 10);
            ShapeShifterSampleCooldown = CustomOption.AddNumber("shape-shifter-sample-cooldown", Properties.Resources.ShapeShifterSampleCooldown, saveValue: true, 0, 0, 30, 2.5f);
            ShapeShifterMorphCooldown = CustomOption.AddNumber("shape-shifter-morph-cooldown", Properties.Resources.ShapeShifterMorphCooldown, saveValue: true, 20, 0, 30, 2.5f);
            ShapeShifterMorphDuration = CustomOption.AddNumber("shape-shifter-morph-duration", Properties.Resources.ShapeShifterMorphDuration, saveValue: true, 10, 0, 30, 2.5f);

            CustomOption.AddHeader(Properties.Resources.JesterOptions);
            JesterSpawnChance = CustomOption.AddNumber("jester-spawn-chance", Properties.Resources.SpawnChance, saveValue: true, 80, 0, 100, 10);

            CustomOption.AddHeader(Properties.Resources.DetectiveOptions);
            DetectiveSpawnChance = CustomOption.AddNumber("detective-spawn-chance", Properties.Resources.SpawnChance, saveValue: true, 80, 0, 100, 10);
            DetectiveShieldedPlayerSeesShield = CustomOption.AddToggle("detective-shielded-player-sees-shield", Properties.Resources.ShieldedPlayerSeesShield, saveValue: true, true);
            //DetectiveDeathReportThreshold = CustomOption.AddNumber("detective-death-report-threshold", Properties.Resources.DetectiveDeathReportThreshold, saveValue: true, 20, 0, 30, 2.5f);

        }
        public LobbyConfig CreateConfig()
        {
            return new LobbyConfig
            {
                HunterSpawnChance = HunterSpawnChance.GetValue(),
                HunterKillCooldown = HunterKillCooldown.GetValue(),
                EngineerSpawnChance = EngineerSpawnChance.GetValue(),
                ShapeShifterSpawnChance = ShapeShifterSpawnChance.GetValue(),
                ShapeShifterSampleCooldown = ShapeShifterSampleCooldown.GetValue(),
                ShapeShifterMorphCooldown = ShapeShifterMorphCooldown.GetValue(),
                ShapeShifterMorphDuration = ShapeShifterMorphDuration.GetValue(),
                JesterSpawnChance = JesterSpawnChance.GetValue(),
                DetectiveSpawnChance = DetectiveSpawnChance.GetValue(),
                DetectiveShieldedPlayerSeesShield = DetectiveShieldedPlayerSeesShield.GetValue(),
                DetectiveDeathReportThreshold = 20
            };
        }
    }
}
