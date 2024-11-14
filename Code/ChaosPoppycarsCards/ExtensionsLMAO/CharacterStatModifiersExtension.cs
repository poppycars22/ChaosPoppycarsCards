using System;
using System.Runtime.CompilerServices;
using ChaosPoppycarsCards.Cards;
using HarmonyLib;
using Photon.Realtime;
using PlayerTimeScale;

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
        public int dashes;
        public bool blockMover;
        public bool blockPush;
        public float blockMoveStrength;
        public float forcedMove;
        public bool forcedMoveEnabled;
        public bool speedyHands;
        public bool triggerFinger;
        public bool acceleratedRejuvenation;
        public bool boostedBlock;
        public int maxWarps;
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
            dashes = 0;
            blockMover = false;
            blockPush = false;
            blockMoveStrength = 0;
            forcedMove = 0;
            forcedMoveEnabled = false;
            speedyHands = false;
            triggerFinger = false;
            acceleratedRejuvenation = false;
            boostedBlock = false;
            maxWarps = 0;
        }
    }
    public static class CharacterStatModifiersExtension
    {
        public static readonly ConditionalWeakTable<CharacterStatModifiers, CharacterStatModifiersAdditionalData> data = new ConditionalWeakTable<CharacterStatModifiers, CharacterStatModifiersAdditionalData>();

        public static CharacterStatModifiersAdditionalData GetAdditionalData(this CharacterStatModifiers statModifiers)
        {
            var a = data.GetOrCreateValue(statModifiers);
            return a;
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
            __instance.GetAdditionalData().dashes = 0;
            __instance.GetAdditionalData().blockMover = false;
            __instance.GetAdditionalData().blockPush = false;
            __instance.GetAdditionalData().blockMoveStrength = 0f;
            __instance.GetAdditionalData().forcedMove = 0f;
            __instance.GetAdditionalData().forcedMoveEnabled = false;
            __instance.GetAdditionalData().speedyHands = false;
            __instance.GetAdditionalData().triggerFinger = false;
            __instance.GetAdditionalData().acceleratedRejuvenation = false;
            __instance.GetAdditionalData().boostedBlock = false;
            __instance.GetAdditionalData().maxWarps = 0;
        }
    }
}