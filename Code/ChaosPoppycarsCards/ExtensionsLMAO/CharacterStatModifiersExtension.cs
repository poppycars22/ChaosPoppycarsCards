﻿using System;
using System.Runtime.CompilerServices;
using ChaosPoppycarsCards.Cards;
using HarmonyLib;

namespace ChaosPoppycarsCards.Extensions
{
    // ADD FIELDS TO GUN
    [Serializable]
    public class CharacterStatModifiersAdditionalData
    {

        public float RainbowLeafHealth;
        public float HealthBouncesBounced;
        public bool useNewRespawnTime;
        public float newRespawnTime;
        public int shuffles;
        public int GeeseSwarms;
        public int StunningStares;
        public int NanoMachines;
        public int remainingTotems;
        public int totems;
        public int Redstone;
        public int Glowstone;
        public bool InvisPot;
        public bool healthCase;
        public bool blockCase;
        public bool everyOther;
        public bool firstHit;
        public float damageMult;
        public float damageMultMax;
        public bool firstDamage;
        public bool reducingDmg;
        public float reducingDmgAmt;
        public float firstHitdmgReduction;
        public bool damagingBullet;
        public CharacterStatModifiersAdditionalData()
        {
         //   RainbowLeafHealth = 0f;
            HealthBouncesBounced = 0f;
            useNewRespawnTime = false;
            newRespawnTime = 0f;
            shuffles = 0;
            GeeseSwarms = 0;
            StunningStares = 0;
            NanoMachines = 0;
            remainingTotems = 0;
            totems = 0;
            Redstone = 0;
            Glowstone = 0;
            InvisPot = false;
            healthCase = false;
            blockCase = false;
            everyOther = true;
            firstHit = true;
            firstHitdmgReduction = 1f;
            firstDamage = true;
            reducingDmg = false;
            damageMult = 1f;
            damageMultMax = 1f;
            reducingDmgAmt = 0f;
            damagingBullet = false;
        }
    }
    public static class CharacterStatModifiersExtension
    {
        public static readonly ConditionalWeakTable<CharacterStatModifiers, CharacterStatModifiersAdditionalData> data = new ConditionalWeakTable<CharacterStatModifiers, CharacterStatModifiersAdditionalData>();

        public static CharacterStatModifiersAdditionalData GetAdditionalData(this CharacterStatModifiers statModifiers)
        {
            return data.GetOrCreateValue(statModifiers);
        }

        public static void AddData(this CharacterStatModifiers statModifiers, CharacterStatModifiersAdditionalData value)
        {
            try
            {
                data.Add(statModifiers, value);
            }
            catch (Exception) { }
        }
    }
    [HarmonyPatch(typeof(CharacterStatModifiers), "ResetStats")]
    class CharacterStatModifiersPatchResetStats
    {
        private static void Prefix(CharacterStatModifiers __instance)
        {
            //__instance.GetAdditionalData().RainbowLeafHealth = 0f;
            __instance.GetAdditionalData().HealthBouncesBounced = 0f;
            __instance.GetAdditionalData().useNewRespawnTime = false;
            __instance.GetAdditionalData().newRespawnTime = 0f;
            __instance.GetAdditionalData().shuffles = 0;
            __instance.GetAdditionalData().GeeseSwarms = 0;
            __instance.GetAdditionalData().StunningStares = 0;
            __instance.GetAdditionalData().NanoMachines = 0;
            __instance.GetAdditionalData().remainingTotems = 0;
            __instance.GetAdditionalData().totems = 0;
            __instance.GetAdditionalData().Redstone = 0;
            __instance.GetAdditionalData().Glowstone = 0;
            __instance.GetAdditionalData().InvisPot = false;
            __instance.GetAdditionalData().healthCase = false;
            __instance.GetAdditionalData().blockCase = false;
            __instance.GetAdditionalData().everyOther = true;
            __instance.GetAdditionalData().firstHit = true;
            __instance.GetAdditionalData().firstHitdmgReduction = 1f;
            __instance.GetAdditionalData().firstDamage = true;
            __instance.GetAdditionalData().reducingDmg = false;
            __instance.GetAdditionalData().damageMult = 1f;
            __instance.GetAdditionalData().damageMultMax = 1f;
            __instance.GetAdditionalData().reducingDmgAmt = 0f;
            __instance.GetAdditionalData().damagingBullet = false;
        }
    }
}