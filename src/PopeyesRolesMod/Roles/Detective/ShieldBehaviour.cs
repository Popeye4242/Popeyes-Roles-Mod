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
        Color detectiveShieldColor = Colors.DetectiveShieldColor;

        public ShieldBehaviour(IntPtr value) : base(value)
        {
        }


        public void Update()
        {
            SetShieldColor(detectiveShieldColor);
        }

        [HideFromIl2Cpp]
        private void SetShieldColor(Color color)
        {
            var myRend = gameObject.GetComponent<SpriteRenderer>();
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

        public float speed = 1.0f;
        public Color startColor = Colors.DetectiveShieldColor;
        public Color endColor = Palette.ImpostorRed;
        float startTime;


        [HideFromIl2Cpp]
        public IEnumerator DestroyShield()
        {
            startTime = Time.time;
            detectiveShieldColor = Palette.ImpostorRed;
            for (int i = 0; i < 50; i++)
            {
                strength += .01f;

                float t = (Time.time - startTime) * speed;
                detectiveShieldColor = Color.Lerp(startColor, endColor, t);
                yield return new WaitForSeconds(.005f);
            }
            strength = 0f;
            yield return new WaitForSeconds(.01f);
            Destroy(this);
        }

        internal void Stop()
        {
            SoundManager.Instance.PlaySound(PopeyesRolesModPlugin.Assets.ShieldDisarm, false, 100f);
            Coroutines.Start(DestroyShield());
        }
    }
}
