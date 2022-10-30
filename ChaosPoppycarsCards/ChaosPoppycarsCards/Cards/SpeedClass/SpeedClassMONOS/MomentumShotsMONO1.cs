using System;
using ModdingUtils.MonoBehaviours;
using UnityEngine;

namespace ChaosPoppycarsCards.MonoBehaviours
{
    public class WarpathBuffMono : ReversibleEffect
    {
        public override void OnStart()
        {
            movespeed = this.player.data.stats.movementSpeed;
            if (movespeed > 1)
            {
                this.gunStatModifier.damage_add += ((movespeed - 1f) * 1.3f + 1f)/6f;
            }
        }

        public override void OnOnDisable()
        {
            Destroy();
        }

        public float movespeed;

    }
}