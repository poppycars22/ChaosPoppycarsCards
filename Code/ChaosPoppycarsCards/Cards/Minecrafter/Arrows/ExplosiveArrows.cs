﻿using System;
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
using ChaosPoppycarsCards.MonoBehaviours;
using ClassesManagerReborn.Util;
using ChaosPoppycarsCards.Cards.Minecrafter;

namespace ChaosPoppycarsCards.Cards
{
    class ExplosiveArrows : CustomCard
    {
        internal static CardInfo Card = null;
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
           
            gun.damage = 0.85f;
           
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been setup.");
            cardInfo.allowMultiple = true;
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
            return "TNT Arrows";
        }
        protected override string GetDescription()
        {
            return "You attached TNT to your arrows, now they will explode shortly after landing";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCards.Bundle.LoadAsset<GameObject>("C_ExplosiveArrow");
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
                    positive = false,
                    stat = "Damage",
                    amount = "-15%",
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
    }
}
