using UnboundLib.Cards;
using UnityEngine;
using UnboundLib;
using ChaosPoppycarsCards.MonoBehaviours;


namespace ChaosPoppycarsCards.Cards
{
    public class TotemOfUndying : CustomCard
    {

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {
            cardInfo.allowMultiple = true;
            statModifiers.respawns = 1;
            statModifiers.movementSpeed = 0.5f;
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            

            

        }
        public override void OnRemoveCard()
        {
        }

        protected override string GetTitle()
        {
            return "Totem Of Undying";
        }
        protected override string GetDescription()
        {
            return "Gain a respawn, but loose some movement speed";
        }

        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCards.Bundle.LoadAsset<GameObject>("C_TotemOfUndying");
        }

        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Rare;
        }

        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat
                {
                positive = true,
                stat = "Revives",
                amount = "+1",
                simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                 new CardInfoStat
                {
                positive = false,
                stat = "Move Speed",
                amount = "-50%",
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