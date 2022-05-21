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
    class WoodenAxe : CustomCard
    {
        internal static CardInfo Card = null;
        
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been setup.");
            gun.damage = 1.50f;
            gun.attackSpeed = 1.5f;
            
            cardInfo.allowMultiple = false;
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            bool everyOtherRound3 = false;
            GameModeManager.AddHook(GameModeHooks.HookRoundEnd, UpgradeAxe);
            IEnumerator UpgradeAxe(IGameModeHandler gm)
            {
                if (everyOtherRound3)
                {
                    everyOtherRound3 = false;
                }
                else if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, ModdingUtils.Utils.Cards.instance.GetCardWithName("Stone Axe")))
                {
                    var upgradeStoneAxe = ModdingUtils.Utils.Cards.instance.GetCardWithName("Stone Axe");
                    ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, upgradeStoneAxe, addToCardBar: true);
                    ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, upgradeStoneAxe);
                    everyOtherRound3 = true;
                }
                else if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, ModdingUtils.Utils.Cards.instance.GetCardWithName("Iron Axe")))
                {
                    var upgradeIronAxe = ModdingUtils.Utils.Cards.instance.GetCardWithName("Iron Axe");
                    ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, upgradeIronAxe, addToCardBar: true);
                    ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, upgradeIronAxe);
                    everyOtherRound3 = true;
                }
                else if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, ModdingUtils.Utils.Cards.instance.GetCardWithName("Diamond Axe")))
                {
                    var upgradeDiamondAxe = ModdingUtils.Utils.Cards.instance.GetCardWithName("Diamond Axe");
                    ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, upgradeDiamondAxe, addToCardBar: true);
                    ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, upgradeDiamondAxe);
                    everyOtherRound3 = true;
                }
                else if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, ModdingUtils.Utils.Cards.instance.GetCardWithName("Netherite Axe")))
                {
                    var upgradeNetheriteAxe = ModdingUtils.Utils.Cards.instance.GetCardWithName("Netherite Axe");
                    ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, upgradeNetheriteAxe, addToCardBar: true);
                    ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, upgradeNetheriteAxe);
                    everyOtherRound3 = true;
                }
                yield break;
            }
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
            return "Wooden Axe";
        }
        protected override string GetDescription()
        {
            return "Gives a lot of damage, reduces attack speed, unlocks stone axe, craft the next level of axe on every other round end";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCards.Bundle.LoadAsset<GameObject>("C_WoodenAxe");
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
                    stat = "Damage",
                    amount = "+50%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Attack speed",
                    amount = "-50%",
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
    }
}
