using System;
using System.Runtime.CompilerServices;
using HarmonyLib;

namespace CPC.Extensions
{
    // ADD FIELDS TO GUN
    [Serializable]
    public class CharacterStatModifiersAdditionalData
    {

        public float RainbowLeafHealth;
        public float HealthBouncesBounced;

        public CharacterStatModifiersAdditionalData()
        {
         //   RainbowLeafHealth = 0f;
            HealthBouncesBounced = 0f;
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
        }
    }
}