using System;
using ModdingUtils.MonoBehaviours;
using UnityEngine;

namespace ChaosPoppycarsCards.MonoBehaviours
{
    public class SpeedyHandsBUFFMono : ReversibleEffect
    {
        public override void OnStart()
        {
            movespeed = this.player.data.stats.movementSpeed;
            if (movespeed > 1)
            {
                this.gunAmmoStatModifier.reloadTimeMultiplier_mult *= (1f/(movespeed/2f) * 0.3f )*3f; 
            }
        }

        public override void OnOnDisable()
        {
            Destroy();
        }

        public float movespeed;

    }
}