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
using ChaosPoppycarsCards.MonoBehaviours;

namespace ChaosPoppycarsCards.Cards.Minecrafter
{
    
    class WoodenShovel : CustomCard
    {
        internal static GameObject drill = null;
        internal static CardInfo Card = null;
        

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been setup.");
            cardInfo.allowMultiple = false;
            
            if (!WoodenShovel.drill)
            {
                WoodenShovel.drill = new GameObject("WoodenshovelDrill", new Type[]
                {
                    typeof(SetupDrill)   
                });
                WoodenShovel.drill.GetComponent<SetupDrill>().metersToDrill = 111111111111111111111111f;
                WoodenShovel.drill.GetComponent<SetupDrill>().speedModFlat2 = 1f;
                WoodenShovel.drill.GetComponent<SetupDrill>().speedMod2 = 1f;
            }
            
            gun.objectsToSpawn = new ObjectsToSpawn[]
            {
                new ObjectsToSpawn
                {
                    AddToProjectile = WoodenShovel.drill
                }
            };
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
            return "Wooden Shovel";
        }
        protected override string GetDescription()
        {
            return "Your bullets drill through the ground for a bit";
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
                    stat = "Bullets drill through walls",
                    amount = "+0.5m",
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
        
        
        internal static IEnumerator UpgradeShovel(IGameModeHandler gm)
        {
            everyOtherRound = !everyOtherRound;
            if (everyOtherRound == false)
            {
                foreach (Player player in PlayerManager.instance.players.ToArray())
                {
                        if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, StoneSword.Card))
                        {
                            ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, StoneSword.Card, addToCardBar: true);
                            ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, StoneSword.Card);

                        }
                        else if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, IronSword.Card))
                        {

                            ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, IronSword.Card, addToCardBar: true);
                            ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, IronSword.Card);

                        }
                        else if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, DiamondSword.Card))
                        {
                            
                            ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, DiamondSword.Card, addToCardBar: true);
                            ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, DiamondSword.Card);

                        }
                        else if (ModdingUtils.Utils.Cards.instance.PlayerIsAllowedCard(player, NetheriteSword.Card))
                        {
                            
                            ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, NetheriteSword.Card, addToCardBar: true);
                            ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, NetheriteSword.Card);

                        }
                }
                    
            }
            yield break;
        }

        
    }
}
