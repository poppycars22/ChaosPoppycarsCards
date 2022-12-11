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
using ChaosPoppycarsCards.Extensions;
using RarityLib.Utils;
using RarityLib;

namespace ChaosPoppycarsCards.Cards


{
    class GeeseSwarm : CustomCard
    {
        
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.allowMultiple = false;
            gun.damage = 1.35f;
            statModifiers.lifeSteal = 0.5f;
            statModifiers.health = 1.35f;
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been setup.");
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            
            statModifiers.numberOfJumps += 3;
            foreach (Player otherPlayer in PlayerStatus.GetOtherPlayers(player))
            {
                if (ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(otherPlayer.data.stats).blacklistedCategories.Contains(CPCCardCategories.GeeseCategory))
                {
                    ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(otherPlayer.data.stats).blacklistedCategories.Remove(CPCCardCategories.GeeseCategory);
                }
            }
                CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            //Edits values on player when card is selected
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
            //Run when the card is removed from the player
            foreach (Player otherPlayer in PlayerStatus.GetOtherPlayers(player))
            {
                if (!ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(otherPlayer.data.stats).blacklistedCategories.Contains(CPCCardCategories.GeeseCategory))
                {
                    ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(otherPlayer.data.stats).blacklistedCategories.Add(CPCCardCategories.GeeseCategory);
                }
            }
        }

        protected override string GetTitle()
        {
            return "Geese Swarm";
        }
        protected override string GetDescription()
        {
            return "Unleash <i><b><color=#ff2020>The Geese</b></color></i> onto your opponents";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCards.Bundle.LoadAsset<GameObject>("C_GeeseSwarm");
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return RarityUtils.GetRarity("Scarce");
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
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Life Steal",
                    amount = "+50%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Health",
                    amount = "+35%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {
                    positive = true,
                    stat = "Jumps",
                    amount = "+3",
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


namespace ChaosPoppycarsCards.Extensions
{
    public static class PlayerStatus
    {
        public static List<Player> GetEnemyPlayers(Player player)
        {
            List<Player> res = new List<Player>() { };
            foreach (Player other_player in PlayerManager.instance.players)
            {
                if (other_player.teamID != player.teamID)
                {
                    res.Add(other_player);
                }
            }
            return res;
        }
        public static List<Player> GetOtherPlayers(Player player)
        {
            List<Player> res = new List<Player>() { };
            foreach (Player other_player in PlayerManager.instance.players)
            {
                if (other_player.playerID != player.playerID)
                {
                    res.Add(other_player);
                }
            }
            return res;
        }
        public static bool PlayerAlive(Player player)
        {
            return !player.data.dead;
        }
        public static bool PlayerSimulated(Player player)
        {
            return (bool)Traverse.Create(player.data.playerVel).Field("simulated").GetValue();
        }
        public static bool PlayerAliveAndSimulated(Player player)
        {
            return (PlayerStatus.PlayerAlive(player) && PlayerStatus.PlayerSimulated(player));
        }
        public static int GetNumberOfEnemyPlayers(Player player)
        {
            int num = 0;
            foreach (Player other_player in PlayerManager.instance.players)
            {
                if (other_player.teamID != player.teamID)
                {
                    num++;
                }
            }
            return num;
        }
    }
}