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
using ChaosPoppycarsCards.Extensions;

namespace ChaosPoppycarsCards.Cards.Minecrafter
{
    class LetherArmor : CustomCard
    {
        internal static CardInfo Card = null;
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {

            statModifiers.health = 1.1f;
            cardInfo.allowMultiple = false;
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been setup.");

            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            characterStats.GetAdditionalData().everyOther = false;
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
            return CardInfo.Rarity.Rare;
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Health",
                    amount = "+10%",
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
            foreach (Player player in PlayerManager.instance.players.ToArray())
            {
                if (player.data.stats.GetAdditionalData().everyOther == false)
                {
                    if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, ChainArmor.Card))
                    {
                        ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, ChainArmor.Card, addToCardBar: true);
                        ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, ChainArmor.Card);
                    }
                    else if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, IronArmor.Card))
                    {
                        ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, IronArmor.Card, addToCardBar: true);
                        ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, IronArmor.Card);
                    }
                    else if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, DiamondArmor.Card))
                    {
                        ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, DiamondArmor.Card, addToCardBar: true);
                        ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, DiamondArmor.Card);
                    }
                    else if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, NetheriteArmor.Card))
                    {
                        ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, NetheriteArmor.Card, addToCardBar: true);
                        ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, NetheriteArmor.Card);
                    }
                }
            }
            yield break;
        }
    }
}
