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
            Roles.Medic.ShieldButton.CreateButton();
            Roles.Morphling.SampleButton.CreateButton();
            Roles.Morphling.MorphButton.CreateButton();
            Roles.Sheriff.ShootButton.CreateButton();
        }
    }
}
