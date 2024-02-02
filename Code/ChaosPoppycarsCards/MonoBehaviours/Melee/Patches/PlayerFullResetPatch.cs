using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoggoBonk.MonoBehaviors.Squish.Lanky;
using DoggoBonk.MonoBehaviors.Squish.Wide;
using UnityEngine;
using ChaosPoppycarsCards.Lightsaber;

namespace DoggoBonk.MonoBehaviors.Squish.Patches
{
    [HarmonyPatch(typeof(Player), "FullReset")]
    [Serializable]
    internal class PlayerFullResetPatch
    {
        private static void Prefix(Player __instance)
        {
            foreach (WidePlayerEffect widePlayerEffect in __instance.gameObject.GetComponentsInChildren<WidePlayerEffect>())
            {
                if (widePlayerEffect != null)
                {
                    GameObject.Destroy(widePlayerEffect);
                }
            }
            foreach (LankyPlayerEffect lankyPlayerEffect in __instance.gameObject.GetComponentsInChildren<LankyPlayerEffect>())
            {
                if (lankyPlayerEffect != null)
                {
                    GameObject.Destroy(lankyPlayerEffect);
                }
            }
            __instance.gameObject.transform.GetChild(3).gameObject.GetComponentInChildren<LegRaycasters>().force = 1000f;

            GameObject.Destroy(__instance.GetComponentInChildren<A_HoldableObject>());
            
            
        }
    }
}
