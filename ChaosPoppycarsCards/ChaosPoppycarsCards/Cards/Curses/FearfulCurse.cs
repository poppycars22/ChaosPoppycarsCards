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
using WillsWackyManagers.Utils;
using ModdingUtils.MonoBehaviours;

namespace ChaosPoppycarsCards.Cards
{
    class FearCurse : CustomCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            
            cardInfo.categories = new CardCategory[] { CurseManager.instance.curseCategory };
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been setup.");
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            HealthBasedEffect effect = player.gameObject.AddComponent<HealthBasedEffect>();
            effect.characterStatModifiersModifier.movementSpeed_mult = 0.5f;
            effect.gunStatModifier.attackSpeed_mult = 1.5f;
            effect.gunStatModifier.spread_add = 0.5f;
            effect.gunAmmoStatModifier.reloadTimeMultiplier_mult = 1.5f;
            effect.SetPercThresholdMax(0.75f);
            effect.SetColor(Color.magenta);
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            //Edits values on player when card is selected
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
            //Run when the card is removed from the player
        }

        protected override string GetTitle()
        {
            return "Paralyzing Fear";
        }
        protected override string GetDescription()
        {
            return "You quickly go into fear when below 75% of your max health";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCards.Bundle.LoadAsset<GameObject>("C_ParalyzingFear");
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Common;
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.EvilPurple;
        }
        public override string GetModName()
        {
            return "CPC Curses";
        }
    }
}
