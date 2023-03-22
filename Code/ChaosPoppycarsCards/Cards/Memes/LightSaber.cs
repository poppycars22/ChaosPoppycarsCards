using UnboundLib.Cards;
using UnityEngine;
using ChaosPoppycarsCards.Utilities;
using RarityLib.Utils;

namespace ChaosPoppycarsCards.Cards
{
    class LightSaber : CustomCard
    {
        
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been setup.");
            
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
            gun.damage = 0.15f;
            cardInfo.allowMultiple = false;
            gun.destroyBulletAfter = 0.066f;
            gun.reloadTime = 0.00003f;
            gun.gravity = 0f;
            gun.attackSpeed = 0.00003f;
            gun.ammo = +50;
            block.cdMultiplier = 2f;
            
            statModifiers.movementSpeed = 0.7f;
            //make this card, if you can figure out how, a light saber png attached to the gun that also makes the bullets invisable
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            //Edits values on player when card is selected
            gun.reflects *= 0;
            gun.spread *= 0;
            block.additionalBlocks = 0;
            
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");

           
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
            return RarityUtils.GetRarity("Legendary");
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
                    amount = "-85%",
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
                    amount = "+50"
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Spread",
                    amount = "No"
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Block CD",
                    amount = "+100%"
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Blocks",
                    amount = "1"
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Movement Speed",
                    amount = "-30%"
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
