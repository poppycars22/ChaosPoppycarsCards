using ChaosPoppycarsCards.Utilities;
using ClassesManagerReborn;
using ClassesManagerReborn.Util;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;

namespace ChaosPoppycarsCards.Cards.Minecrafter
{
    class NetheriteAxe : CustomCard
    {
        internal static CardInfo Card = null;
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been setup.");
            gun.damage = 2.5f;
            gun.attackSpeed = 3f;
            
            cardInfo.allowMultiple = false;
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            ClassesRegistry.Get(WoodenAxe.Card).DeBhitelist(WoodenHoe.Card);
            ClassesRegistry.Get(WoodenAxe.Card).DeBhitelist(WoodenSword.Card);
            ClassesRegistry.Get(WoodenAxe.Card).DeBhitelist(LetherArmor.Card);
            ClassesRegistry.Get(WoodenSword.Card).DeBhitelist(WoodenAxe.Card);
            ClassesRegistry.Get(WoodenHoe.Card).DeBhitelist(WoodenAxe.Card);
            ClassesRegistry.Get(LetherArmor.Card).DeBhitelist(WoodenAxe.Card);
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            //Edits values on player when card is selected
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            ClassesRegistry.Get(WoodenAxe.Card).Blacklist(WoodenHoe.Card);
            ClassesRegistry.Get(WoodenAxe.Card).Blacklist(WoodenSword.Card);
            ClassesRegistry.Get(WoodenAxe.Card).Blacklist(LetherArmor.Card);
            ClassesRegistry.Get(WoodenSword.Card).Blacklist(WoodenAxe.Card);
            ClassesRegistry.Get(WoodenHoe.Card).Blacklist(WoodenAxe.Card);
            ClassesRegistry.Get(LetherArmor.Card).Blacklist(WoodenAxe.Card);
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
            //Run when the card is removed from the player
        }
        public override void Callback()
        {
            gameObject.GetOrAddComponent<ClassNameMono>().className = MinecrafterClass.name;
        }
        protected override string GetTitle()
        {
            return "Netherite Axe";
        }
        protected override string GetDescription()
        {
            return "Gives a lot of damage, reduces attack speed";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCards.Bundle.LoadAsset<GameObject>("C_NetheriteAxe");
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
                    amount = "+150%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = false,
                    stat = "Attack speed",
                    amount = "-200%",
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
