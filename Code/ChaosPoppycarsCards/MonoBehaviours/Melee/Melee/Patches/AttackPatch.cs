using HarmonyLib;
using ChaosPoppycarsCards.Lightsaber.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChaosPoppycarsCards.Lightsaber.Patches
{
    internal class AttackPatch
    {
        [HarmonyPatch(typeof(Gun), nameof(Gun.Attack))]
        class GunPatchAttack
        {
            static bool Prefix(Gun __instance)
            {
                return (!__instance.GetData().disabled);
            }
        }
    }
}
