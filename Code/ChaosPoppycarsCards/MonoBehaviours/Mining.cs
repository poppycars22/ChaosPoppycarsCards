using System;
using UnityEngine;

namespace ChaosPoppycarsCards.MonoBehaviours
{
	// Token: 0x02000021 RID: 33
	internal class Minning : MonoBehaviour
	{
		// Token: 0x06000084 RID: 132 RVA: 0x00004727 File Offset: 0x00002927
		private void Start()
		{
			this.time = 1f;
			this.scale = base.transform.localScale;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004748 File Offset: 0x00002948
		private void Update()
		{
			this.time -= Time.deltaTime;
			bool flag = this.time < 0f;
			if (flag)
			{
				this.time = 0f;
			}
			base.transform.localScale = this.scale * this.time;
		}

		// Token: 0x04000046 RID: 70
		private float time;

		// Token: 0x04000047 RID: 71
		private Vector3 scale;
	}
}