using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using BepInEx;
using ChaosPoppycarsCards.Cards;
using ChaosPoppycarsCards.Utilities;
using HarmonyLib;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using ChaosPoppycarsCards.MonoBehaviours;
using System.Reflection;
using UnboundLib.Networking;
using System.Collections.ObjectModel;
using UnboundLib.Utils;
using ClassesManagerReborn.Util;

namespace ChaosPoppycarsCards.Cards.Minecrafter
{
    class UltimatePotion : CustomCard
    {
        internal static CardInfo Card = null;
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            
            cardInfo.allowMultiple = false;
            block.cdMultiplier = 1.75f;
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been setup.");
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            var mono = player.gameObject.GetOrAddComponent<RegenEffect>();
            var mono2 = player.gameObject.GetOrAddComponent<JumpEffect>();
            var mono3 = player.gameObject.GetOrAddComponent<SpeedEffect>();
            var mono4 = player.gameObject.GetOrAddComponent<StrengthEffect>();
            var mono5 = player.gameObject.GetOrAddComponent<InvisEffect>();
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Add(CPCCardCategories.PotionCategory);
            //Edits values on player when card is selected
        }

        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
            var mono = player.gameObject.GetOrAddComponent<RegenEffect>();
            var mono2 = player.gameObject.GetOrAddComponent<JumpEffect>();
            var mono3 = player.gameObject.GetOrAddComponent<SpeedEffect>();
            var mono4 = player.gameObject.GetOrAddComponent<StrengthEffect>();
            var mono5 = player.gameObject.GetOrAddComponent<InvisEffect>();
            UnityEngine.GameObject.Destroy(mono);
            UnityEngine.GameObject.Destroy(mono2);
            UnityEngine.GameObject.Destroy(mono3);
            UnityEngine.GameObject.Destroy(mono4);
            UnityEngine.GameObject.Destroy(mono5);
            ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Remove(CPCCardCategories.PotionCategory);
            //Run when the card is removed from the player
        }
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = MinecrafterClass.name;
        }
        protected override string GetTitle()
        {
            return "Ultimate Potion";
        }
        protected override string GetDescription()
        {
            return "When you block you get all the potion effects";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCards.Bundle.LoadAsset<GameObject>("C_UltimatePotion");
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Rare;
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Block Cooldown",
                    amount = "+75%",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                }
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.MagicPink;
        }
        public override string GetModName()
        {
            return "CPC";
        }
    }
}
