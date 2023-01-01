using System;
using System.Linq;
using ModdingUtils.RoundsEffects;
using Sonigon;
using Sonigon.Internal;
using UnityEngine;
using WWMO.MonoBehaviours;

namespace ChaosPoppycarsCards.MonoBehaviours
{
	// Token: 0x02000010 RID: 16
	public class BurnMono : HitSurfaceEffect
	{
		// Token: 0x06000043 RID: 67 RVA: 0x000035DE File Offset: 0x000017DE
		private void Awake()
		{
			this.player = base.GetComponent<Player>();
			this.block = base.GetComponent<Block>();
			this.data = base.GetComponent<CharacterData>();
			this.gun = base.GetComponent<Gun>();
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003610 File Offset: 0x00001810
		public override void Hit(Vector2 position, Vector2 normal, Vector2 velocity)
		{
			
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(((GameObject)Resources.Load("0 cards/Demonic pact")).GetComponent<Gun>().objectsToSpawn[0].effect);
			gameObject.transform.position = position;
			gameObject.hideFlags = HideFlags.HideAndDontSave;
			gameObject.name = "Explosion";
			gameObject.AddComponent<RemoveAfterSeconds>().seconds = 5f;
			ParticleSystem[] componentsInChildren = gameObject.GetComponentsInChildren<ParticleSystem>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].startColor = new Color(1f, 0.8f, 0.1f, 1f);
			}
			foreach (ParticleSystemRenderer particleSystemRenderer in gameObject.GetComponentsInChildren<ParticleSystemRenderer>())
			{
				particleSystemRenderer.material.color = new Color(1f, 0.8f, 0.1f, 1f);
				particleSystemRenderer.sharedMaterial.color = new Color(1f, 0.8f, 0.1f, 1f);
			}
			Material[] componentsInChildren3 = gameObject.GetComponentsInChildren<Material>();
			for (int i = 0; i < componentsInChildren3.Length; i++)
			{
				componentsInChildren3[i].color = new Color(1f, 0.8f, 0.1f, 1f);
			}
			foreach (Collider2D collider2D in from uwu in Physics2D.OverlapCircleAll(position, 3f)
											  where !uwu.gameObject.GetComponentInParent<ProjectileHit>() && !uwu.gameObject.GetComponent<Player>() && uwu.GetComponent<Rigidbody2D>()
											  select uwu)
			{
				if (!collider2D.attachedRigidbody.isKinematic && collider2D.attachedRigidbody.gameObject.layer != LayerMask.NameToLayer("BackgroundObject"))
				{
					if (collider2D.attachedRigidbody.GetComponent<DamagableEvent>())
					{
						collider2D.attachedRigidbody.GetComponent<DamagableEvent>().CallTakeDamage(Vector2.up * 2f, Vector2.zero, null, null, true);
					}
					else
					{
						if (!collider2D.attachedRigidbody.gameObject.GetComponent<BoxTouchingLava_Mono>())
						{
							this.hotbox = collider2D.attachedRigidbody.gameObject.AddComponent<BoxTouchingLava_Mono>();
						}
						else
						{
							this.hotbox = collider2D.attachedRigidbody.gameObject.GetComponent<BoxTouchingLava_Mono>();
						}
						this.hotbox.heatPercent += 0.5f;
						this.hotbox.heatPercent = Mathf.Min(this.hotbox.heatPercent, 3f);
					}
				}
			}
		}

		// Token: 0x0400003E RID: 62
		public Block block;

		// Token: 0x0400003F RID: 63
		public Player player;

		// Token: 0x04000040 RID: 64
		public CharacterData data;

		// Token: 0x04000041 RID: 65
		public Gun gun;

		// Token: 0x04000042 RID: 66
		public BoxTouchingLava_Mono hotbox;

		// Token: 0x04000043 RID: 67
		
	}
}
