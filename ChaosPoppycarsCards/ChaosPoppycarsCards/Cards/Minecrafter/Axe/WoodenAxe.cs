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
       static bool everyOtherRound3 = true;
        internal static IEnumerator UpgradeAxe(IGameModeHandler gm)
        {
            everyOtherRound3 = !everyOtherRound3;
            if (everyOtherRound3 == false)
            {
                foreach (Player player in PlayerManager.instance.players.ToArray())
                {
                    if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, StoneAxe.Card))
                    {

                        ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, StoneAxe.Card, addToCardBar: true);
                        ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, StoneAxe.Card);

                    }
                    else if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, IronAxe.Card))
                    {
                       
                        ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, IronAxe.Card, addToCardBar: true);
                        ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, IronAxe.Card);

                    }
                    else if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, DiamondAxe.Card))
                    {
                        
                        ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, DiamondAxe.Card, addToCardBar: true);
                        ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, DiamondAxe.Card);

                    }
                    else if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, NetheriteAxe.Card))
                    {
                        
                        ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, NetheriteAxe.Card, addToCardBar: true);
                        ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, NetheriteAxe.Card);

                    }
                    
                }
                
            }
            yield break;
        }
    }
}
