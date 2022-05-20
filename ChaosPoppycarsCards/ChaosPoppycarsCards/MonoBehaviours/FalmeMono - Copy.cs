using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using BepInEx;
using ChaosPoppycarsCards.Utilities;
using HarmonyLib;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;

namespace ChaosPoppycarsCards.MonoBehaviours
{
	public class FlameMono : RayHitEffect
	{

		private void Start()
		{
			if (this.GetComponentInParent<ProjectileHit>() != null)
			{
				this.GetComponentInParent<ProjectileHit>().bulletCanDealDeamage = false;
			}
		}

		public override HasToReturn DoHitEffect(HitInfo hit)
		{
			if (!hit.transform)
			{
				return HasToReturn.canContinue;
			}
			FlameMono[] componentsInChildren = this.transform.root.GetComponentsInChildren<FlameMono>();
			ProjectileHit componentInParent = this.GetComponentInParent<ProjectileHit>();
			DamageOverTime component = hit.transform.GetComponent<DamageOverTime>();
			if (component)
			{
				component.TakeDamageOverTime(componentInParent.damage * this.transform.forward, this.transform.position, this.time, this.interval, new Color(1f, 0.3f, 0f, 1f), null, this.GetComponentInParent<ProjectileHit>().ownWeapon, this.GetComponentInParent<ProjectileHit>().ownPlayer, true);
			}

			return HasToReturn.canContinue;
		}

		public void Destroy()
		{
			UnityEngine.Object.Destroy(this);
		}


		
		public float time = 2f;

		public float interval = 0.1f;

	}
}
