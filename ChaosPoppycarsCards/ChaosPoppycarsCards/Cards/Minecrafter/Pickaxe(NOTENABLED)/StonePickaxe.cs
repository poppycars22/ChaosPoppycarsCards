using System;
using UnboundLib.Cards;
using UnityEngine;
using ModdingUtils.MonoBehaviours;
using UnboundLib;
using ModdingUtils.Extensions;
using System.Collections.Generic;
using ChaosPoppycarsCards.Cards;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChaosPoppycarsCards.MonoBehaviours;
using BepInEx;
using ChaosPoppycarsCards.Utilities;
using HarmonyLib;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;

namespace ChaosPoppycarsCards.Cards
{
	internal class StonePickaxe : CustomCard
	{
		public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
		{
			gun.objectsToSpawn = new ObjectsToSpawn[]
			{
				new ObjectsToSpawn
				{
					effect = StonePickaxe.drill,
					spawnAsChild = true,
					spawnOn = ObjectsToSpawn.SpawnOn.notPlayer
				}
			};
			gun.attackSpeed = 6f;
            gun.projectielSimulatonSpeed = 0.1f;
            gun.reloadTime = 6f;
            gun.spread = 0.6f;
			gun.damage = 0.6f;
        }

        // Token: 0x06000076 RID: 118 RVA: 0x0000457D File Offset: 0x0000277D
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
			gun.reflects *= 0;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00004580 File Offset: 0x00002780
		public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
		{
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00004584 File Offset: 0x00002784
		protected override string GetTitle()
		{
			return "Stone Pickaxe";
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000459C File Offset: 0x0000279C
		protected override string GetDescription()
		{
			return "Your bullets mine away the ground they touch MAY BE UNSTABLE, USE AT YOUR OWN RISK (on hit effect made by Tess)";
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000045B4 File Offset: 0x000027B4
		protected override GameObject GetCardArt()
		{
			return null;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x000045C8 File Offset: 0x000027C8
		protected override CardInfo.Rarity GetRarity()
		{
			return CardInfo.Rarity.Common;
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000045DC File Offset: 0x000027DC
		protected override CardInfoStat[] GetStats()
		{
			return new CardInfoStat[]
			{
				new CardInfoStat
				{
					positive = false,
					stat = "Projectile Speed",
					amount = "-90%",
					simepleAmount = CardInfoStat.SimpleAmount.aLotLower
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Attack Speed",
					amount = "-500%",
					simepleAmount = CardInfoStat.SimpleAmount.aLotLower
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Reload Speed",
					amount = "-500%",
					simepleAmount = CardInfoStat.SimpleAmount.aLotLower
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Bounces",
					amount = "Reset",
					simepleAmount = CardInfoStat.SimpleAmount.aLotLower
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Spread",
					amount = "+40%",
					simepleAmount = CardInfoStat.SimpleAmount.aLotLower
				},
				new CardInfoStat
				{
					positive = false,
					stat = "Damage",
					amount = "-40%",
					simepleAmount = CardInfoStat.SimpleAmount.aLotLower
				}
			};
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000464C File Offset: 0x0000284C
		protected override CardThemeColor.CardThemeColorType GetTheme()
		{
			return CardThemeColor.CardThemeColorType.FirepowerYellow;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00004660 File Offset: 0x00002860
		public override string GetModName()
		{
			return "CPC";
		}

		

		// Token: 0x04000045 RID: 69
		private static GameObject drill = new GameObject("drill").AddComponent<Dirll>().gameObject;
	}
}
