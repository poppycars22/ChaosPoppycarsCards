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
using ChaosPoppycarsCards.MonoBehaviours;
using ClassesManagerReborn;
using ClassesManagerReborn.Patchs;
using ClassesManagerReborn.Util;
using ModdingUtils.Extensions;
using UnityEngine.UI;


namespace ChaosPoppycarsCards.Cards
{
    class LightSaber : CustomCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been setup.");
            gameObject.AddComponent<Legend>();
            var cardstuffs = gameObject.AddComponent<ClassNameMono>();
            cardstuffs.color1 = Legend.color;
            cardstuffs.color2 = Legend.color;
            cardInfo.allowMultiple = false;
            cardstuffs.className = "";
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
            gun.damage = 0.5f;
            cardInfo.allowMultiple = false;
            gun.destroyBulletAfter = 0.066f;
            gun.reloadTime = 0.0000000003f;
            gun.gravity = 0f;
            gun.attackSpeed = 0.0000000000000003f;
            gun.ammo = -101;
            gun.unblockable = true;
           
            //make this card, if you can figure out how, a light saber png attached to the gun that also makes the bullets invisable and makes the gun auto fire
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            //Edits values on player when card is selected
            gun.reflects *= 0;
            gun.spread *= 0;

            var mono = player.gameObject.GetOrAddComponent<TestMono>();
            
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");

            var mono = player.gameObject.GetOrAddComponent<TestMono>();
            UnityEngine.GameObject.Destroy(mono);
            //Run when the card is removed from the player
        }

        protected override string GetTitle()
        {
            return "Light Saber";
        }
        protected override string GetDescription()
        {
            return "Replace your gun with a lightsaber";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCards.Bundle.LoadAsset<GameObject>("C_LightSaber");
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
                    amount = "Almost Instant",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Range",
                    amount = "Very Little",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Damage",
                    amount = "-50%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Reload",
                    amount = "Almost Instant",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Bounces",
                    amount = "Reset",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Ammo",
                    amount = "1"
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Spread",
                    amount = "No"
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Bullets",
                    amount = "Unblockable"
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
