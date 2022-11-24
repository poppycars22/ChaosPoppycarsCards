using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using Photon.Pun;
using BepInEx;
using ChaosPoppycarsCards.Cards;
using ChaosPoppycarsCards.Utilities;
using HarmonyLib;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using ChaosPoppycarsCards.MonoBehaviours;
using UnboundLib.Networking;
using System.Reflection;
using System.Collections.ObjectModel;
using ModdingUtils.MonoBehaviours;
using UnboundLib.Utils;
using WillsWackyManagers.Utils;
using RarityLib.Utils;
using ModdingUtils.Extensions;


namespace ChaosPoppycarsCards.Cards
{
    class PoppysChaos : CustomCard
    {
       

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.GetAdditionalData().canBeReassigned = false;
            cardInfo.categories = new CardCategory[] { CurseManager.instance.curseSpawnerCategory, RerollManager.instance.NoFlip };
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been setup.");
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            ChaosPoppycarsCards.Instance.ExecuteAfterFrames(20, () =>
            {
                var legend = ModdingUtils.Utils.Cards.instance.GetRandomCardWithCondition(player, gun, gunAmmo, data, health, gravity, block, characterStats, LegendCondition);
                ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, legend, false, "", 2f, 2f, true);
                ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, legend, 3f);
                CurseManager.instance.CursePlayer(player, (curse) => {
                    ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, curse, 3f);
                });
                CurseManager.instance.CursePlayer(player, (curse) => {
                    ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, curse, 3f);
                });
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
        private bool LegendCondition(CardInfo card, Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            return card.rarity == RarityUtils.GetRarity("Legendary");

        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
            //Run when the card is removed from the player
        }


        protected override string GetTitle()
        {
            return "Poppys Chaos";
        }
        protected override string GetDescription()
        {
            return "This cards changes (almost) every update, The chaoth hath returned";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCards.Bundle.LoadAsset<GameObject>("C_PoppysChaos");
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Uncommon;
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    stat = "Who knows",
                    amount = "+???",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                },
                new CardInfoStat()
                {

                    stat = "I dont",
                    amount = "-???",
                    simepleAmount = CardInfoStat.SimpleAmount.Some
                }
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
