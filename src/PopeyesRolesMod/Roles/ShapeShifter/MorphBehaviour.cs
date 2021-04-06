using Reactor;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace PopeyesRolesMod.Roles.ShapeShifter
{
    [RegisterInIl2Cpp]
    public class MorphBehaviour : MonoBehaviour
    {
        private static readonly int BodyColor = Shader.PropertyToID("_BodyColor");
        private static readonly int BackColor = Shader.PropertyToID("_BackColor");

        public MorphBehaviour(IntPtr value) : base(value)
        {
        }

        public PlayerControl Player { get; set; }
        public PlayerControl SampledPlayer { get; set; }

        public void Start()
        {
            Player.nameText.Text = SampledPlayer.Data.PlayerName;
            Player.myRend.material.SetColor(BackColor, Palette.ShadowColors[SampledPlayer.Data.ColorId]);
            Player.myRend.material.SetColor(BodyColor, Palette.PlayerColors[SampledPlayer.Data.ColorId]);
            Player.HatRenderer.SetHat(SampledPlayer.Data.HatId, SampledPlayer.Data.ColorId);
            Player.nameText.transform.localPosition = new Vector3(0f, (SampledPlayer.Data.HatId == 0U) ? 0.7f : 1.05f, -0.5f);

            var allSkins = new List<SkinData>(DestroyableSingleton<HatManager>.Instance.AllSkins.ToArray());
            if (Player.MyPhysics.Skin.skin.ProdId != allSkins[(int)SampledPlayer.Data.SkinId].ProdId)
            {
                SetSkinWithAnim(Player.MyPhysics, SampledPlayer.Data.SkinId);
            }

            var allPets = new List<PetBehaviour>(DestroyableSingleton<HatManager>.Instance.AllPets.ToArray());
            if (Player.CurrentPet == null || Player.CurrentPet.ProdId != allPets[(int)SampledPlayer.Data.PetId].ProdId)
            {
                if (Player.CurrentPet)
                {
                    Destroy(Player.CurrentPet.gameObject);
                }
                Player.CurrentPet = Instantiate(allPets[(int)SampledPlayer.Data.PetId]);
                Player.CurrentPet.transform.position = Player.transform.position;
                Player.CurrentPet.Source = Player;
                Player.CurrentPet.Visible = Player.Visible;
                PlayerControl.SetPlayerMaterialColors(SampledPlayer.Data.ColorId, Player.CurrentPet.rend);
            }
            else if (Player.CurrentPet)
            {
                PlayerControl.SetPlayerMaterialColors(SampledPlayer.Data.ColorId, Player.CurrentPet.rend);
            }
        }

        internal void Stop()
        {
            Player.SetName(Player.Data.PlayerName);
            Player.SetHat(Player.Data.HatId, Player.Data.ColorId);
            SetSkinWithAnim(Player.MyPhysics, Player.Data.SkinId);
            Player.SetPet(Player.Data.PetId);
            Player.CurrentPet.Visible = Player.Visible;
            Player.SetColor(Player.Data.ColorId);
            Destroy(this);
        }

        private static void SetSkinWithAnim(PlayerPhysics playerPhysics, uint skinId)
        {
            var allSkins = new List<SkinData>(DestroyableSingleton<HatManager>.Instance.AllSkins.ToArray());
            SkinData nextSkin = allSkins[(int)skinId];
            AnimationClip clip = null;
            var spriteAnim = playerPhysics.Skin.animator;
            var anim = spriteAnim.m_animator;
            var skinLayer = playerPhysics.Skin;

            var currentPhysicsAnim = playerPhysics.Animator.GetCurrentAnimation();
            if (currentPhysicsAnim == playerPhysics.RunAnim) clip = nextSkin.RunAnim;
            else if (currentPhysicsAnim == playerPhysics.SpawnAnim) clip = nextSkin.SpawnAnim;
            else if (currentPhysicsAnim == playerPhysics.EnterVentAnim) clip = nextSkin.EnterVentAnim;
            else if (currentPhysicsAnim == playerPhysics.ExitVentAnim) clip = nextSkin.ExitVentAnim;
            else if (currentPhysicsAnim == playerPhysics.IdleAnim) clip = nextSkin.IdleAnim;
            else clip = nextSkin.IdleAnim;

            float progress = playerPhysics.Animator.m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            skinLayer.skin = nextSkin;

            spriteAnim.Play(clip, 1f);
            anim.Play("a", 0, progress % 1);
            anim.Update(0f);
        }

    }
}
