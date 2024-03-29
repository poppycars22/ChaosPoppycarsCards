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
using ClassesManagerReborn.Util;
using ClassesManagerReborn;

namespace ChaosPoppycarsCards.Cards.Minecrafter
{
    class NetheriteSword : CustomCard
    {
        internal static CardInfo Card = null;
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been setup.");
            gun.damage = 1.8f;
            gun.attackSpeed = 1f / 1.25f;
            cardInfo.allowMultiple = false;
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            ClassesRegistry.Get(WoodenSword.Card).DeBhitelist(WoodenHoe.Card);
            ClassesRegistry.Get(WoodenSword.Card).DeBhitelist(WoodenAxe.Card);
            ClassesRegistry.Get(WoodenSword.Card).DeBhitelist(LetherArmor.Card);
            ClassesRegistry.Get(WoodenAxe.Card).DeBhitelist(WoodenSword.Card);
            ClassesRegistry.Get(WoodenHoe.Card).DeBhitelist(WoodenSword.Card);
            ClassesRegistry.Get(LetherArmor.Card).DeBhitelist(WoodenSword.Card);
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            //Edits values on player when card is selected
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            ClassesRegistry.Get(WoodenSword.Card).Blacklist(WoodenHoe.Card);
            ClassesRegistry.Get(WoodenSword.Card).Blacklist(WoodenAxe.Card);
            ClassesRegistry.Get(WoodenSword.Card).Blacklist(LetherArmor.Card);
            ClassesRegistry.Get(WoodenAxe.Card).Blacklist(WoodenSword.Card);
            ClassesRegistry.Get(WoodenHoe.Card).Blacklist(WoodenSword.Card);
            ClassesRegistry.Get(LetherArmor.Card).Blacklist(WoodenSword.Card);
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
            //Run when the card is removed from the player
        }
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = MinecrafterClass.name;
        }
        protected override string GetTitle()
        {
            return "Netherite Sword";
        }
        protected override string GetDescription()
        {
            return "Gives a lot of damage, and some attack speed";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCards.Bundle.LoadAsset<GameObject>("C_NetheriteSword");
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
                    stat = "Damage",
                    amount = "+80%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Attack Speed",
                    amount = "+25%",
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
