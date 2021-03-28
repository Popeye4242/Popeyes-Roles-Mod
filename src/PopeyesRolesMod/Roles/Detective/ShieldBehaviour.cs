using Reactor;
using System;
using System.Collections;
using UnhollowerBaseLib.Attributes;
using UnityEngine;

namespace PopeyesRolesMod.Roles.Detective
{
    [RegisterInIl2Cpp]
    public class ShieldBehaviour : MonoBehaviour
    {
        float strength = 1f;
        public ShieldBehaviour(IntPtr value) : base(value)
        {
        }


        public void Update()
        {
            SetShieldColor(Colors.DetectiveShieldColor);
        }

        [HideFromIl2Cpp]
        private void SetShieldColor(Color color)
        {
            var myRend = gameObject.GetComponent<SpriteRenderer>();
            myRend.material.SetColor("_VisorColor", color);
            myRend.material.SetFloat("_Outline", strength);
            myRend.material.SetColor("_OutlineColor", color);
        }

        internal void GlowShield()
        {
            Coroutines.Start(MakeGlow());
            SoundManager.Instance.PlaySound(PopeyesRolesModPlugin.Assets.ShieldDisarm, false, 100f);
        }

        [HideFromIl2Cpp]
        public IEnumerator MakeGlow()
        {
            for (int i = 0; i < 10; i++)
            {
                strength -= .1f;
                yield return new WaitForSeconds(.05f);
            }
            for (int i = 0; i < 10; i++)
            {
                strength += .1f;
                yield return new WaitForSeconds(.05f);
            }
        }
    }
}
