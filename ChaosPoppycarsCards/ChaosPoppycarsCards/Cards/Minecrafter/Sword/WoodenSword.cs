using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using ChaosPoppycarsCards.Cards;
using ChaosPoppycarsCards.Utilities;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using ClassesManagerReborn.Util;
using UnboundLib.GameModes;
using System.Collections;
using System.Collections.ObjectModel;
using System.Reflection;
using TMPro;
using UnityEngine.UI;
using UnboundLib.Utils;
using UnboundLib.Networking;
using Photon.Pun;

namespace ChaosPoppycarsCards.Cards.Minecrafter
{
    
    class WoodenSword : CustomCard
    {
        internal static CardInfo Card = null;
        

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been setup.");
            cardInfo.allowMultiple = false;
            gun.damage = 1.35f;
            
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
            return "Wooden Sword";
        }
        protected override string GetDescription()
        {
            return "Gives a little damage, unlocks stone sword, craft the next level of sword on every other round end";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCards.Bundle.LoadAsset<GameObject>("C_WoodenSword");
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
                    amount = "+35%",
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
        static bool everyOtherRound = true;
        
        
        internal static IEnumerator UpgradeSword(IGameModeHandler gm)
        {
            everyOtherRound = !everyOtherRound;
            if (everyOtherRound == false)
            {
                foreach (Player player in PlayerManager.instance.players.ToArray())
                {
                        if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, ModdingUtils.Utils.Cards.instance.GetCardWithName("Stone Sword")))
                        {
                            var upgradeStone = ModdingUtils.Utils.Cards.instance.GetCardWithName("Stone Sword");
                            ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, upgradeStone, addToCardBar: true);
                            ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, upgradeStone);

                        }
                        else if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, ModdingUtils.Utils.Cards.instance.GetCardWithName("Iron Sword")))
                        {
                            var upgradeIron = ModdingUtils.Utils.Cards.instance.GetCardWithName("Iron Sword");
                            ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, upgradeIron, addToCardBar: true);
                            ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, upgradeIron);

                        }
                        else if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, ModdingUtils.Utils.Cards.instance.GetCardWithName("Diamond Sword")))
                        {
                            var upgradeDiamond = ModdingUtils.Utils.Cards.instance.GetCardWithName("Diamond Sword");
                            ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, upgradeDiamond, addToCardBar: true);
                            ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, upgradeDiamond);

                        }
                        else if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, ModdingUtils.Utils.Cards.instance.GetCardWithName("Netherite Sword")))
                        {
                            var upgradeNetherite = ModdingUtils.Utils.Cards.instance.GetCardWithName("Netherite Sword");
                            ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, upgradeNetherite, addToCardBar: true);
                            ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, upgradeNetherite);

                        }
                }
                    
            }
            yield break;
        }

        
    }
}
