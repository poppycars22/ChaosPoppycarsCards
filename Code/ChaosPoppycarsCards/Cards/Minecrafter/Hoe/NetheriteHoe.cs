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
    class NetheriteHoe : CustomCard
    {
        internal static CardInfo Card = null;
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been setup.");
            gun.attackSpeed = .5f;
            gun.reloadTime = 0.5f;
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
            cardInfo.allowMultiple = false;
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            ClassesRegistry.Get(WoodenHoe.Card).DeBhitelist(WoodenSword.Card);
            ClassesRegistry.Get(WoodenHoe.Card).DeBhitelist(WoodenAxe.Card);
            ClassesRegistry.Get(WoodenHoe.Card).DeBhitelist(LetherArmor.Card);
            ClassesRegistry.Get(WoodenAxe.Card).DeBhitelist(WoodenHoe.Card);
            ClassesRegistry.Get(WoodenSword.Card).DeBhitelist(WoodenHoe.Card);
            ClassesRegistry.Get(LetherArmor.Card).DeBhitelist(WoodenHoe.Card);
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            //Edits values on player when card is selected
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            ClassesRegistry.Get(WoodenHoe.Card).Blacklist(WoodenSword.Card);
            ClassesRegistry.Get(WoodenHoe.Card).Blacklist(WoodenAxe.Card);
            ClassesRegistry.Get(WoodenHoe.Card).Blacklist(LetherArmor.Card);
            ClassesRegistry.Get(WoodenAxe.Card).Blacklist(WoodenHoe.Card);
            ClassesRegistry.Get(WoodenSword.Card).Blacklist(WoodenHoe.Card);
            ClassesRegistry.Get(LetherArmor.Card).Blacklist(WoodenHoe.Card);
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");

            //Run when the card is removed from the player
        }
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = MinecrafterClass.name;
        }
        protected override string GetTitle()
        {
            return "Netherite Hoe";
        }
        protected override string GetDescription()
        {
            return "Gives attack speed and reload speed";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCards.Bundle.LoadAsset<GameObject>("C_NetheriteHoe");
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
                    stat = "Attack Speed",
                    amount = "+50%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Reload Speed",
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
    }
}
