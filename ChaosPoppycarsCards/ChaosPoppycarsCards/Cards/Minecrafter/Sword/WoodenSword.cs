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

namespace ChaosPoppycarsCards.Cards.Minecrafter
{
    class WoodenSword : CustomCard
    {
        internal static CardInfo Card = null;
        public static CardCategory[] StoneUpgrade = new CardCategory[] { CPCCardCategories.StoneSwordCategory };
        public static CardCategory[] IronUpgrade = new CardCategory[] { CPCCardCategories.IronSwordCategory };
        public static CardCategory[] DiamondUpgrade = new CardCategory[] { CPCCardCategories.DiamondSwordCategory };
        public static CardCategory[] NetheriteUpgrade = new CardCategory[] { CPCCardCategories.NetheriteSwordCategory };
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been setup.");
            cardInfo.allowMultiple = false;
            gun.damage = 1.35f;
           
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            bool everyOtherRound = false;
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            GameModeManager.AddHook(GameModeHooks.HookRoundEnd, UpgradeSword);
            IEnumerator UpgradeSword(IGameModeHandler gm)
            {
                if (everyOtherRound == true)
                {
                    everyOtherRound = false;
                }
                else if (ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Contains(CPCCardCategories.StoneSwordCategory) == false)
                {
                   
                    CardInfo upgradeStone = ModdingUtils.Utils.Cards.instance.NORARITY_GetRandomCardWithCondition(null, null, null, null, null, null, null, null, this.condition);
                    ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, upgradeStone, addToCardBar: true);
                    ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, upgradeStone);
                    everyOtherRound = true;
                }
                else if (ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Contains(CPCCardCategories.IronSwordCategory) == false)
                {
                    
                    CardInfo upgradeIron = ModdingUtils.Utils.Cards.instance.NORARITY_GetRandomCardWithCondition(null, null, null, null, null, null, null, null, this.condition2);
                    ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, upgradeIron, addToCardBar: true);
                    ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, upgradeIron);
                    everyOtherRound = true;
                }
                else if (ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Contains(CPCCardCategories.DiamondSwordCategory) == false)
                {
                    
                    CardInfo upgradeDiamond = ModdingUtils.Utils.Cards.instance.NORARITY_GetRandomCardWithCondition(null, null, null, null, null, null, null, null, this.condition3);
                    ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, upgradeDiamond, addToCardBar: true);
                    ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, upgradeDiamond);
                    everyOtherRound = true;
                }
                else if (ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Contains(CPCCardCategories.NetheriteSwordCategory) == false)
                {
                    
                    CardInfo upgradeNetherite = ModdingUtils.Utils.Cards.instance.NORARITY_GetRandomCardWithCondition(null, null, null, null, null, null, null, null, this.condition4);
                    ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, upgradeNetherite, addToCardBar: true);
                    ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, upgradeNetherite);
                    everyOtherRound = true;
                }
                yield break;
            }
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
                    amount = "+35%",
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
            return card.categories.Intersect(WoodenSword.StoneUpgrade).Any();
        }
        public bool condition2(CardInfo card, Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            return card.categories.Intersect(WoodenSword.IronUpgrade).Any();
        }
        public bool condition3(CardInfo card, Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            return card.categories.Intersect(WoodenSword.DiamondUpgrade).Any();
        }
        public bool condition4(CardInfo card, Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            return card.categories.Intersect(WoodenSword.NetheriteUpgrade).Any();
        }
        public override string GetModName()
        {
            return "CPC";
        }
        
    }
}
