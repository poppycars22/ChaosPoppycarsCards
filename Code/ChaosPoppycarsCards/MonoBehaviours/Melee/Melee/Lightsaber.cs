using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib.Cards;
using UnityEngine;

namespace ChaosPoppycarsCards.Lightsaber
{
    /*
    public class LightsaberCard : CustomCard
    {
        internal static CardInfo Card = null;
        /*
         * TRT traitor shop card that allows the player to switch to a knife with [item 2]
         */
    /*
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers)
        {
            cardInfo.allowMultiple = false;
            statModifiers.AddObjectToPlayer = A_Lightsaber.Lightsaber;
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            // hide knife just to be sure
            try
            {
                A_HoldableObject.MakeGunHoldable(player.playerID, false);
            }
            catch { }
        }

        protected override string GetTitle()
        {
            return "Lightsaber";
        }
        protected override string GetDescription()
        {
            return "A decisive melee weapon you can switch to with [item 2]. Good for one kill.";
        }

        protected override GameObject GetCardArt()
        {
            return null;//GameModeCollection.TRT_Assets.LoadAsset<GameObject>("C_Knife");
        }

        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Rare;
        }

        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.DestructiveRed;
        }
    }*/
}
