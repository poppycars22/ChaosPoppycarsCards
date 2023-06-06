using System;
using System.Runtime.CompilerServices;
using CPCCardInfostuffs;
using HarmonyLib;
using UnityEngine;

namespace CPC.Extensions
{

    // ADD FIELDS TO GUN
    [Serializable]
    public class GunAdditionalData
    {

        public float criticalHitChance1 = 0f;
        public float criticalHitDamage1 = 1f;


        public GunAdditionalData()
        {
            UnityEngine.Debug.Log("GunAdditionalData Loaded");
            criticalHitChance1 = 0f;
            UnityEngine.Debug.Log($"[crit chance {criticalHitChance1}]");
            criticalHitDamage1 = 1f;
            UnityEngine.Debug.Log($"[crit damage {criticalHitDamage1}]");
        }
    }
    public static class GunExtension
    {
        public static readonly ConditionalWeakTable<Gun, GunAdditionalData> data = new ConditionalWeakTable<Gun, GunAdditionalData>();

        public static GunAdditionalData GetAdditionalData(this Gun gun)
        {
            UnityEngine.Debug.Log("GunAdditionalData#2 Loaded");
            return data.GetOrCreateValue(gun);
        }

        public static void AddData(this Gun gun, GunAdditionalData value)
        {
            UnityEngine.Debug.Log("GunAddData Loaded");
            try
            {
                data.Add(gun, value);
                UnityEngine.Debug.Log("Try succeded");
            }
            catch (Exception) { UnityEngine.Debug.Log("Try failed"); }
        }
       

    }
    // reset extra gun attributes when resetstats is called
    [HarmonyPatch(typeof(Gun), "ResetStats")]
    class GunPatchResetStats
    {
        private static void Prefix(Gun __instance)
        {
            UnityEngine.Debug.Log("prefix got applied");
            __instance.GetAdditionalData().criticalHitChance1 = 0f;
            UnityEngine.Debug.Log($"[crit chance {__instance.GetAdditionalData().criticalHitChance1}]");
            __instance.GetAdditionalData().criticalHitDamage1 = 1f;
            UnityEngine.Debug.Log($"[crit damage {__instance.GetAdditionalData().criticalHitDamage1}]");
        }
    }
    [HarmonyPatch(typeof(ApplyCardStats), "ApplyStats")]

    internal class ApplyCardStatsPatch
    {
        private static void Postfix(ApplyCardStats __instance, Player ___playerToUpgrade)
        {
            var THINGaa = __instance.GetComponent<CPCCardInfo>();

            UnityEngine.Debug.Log("PostFix loaded");
            UnityEngine.Debug.Log($"[test1 GunCritDamage {THINGaa.GunCritDamage2}, GunCritChance {THINGaa.GunCritChance2} ]");
            if (THINGaa != null)
            {

                UnityEngine.Debug.Log("CPCCARDinfo isnt null");
                UnityEngine.Debug.Log($"[test2 GunCritDamage {THINGaa.GunCritDamage2}, GunCritChance {THINGaa.GunCritChance2} ]");
                if (___playerToUpgrade.data.weaponHandler.gun != null)
                {
                    UnityEngine.Debug.Log("Gun Additional Data isnt null");
                    UnityEngine.Debug.Log($"[test3 GunCritDamage {THINGaa.GunCritDamage2} , GunCritChance  {THINGaa.GunCritChance2} ]");
                    UnityEngine.Debug.Log($"[ criticalHitDamage {___playerToUpgrade.data.weaponHandler.gun.GetAdditionalData().criticalHitDamage1}, criticalHitChance {___playerToUpgrade.data.weaponHandler.gun.GetAdditionalData().criticalHitChance1} ]");
                    ___playerToUpgrade.data.weaponHandler.gun.GetAdditionalData().criticalHitDamage1 += THINGaa.GunCritDamage2;
                    ___playerToUpgrade.data.weaponHandler.gun.GetAdditionalData().criticalHitChance1 += THINGaa.GunCritChance2;
                    UnityEngine.Debug.Log($"[crit chance {___playerToUpgrade.data.weaponHandler.gun.GetAdditionalData().criticalHitChance1}, crit damage {___playerToUpgrade.data.weaponHandler.gun.GetAdditionalData().criticalHitDamage1} ]");
                }
            }
        }
    }
}