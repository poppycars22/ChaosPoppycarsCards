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
using ClassesManagerReborn.Util;
using UnboundLib.GameModes;
using System.Collections;

namespace ChaosPoppycarsCards.Cards.Minecrafter
{
    class AttackSpeed : CustomCard
    {

        internal static CardInfo Card = null;

        static bool everyOtherRound2 = true;


        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {

            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been setup.");
            gun.attackSpeed = .5f;
            cardInfo.allowMultiple = false;
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {

            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            //Edits values on player when card is selected





        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");

            //Run when the card is removed from the player
        }

        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = MinecrafterClass.name;
        }

        protected override string GetTitle()
        {
            return "Wooden Hoe";
        }
        protected override string GetDescription()
        {
            return "Gives attack speed, unlocks stone hoe, craft the next level of hoe on every other round end";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCards.Bundle.LoadAsset<GameObject>("C_WoodenHoe");
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Common;
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Attack Speed",
                    amount = "+50%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.FirepowerYellow;
        }

        public override string GetModName()
        {
            return "CPC";
        }
        internal static IEnumerator UpgradeHoe(IGameModeHandler gm)
        {
            everyOtherRound2 = !everyOtherRound2;
            if (everyOtherRound2 == false)
            {
                foreach (Player player in PlayerManager.instance.players.ToArray())
                {
                    if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, ModdingUtils.Utils.Cards.instance.GetCardWithName("Stone Hoe")))
                    {
                        var upgradeStoneHoe = ModdingUtils.Utils.Cards.instance.GetCardWithName("Stone Hoe");
                        ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, upgradeStoneHoe, addToCardBar: true);
                        ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, upgradeStoneHoe);
                        
                    }
                    else if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, ModdingUtils.Utils.Cards.instance.GetCardWithName("Iron Hoe")))
                    {
                        var upgradeIronHoe = ModdingUtils.Utils.Cards.instance.GetCardWithName("Iron Hoe");
                        ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, upgradeIronHoe, addToCardBar: true);
                        ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, upgradeIronHoe);
                        
                    }
                    else if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, ModdingUtils.Utils.Cards.instance.GetCardWithName("Diamond Hoe")))
                    {
                        var upgradeDiamondHoe = ModdingUtils.Utils.Cards.instance.GetCardWithName("Diamond Hoe");
                        ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, upgradeDiamondHoe, addToCardBar: true);
                        ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, upgradeDiamondHoe);
                       
                    }
                    else if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, ModdingUtils.Utils.Cards.instance.GetCardWithName("Netherite Hoe")))
                    {
                        var upgradeNetheriteHoe = ModdingUtils.Utils.Cards.instance.GetCardWithName("Netherite Hoe");
                        ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, upgradeNetheriteHoe, addToCardBar: true);
                        ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, upgradeNetheriteHoe);
                        
                    }
                    yield break;
                }
            }
        }
    }
}
