using System;
using System.Runtime.CompilerServices;
using ChaosPoppycarsCards.Cards;
using HarmonyLib;

namespace CPC.Extensions
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
        }
    }
}