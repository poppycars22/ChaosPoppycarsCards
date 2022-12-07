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
using WillsWackyManagers.Utils;
using ModdingUtils.Extensions;
using RarityLib.Utils;

namespace ChaosPoppycarsCards.Cards
{
    class Peptide : CustomCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.GetAdditionalData().canBeReassigned = false;
            cardInfo.categories = new CardCategory[] { CustomCardCategories.instance.CardCategory("CardManipulation"), RerollManager.instance.NoFlip };
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been setup.");
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            ChaosPoppycarsCards.Instance.ExecuteAfterFrames(20, () => {
                var rare = ModdingUtils.Utils.Cards.instance.GetRandomCardWithCondition(player, gun, gunAmmo, data, health, gravity, block, characterStats, RareCondition);
                var rare2 = ModdingUtils.Utils.Cards.instance.GetRandomCardWithCondition(player, gun, gunAmmo, data, health, gravity, block, characterStats, RareCondition);
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, rare, false, "", 2f, 2f, true);
            ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, rare, 3f);
            ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, rare2, false, "", 2f, 2f, true);
            ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, rare2, 3f);
                CurseManager.instance.CursePlayer(player, (curse) => {
                    ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, curse, 3f);
                });
                CurseManager.instance.CursePlayer(player, (curse) => {
                    ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, curse, 3f);
                });
            });
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            //Edits values on player when card is selected
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
            //Run when the card is removed from the player
        }

        protected override string GetTitle()
        {
            return "Peptide";
        }
        protected override string GetDescription()
        {
            return "I'm a gambler at heart";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCards.Bundle.LoadAsset<GameObject>("C_Peptide");
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return RarityUtils.GetRarity("Scarce");
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Rares",
                    amount = "+2",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Curses",
                    amount = "+2",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.EvilPurple;
        }
        public override string GetModName()
        {
            return "CPC";
        }
        private bool RareCondition(CardInfo card, Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            return card.rarity == CardInfo.Rarity.Rare && card.cardName != "Purifying Light" && card.cardName != "Peptide" && card.cardName != "Distill" && cardInfo != CustomCardCategories.instance.CardCategory("CardManipulation");
            
        }
    }
}
