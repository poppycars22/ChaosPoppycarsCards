using UnboundLib.Cards;
using UnityEngine;
using UnboundLib;
using CPC.Extensions;
using ChaosPoppycarsCards.MonoBehaviours;


namespace ChaosPoppycarsCards.Cards
{
    public class TotemOfUndying : CustomCard
    {

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {
            cardInfo.allowMultiple = true;
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            

            player.gameObject.GetOrAddComponent<TotemEffect>();
            characterStats.GetAdditionalData().mulligans++;
            characterStats.GetAdditionalData().remainingMulligans = characterStats.GetAdditionalData().mulligans;

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
            return "Always survive a fatal blow, and gain a bit of regen";
        }

        protected override GameObject GetCardArt()
        {
            return null;
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
                stat = "Mulligans",
                amount = "+1",
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