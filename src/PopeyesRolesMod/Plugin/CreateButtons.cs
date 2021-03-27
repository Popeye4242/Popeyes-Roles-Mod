using System;
using System.Collections.Generic;
using System.Text;

namespace PopeyesRolesMod
{
    public partial class PopeyesRolesModPlugin
    {
        private void CreateButtons()
        {
            Roles.Engineer.RepairButton.CreateButton();
            Roles.Detective.ShieldButton.CreateButton();
            Roles.ShapeShifter.SampleButton.CreateButton();
            Roles.ShapeShifter.MorphButton.CreateButton();
            Roles.Hunter.ShootButton.CreateButton();
        }
    }
}
