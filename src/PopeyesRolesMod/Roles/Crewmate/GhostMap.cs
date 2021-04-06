using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace PopeyesRolesMod.Roles.Crewmate
{

    [HarmonyPatch(typeof(MapBehaviour))]
    public static class GhostMap
    {

        [HarmonyPrefix]
        [HarmonyPatch(nameof (MapBehaviour.ShowNormalMap))]
        public static bool PrefixShowNormalMap(MapBehaviour __instance)
        {
            HerePoints.Clear();
            return false;
        }

        private static Dictionary<byte, SpriteRenderer> HerePoints = new Dictionary<byte, SpriteRenderer>();

        [HarmonyPostfix]
        [HarmonyPatch(nameof (MapBehaviour.ShowNormalMap))]
        public static void PostfixShowNormalMap(MapBehaviour __instance)
        {
            if (__instance.IsOpen)
            {
                __instance.Close();
                return;
            }
            if (!PlayerControl.LocalPlayer.CanMove)
            {
                return;
            }
            HerePoints[PlayerControl.LocalPlayer.PlayerId] = __instance.HerePoint;
            if (PlayerControl.LocalPlayer.Data.IsDead)
            {
                foreach (var player in PlayerControl.AllPlayerControls)
                {
                    if (!HerePoints.ContainsKey(player.PlayerId))
                    {
                        var point = UnityEngine.Object.Instantiate(__instance.HerePoint, __instance.HerePoint.transform.parent);
                        point.enabled = true;
                        HerePoints[player.PlayerId] = point;
                    }
                    player.SetPlayerMaterialColors(HerePoints[player.PlayerId]);

                }
            }
            __instance.PDPJBNINLPF();
            __instance.taskOverlay.Show();
            __instance.ColorControl.SetColor(new Color(0.05f, 0.2f, 1f, 1f));
            DestroyableSingleton<HudManager>.Instance.SetHudActive(false);
        }

        [HarmonyPrefix]
        [HarmonyPatch(typeof(MapBehaviour), nameof(MapBehaviour.FixedUpdate))]
        public static bool PrefixFixedUpdate(MapBehaviour __instance)
        {
            return false;
        }

        [HarmonyPostfix]
        [HarmonyPatch(typeof(MapBehaviour), nameof(MapBehaviour.FixedUpdate))]
        public static void PostfixFixedUpdate(MapBehaviour __instance)
        {
            if (!ShipStatus.Instance)
            {
                return;
            }
            foreach (var player in HerePoints)
            {
                Vector3 vector = PlayerDataManager.GetPlayerById(player.Key).transform.position;
                vector /= ShipStatus.Instance.MapScale;
                vector.x *= Mathf.Sign(ShipStatus.Instance.transform.localScale.x);
                vector.z = -1f;
                player.Value.transform.localPosition = vector;
            }
        }
    }
}
