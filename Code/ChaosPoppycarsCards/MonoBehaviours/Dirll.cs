using System;
using System.Runtime.CompilerServices;
using UnboundLib.Cards;
using UnityEngine;
using ChaosPoppycarsCards.MonoBehaviours;
using UnboundLib;

namespace ChaosPoppycarsCards.MonoBehaviours
{
	// Token: 0x02000020 RID: 32
	internal class Dirll : MonoBehaviour
	{
		
		private void Update()
		{
			bool flag = base.gameObject.transform.parent != null;
			if (flag)
			{
				ExtensionMethods.GetOrAddComponent<Minning>(base.gameObject.transform.parent.gameObject, false);
				GameObject.Destroy(base.gameObject.transform.parent.gameObject, 1.2f);
				GameObject.Destroy(base.gameObject);
			}
		}
	}
}