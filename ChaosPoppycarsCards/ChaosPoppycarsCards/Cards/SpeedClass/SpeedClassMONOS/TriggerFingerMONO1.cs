using System;
using ModdingUtils.MonoBehaviours;
using UnityEngine;

namespace ChaosPoppycarsCards.MonoBehaviours
{
    public class TriggerFingerBUFFMono : ReversibleEffect
    {
        public override void OnStart()
        {
            movespeed = this.player.data.stats.movementSpeed;
            if (movespeed > 1)
            {
                this.gunStatModifier.attackSpeed_mult *= (1f/(movespeed/2.5f) * 0.5f )*1.5f; 
            }
        }

        public override void OnOnDisable()
        {
            Destroy();
        }

        public float movespeed;

    }
}