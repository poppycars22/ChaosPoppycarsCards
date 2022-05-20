using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using ClassesManagerReborn.Util;
using ChaosPoppycarsCards.Cards;
using ChaosPoppycarsCards.Utilities;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using UnboundLib.GameModes;
using System.Collections;

namespace ChaosPoppycarsCards.Cards.Minecrafter
{
    class Larmor : CustomCard
    {
        internal static CardInfo Card = null;
        public static CardCategory[] ChainArmorUpgrade = new CardCategory[] { CPCCardCategories.ChainArmorCategory };
        public static CardCategory[] IronArmorUpgrade = new CardCategory[] { CPCCardCategories.IronArmorCategory };
        public static CardCategory[] DiamondArmorUpgrade = new CardCategory[] { CPCCardCategories.DiamondArmorCategory };
        public static CardCategory[] NetheriteArmorUpgrade = new CardCategory[] { CPCCardCategories.NetheriteArmorCategory };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            
            statModifiers.health = 1.8f;
            cardInfo.allowMultiple = false;
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been setup.");
            
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            bool everyOtherRound4 = false;
            GameModeManager.AddHook(GameModeHooks.HookRoundEnd, UpgradeArmor);
            IEnumerator UpgradeArmor(IGameModeHandler gm)
            {
                if (everyOtherRound4 == true)
                {
                    everyOtherRound4 = false;
                }
                else if (ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Contains(CPCCardCategories.ChainArmorCategory) == false)
                {

                    CardInfo upgradeStoneArmor = ModdingUtils.Utils.Cards.instance.NORARITY_GetRandomCardWithCondition(null, null, null, null, null, null, null, null, this.condition);
                    ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, upgradeStoneArmor, addToCardBar: true);
                    ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, upgradeStoneArmor);
                    everyOtherRound4 = true;
                }
                else if (ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Contains(CPCCardCategories.IronArmorCategory) == false)
                {

                    CardInfo upgradeIronArmor = ModdingUtils.Utils.Cards.instance.NORARITY_GetRandomCardWithCondition(null, null, null, null, null, null, null, null, this.condition2);
                    ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, upgradeIronArmor, addToCardBar: true);
                    ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, upgradeIronArmor);
                    everyOtherRound4 = true;
                }
                else if (ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Contains(CPCCardCategories.DiamondArmorCategory) == false)
                {

                    CardInfo upgradeDiamondArmor = ModdingUtils.Utils.Cards.instance.NORARITY_GetRandomCardWithCondition(null, null, null, null, null, null, null, null, this.condition3);
                    ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, upgradeDiamondArmor, addToCardBar: true);
                    ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, upgradeDiamondArmor);
                    everyOtherRound4 = true;
                }
                else if (ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Contains(CPCCardCategories.NetheriteArmorCategory) == false)
                {

                    CardInfo upgradeNetheriteArmor = ModdingUtils.Utils.Cards.instance.NORARITY_GetRandomCardWithCondition(null, null, null, null, null, null, null, null, this.condition4);
                    ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, upgradeNetheriteArmor, addToCardBar: true);
                    ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, upgradeNetheriteArmor);
                    everyOtherRound4 = true;
                }
                yield break;
            }
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
            return "Leather Armor";
        }
        protected override string GetDescription()
        {
            return "Put on leather armor to survive longer, unlocks chainmail armor, craft the next level of armor on every other round end";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCards.Bundle.LoadAsset<GameObject>("C_LeatherArmor");
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
                    stat = "Health",
                    amount = "+80%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.ColdBlue;
        }
        public bool condition(CardInfo card, Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            return card.categories.Intersect(Larmor.ChainArmorUpgrade).Any();
        }
        public bool condition2(CardInfo card, Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            return card.categories.Intersect(Larmor.IronArmorUpgrade).Any();
        }
        public bool condition3(CardInfo card, Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            return card.categories.Intersect(Larmor.DiamondArmorUpgrade).Any();
        }
        public bool condition4(CardInfo card, Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            return card.categories.Intersect(Larmor.NetheriteArmorUpgrade).Any();
        }
        public override string GetModName()
        {
            return "CPC";
        }
    }
}
