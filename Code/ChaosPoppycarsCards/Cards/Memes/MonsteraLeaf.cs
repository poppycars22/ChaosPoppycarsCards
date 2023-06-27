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
using RarityLib.Utils;
using CPC.Extensions;

namespace ChaosPoppycarsCards.Cards
{
    class MonsteraLeaf : CustomCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {

            cardInfo.allowMultiple = true;
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been setup.");
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}]{characterStats.GetAdditionalData().RainbowLeafHealth}");
            data.maxHealth += characterStats.GetAdditionalData().RainbowLeafHealth;
            characterStats.health += characterStats.GetAdditionalData().RainbowLeafHealth;
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}]{characterStats.GetAdditionalData().RainbowLeafHealth}");
            //Edits values on player when card is selected
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {

            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
            //Run when the card is removed from the player
        }

        protected override string GetTitle()
        {
            return "Monstera Leaf";
        }
        protected override string GetDescription()
        {
            return "Gain 25 max hp every point";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCards.Bundle.LoadAsset<GameObject>("C_RainbowLeaf");
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return RarityUtils.GetRarity("Divine");
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.ColdBlue;
        }
        public override string GetModName()
        {
            return "CPC";
        }
    }
}
