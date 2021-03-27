using Reactor;
using System;
using UnityEngine;

namespace PopeyesRolesMod.Roles.ShapeShifter
{
    [RegisterInIl2Cpp]
    public class MorphBehaviour : MonoBehaviour
    {
        public MorphBehaviour(IntPtr value) : base(value)
        {
        }

        public PlayerControl Player { get; set; }
        public PlayerControl SampledPlayer { get; set; }

        public void Start()
        {
            Player.nameText.Text = SampledPlayer.Data.PlayerName;
            Player.myRend.material.SetColor("_BackColor", Palette.ShadowColors[SampledPlayer.Data.ColorId]);
            Player.myRend.material.SetColor("_BodyColor", Palette.PlayerColors[SampledPlayer.Data.ColorId]);
            Player.HatRenderer.SetHat(SampledPlayer.Data.HatId, SampledPlayer.Data.ColorId);
            Player.nameText.transform.localPosition = new Vector3(0f, (SampledPlayer.Data.HatId == 0U) ? 0.7f : 1.05f, -0.5f);

            if (Player.MyPhysics.Skin.skin.ProdId != DestroyableSingleton<HatManager>.Instance.AllSkins[(int)SampledPlayer.Data.SkinId].ProdId)
            {
                setSkinWithAnim(Player.MyPhysics, SampledPlayer.Data.SkinId);
            }
            if (Player.CurrentPet == null || Player.CurrentPet.ProdId != DestroyableSingleton<HatManager>.Instance.AllPets[(int)SampledPlayer.Data.PetId].ProdId)
            {
                if (Player.CurrentPet)
                {
                    Destroy(Player.CurrentPet.gameObject);
                }
                Player.CurrentPet = Instantiate(DestroyableSingleton<HatManager>.Instance.AllPets[(int)SampledPlayer.Data.PetId]);
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
            setSkinWithAnim(Player.MyPhysics, Player.Data.SkinId);
            Player.SetPet(Player.Data.PetId);
            Player.CurrentPet.Visible = Player.Visible;
            Player.SetColor(Player.Data.ColorId);
            Destroy(this);
        }

        public static void setSkinWithAnim(PlayerPhysics playerPhysics, uint skinId)
        {
            SkinData nextSkin = DestroyableSingleton<HatManager>.Instance.AllSkins[(int)skinId];
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
