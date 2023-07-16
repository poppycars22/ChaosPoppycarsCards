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
using ModdingUtils.MonoBehaviours;
namespace ChaosPoppycarsCards.Cards
{
    class GetawayTest : CustomCard
    {
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been setup.");
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            HealthBasedEffect Above90 = player.gameObject.AddComponent<HealthBasedEffect>();
            Above90.healthHandlerModifier.regen_add += 1;
            Above90.SetPercThresholdMax(1f);
            Above90.SetPercThresholdMin(0.9f);
            HealthBasedEffect Above80 = player.gameObject.AddComponent<HealthBasedEffect>();
            Above80.healthHandlerModifier.regen_add += 3;
            Above80.SetPercThresholdMax(0.9f);
            Above80.SetPercThresholdMin(0.8f);
            Above80.SetColor(Color.grey);
            HealthBasedEffect Above70 = player.gameObject.AddComponent<HealthBasedEffect>();
            Above70.healthHandlerModifier.regen_add += 6f;
            Above70.SetPercThresholdMax(0.8f);
            Above70.SetPercThresholdMin(0.7f);
            Above70.SetColor(Color.green);
            HealthBasedEffect Above60 = player.gameObject.AddComponent<HealthBasedEffect>();
            Above60.healthHandlerModifier.regen_add += 12;
            Above60.SetPercThresholdMax(0.7f);
            Above60.SetPercThresholdMin(0.6f);
            Above60.SetColor(Color.cyan);
            HealthBasedEffect Above50 = player.gameObject.AddComponent<HealthBasedEffect>();
            Above50.healthHandlerModifier.regen_add += 16f;
            Above50.SetPercThresholdMax(0.6f);
            Above50.SetPercThresholdMin(0.5f);
            Above50.SetColor(Color.magenta);
            HealthBasedEffect Above40 = player.gameObject.AddComponent<HealthBasedEffect>();
            Above40.healthHandlerModifier.regen_add += 20;
            Above40.SetPercThresholdMax(0.5f);
            Above40.SetPercThresholdMin(0.4f);
            Above40.SetColor(Color.yellow);
            HealthBasedEffect Above30 = player.gameObject.AddComponent<HealthBasedEffect>();
            Above30.healthHandlerModifier.regen_add += 24;
            Above30.SetPercThresholdMax(0.4f);
            Above30.SetPercThresholdMin(0.3f);
            Above30.SetColor(Color.red);
            HealthBasedEffect Above20 = player.gameObject.AddComponent<HealthBasedEffect>();
            Above20.healthHandlerModifier.regen_add += 28;
            Above20.SetPercThresholdMax(0.3f);
            Above20.SetPercThresholdMin(0.2f);
            Above20.SetColor(Color.blue);
            HealthBasedEffect Above10 = player.gameObject.AddComponent<HealthBasedEffect>();
            Above10.healthHandlerModifier.regen_add += 32;
            Above10.SetPercThresholdMax(0.2f);
            Above10.SetPercThresholdMin(0.1f);
            Above10.SetColor(Color.white);
            HealthBasedEffect Above0 = player.gameObject.AddComponent<HealthBasedEffect>();
            Above0.healthHandlerModifier.regen_add += 36;
            Above0.SetPercThresholdMax(0.1f);
            Above0.SetPercThresholdMin(0f);
            Above0.SetColor(Color.clear);
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
            return "Test";
        }
        protected override string GetDescription()
        {
            return "Once below 50% hp gain movment speed, attack speed, and knockback";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCards.Bundle.LoadAsset<GameObject>("C_GETAWAY");
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
                    positive = true,
                    stat = "WHEN BELOW 50%",
                    amount = "",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Movement Speed",
                    amount = "+100%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
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
                stat = "Knockback",
                amount = "+1000%",
                simepleAmount = CardInfoStat.SimpleAmount.notAssigned
            }
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.DefensiveBlue;
        }
        public override string GetModName()
        {
            return "CPC";
        }
    }
}
