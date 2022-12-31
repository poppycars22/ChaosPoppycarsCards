using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using Photon.Pun;
using BepInEx;
using ChaosPoppycarsCards.Cards;
using ChaosPoppycarsCards.Utilities;
using HarmonyLib;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using ChaosPoppycarsCards.MonoBehaviours;
using UnboundLib.Networking;
using System.Reflection;
using System.Collections.ObjectModel;
using ModdingUtils.MonoBehaviours;
using UnboundLib.Utils;
using WillsWackyManagers.Utils;
using RarityLib.Utils;
using ChaosPoppycarsCards.Extensions;
using Nullmanager;
using ChaosPoppycarsCards.MonoBehaviours.SodaMonos;

namespace ChaosPoppycarsCards.Cards
{
    class PoppysChaos : CustomCard
    {

        internal static CardInfo Card = null;
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
            //next versions poppys chaos 
            /*
            statModifiers.movementSpeed = -1f;
            gun.attackSpeed = -1f;
            gun.damage = -1f;
            statModifiers.jump = -1f;
            statModifiers.gravity = -1f;
            gun.gravity = -1f;
            gun.dmgMOnBounce = -1f;
            gun.knockback = -1f;
            gun.reflects = -1;
            statModifiers.secondsToTakeDamageOver = -1f;
            statModifiers.sizeMultiplier = -1f;
            */

            block.cdAdd = 3f;
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been setup.");
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            block.additionalBlocks = 0;
            
            var mono = player.gameObject.GetOrAddComponent<RegenEffect>();
            var mono2 = player.gameObject.GetOrAddComponent<JumpEffect>();
            var mono3 = player.gameObject.GetOrAddComponent<SpeedEffect>();
            var mono4 = player.gameObject.GetOrAddComponent<StrengthEffect>();
            var mono6 = player.gameObject.GetOrAddComponent<SPRSodaEffect>();
            var mono7 = player.gameObject.GetOrAddComponent<COCSodaEffect>();
            var mono8 = player.gameObject.GetOrAddComponent<DRSodaEffect>();
            var mono9 = player.gameObject.GetOrAddComponent<PEPSodaEffect>();
            var mono10 = player.gameObject.GetOrAddComponent<MTDSodaEffect>();
            var mono5 = player.gameObject.GetOrAddComponent<InvisEffect>();
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            //Edits values on player when card is selected

        }
        
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            var mono = player.gameObject.GetOrAddComponent<RegenEffect>();
            var mono2 = player.gameObject.GetOrAddComponent<JumpEffect>();
            var mono3 = player.gameObject.GetOrAddComponent<SpeedEffect>();
            var mono4 = player.gameObject.GetOrAddComponent<StrengthEffect>();
            var mono5 = player.gameObject.GetOrAddComponent<InvisEffect>();
            var mono6 = player.gameObject.GetOrAddComponent<SPRSodaEffect>();
            var mono7 = player.gameObject.GetOrAddComponent<COCSodaEffect>();
            var mono8 = player.gameObject.GetOrAddComponent<DRSodaEffect>();
            var mono9 = player.gameObject.GetOrAddComponent<PEPSodaEffect>();
            var mono10 = player.gameObject.GetOrAddComponent<MTDSodaEffect>();
            UnityEngine.GameObject.Destroy(mono);
            UnityEngine.GameObject.Destroy(mono2);
            UnityEngine.GameObject.Destroy(mono3);
            UnityEngine.GameObject.Destroy(mono4);
            UnityEngine.GameObject.Destroy(mono5);
            UnityEngine.GameObject.Destroy(mono6);
            UnityEngine.GameObject.Destroy(mono7);
            UnityEngine.GameObject.Destroy(mono8);
            UnityEngine.GameObject.Destroy(mono9);
            UnityEngine.GameObject.Destroy(mono10);
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
            //Run when the card is removed from the player
        }


        protected override string GetTitle()
        {
            return "Poppys Chaos";
        }
        protected override string GetDescription()
        {
            return "This cards changes (almost) every update";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCards.Bundle.LoadAsset<GameObject>("C_PoppysChaos");
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
                    stat = "",
                    amount = "<#FFFF00>+???</color>",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {

                    stat = "",
                    amount = "<#FFFF00>-???</color>",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
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
