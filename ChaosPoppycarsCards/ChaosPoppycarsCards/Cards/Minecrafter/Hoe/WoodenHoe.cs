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
using ClassesManagerReborn.Util;
using UnboundLib.GameModes;
using System.Collections;

namespace ChaosPoppycarsCards.Cards.Minecrafter
{
    class AttackSpeed : CustomCard
    {
        public static CardCategory[] StoneHoeUpgrade = new CardCategory[] { CPCCardCategories.StoneHoeCategory };
        public static CardCategory[] IronHoeUpgrade = new CardCategory[] { CPCCardCategories.IronHoeCategory };
        public static CardCategory[] DiamondHoeUpgrade = new CardCategory[] { CPCCardCategories.DiamondHoeCategory };
        public static CardCategory[] NetheriteHoeUpgrade = new CardCategory[] { CPCCardCategories.NetheriteHoeCategory };
        internal static CardInfo Card = null;
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been setup.");
            gun.attackSpeed = .5f;
            cardInfo.allowMultiple = false;
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            bool everyOtherRound2 = false;
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            //Edits values on player when card is selected
            GameModeManager.AddHook(GameModeHooks.HookRoundEnd, UpgradeHoe);
            IEnumerator UpgradeHoe(IGameModeHandler gm)
            {
                if (everyOtherRound2 == true)
                {
                    everyOtherRound2 = false;
                }
                else if (ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Contains(CPCCardCategories.StoneHoeCategory) == false)
                {

                    CardInfo upgradeStoneHoe = ModdingUtils.Utils.Cards.instance.NORARITY_GetRandomCardWithCondition(null, null, null, null, null, null, null, null, this.condition);
                    ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, upgradeStoneHoe, addToCardBar: true);
                    ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, upgradeStoneHoe);
                    everyOtherRound2 = true;
                }
                else if (ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Contains(CPCCardCategories.IronHoeCategory) == false)
                {

                    CardInfo upgradeIronHoe = ModdingUtils.Utils.Cards.instance.NORARITY_GetRandomCardWithCondition(null, null, null, null, null, null, null, null, this.condition2);
                    ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, upgradeIronHoe, addToCardBar: true);
                    ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, upgradeIronHoe);
                    everyOtherRound2 = true;
                }
                else if (ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Contains(CPCCardCategories.DiamondHoeCategory) == false)
                {

                    CardInfo upgradeDiamondHoe = ModdingUtils.Utils.Cards.instance.NORARITY_GetRandomCardWithCondition(null, null, null, null, null, null, null, null, this.condition3);
                    ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, upgradeDiamondHoe, addToCardBar: true);
                    ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, upgradeDiamondHoe);
                    everyOtherRound2 = true;
                }
                else if (ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Contains(CPCCardCategories.NetheriteHoeCategory) == false)
                {

                    CardInfo upgradeNetheriteHoe = ModdingUtils.Utils.Cards.instance.NORARITY_GetRandomCardWithCondition(null, null, null, null, null, null, null, null, this.condition4);
                    ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, upgradeNetheriteHoe, addToCardBar: true);
                    ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, upgradeNetheriteHoe);
                    everyOtherRound2 = true;
                }
                yield break;
            }
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
            return "Wooden Hoe";
        }
        protected override string GetDescription()
        {
            return "Gives attack speed, unlocks stone hoe, craft the next level of hoe on every other round end";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCards.Bundle.LoadAsset<GameObject>("C_WoodenHoe");
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
                    stat = "Attack Speed",
                    amount = "+50%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.FirepowerYellow;
        }
        public bool condition(CardInfo card, Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            return card.categories.Intersect(AttackSpeed.StoneHoeUpgrade).Any();
        }
        public bool condition2(CardInfo card, Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            return card.categories.Intersect(AttackSpeed.IronHoeUpgrade).Any();
        }
        public bool condition3(CardInfo card, Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            return card.categories.Intersect(AttackSpeed.DiamondHoeUpgrade).Any();
        }
        public bool condition4(CardInfo card, Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            return card.categories.Intersect(AttackSpeed.NetheriteHoeUpgrade).Any();
        }
        public override string GetModName()
        {
            return "CPC";
        }
    }
}
