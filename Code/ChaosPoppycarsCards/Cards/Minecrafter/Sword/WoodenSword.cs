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
using ClassesManagerReborn;
using UnboundLib.Extensions;
using ChaosPoppycarsCards.Extensions;

namespace ChaosPoppycarsCards.Cards.Minecrafter
{
    
    class WoodenSword : CustomCard
    {
        internal static CardInfo Card = null;
        

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been setup.");
            cardInfo.allowMultiple = false;
            gun.damage = 1.1f;
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
                    amount = "+10%",
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
        
        
        internal static IEnumerator UpgradeSword(IGameModeHandler gm)
        {
            foreach (Player player in PlayerManager.instance.players.ToArray())
            {
                player.data.stats.GetAdditionalData().everyOther = !(player.data.stats.GetAdditionalData().everyOther);
                if (player.data.stats.GetAdditionalData().everyOther == false)
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
