using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using ClassesManagerReborn.Util;
using ChaosPoppycarsCards.Cards;
using ChaosPoppycarsCards.Utilities;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using UnboundLib.GameModes;
using System.Collections;

namespace ChaosPoppycarsCards.Cards.Minecrafter
{
    class Larmor : CustomCard
    {
        internal static CardInfo Card = null;
        static bool everyOtherRound4 = true;
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {

            statModifiers.health = 1.7f;
            cardInfo.allowMultiple = false;
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been setup.");

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
            return "Leather Armor";
        }
        protected override string GetDescription()
        {
            return "Put on leather armor to survive longer, unlocks chainmail armor, craft the next level of armor on every other round end";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCards.Bundle.LoadAsset<GameObject>("C_LeatherArmor");
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
                    stat = "Health",
                    amount = "+70%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
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

        internal static IEnumerator UpgradeArmor(IGameModeHandler gm)
        {
            everyOtherRound4 = !everyOtherRound4;
            if (everyOtherRound4 == false)
            {
                foreach (Player player in PlayerManager.instance.players.ToArray())
                {
                    if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, ModdingUtils.Utils.Cards.instance.GetCardWithName("Chainmail Armor")))
                    {
                        var upgradeChainArmor = ModdingUtils.Utils.Cards.instance.GetCardWithName("Chainmail Armor");
                        ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, upgradeChainArmor, addToCardBar: true);
                        ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, upgradeChainArmor);

                    }
                    else if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, ModdingUtils.Utils.Cards.instance.GetCardWithName("Iron Armor")))
                    {
                        var upgradeIronArmor = ModdingUtils.Utils.Cards.instance.GetCardWithName("Iron Armor");
                        ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, upgradeIronArmor, addToCardBar: true);
                        ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, upgradeIronArmor);

                    }
                    else if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, ModdingUtils.Utils.Cards.instance.GetCardWithName("Diamond Armor")))
                    {
                        var upgradeDiamondArmor = ModdingUtils.Utils.Cards.instance.GetCardWithName("Diamond Armor");
                        ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, upgradeDiamondArmor, addToCardBar: true);
                        ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, upgradeDiamondArmor);

                    }
                    else if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, ModdingUtils.Utils.Cards.instance.GetCardWithName("Netherite Armor")))
                    {
                        var upgradeNetheriteArmor = ModdingUtils.Utils.Cards.instance.GetCardWithName("Netherite Armor");
                        ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, upgradeNetheriteArmor, addToCardBar: true);
                        ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, upgradeNetheriteArmor);

                    }
                    yield break;
                }
            }
        }
    }
}
