using UnityEngine;
using ModdingUtils.MonoBehaviours;
using UnboundLib.Cards;
using UnboundLib;
using ModdingUtils.Extensions;
using System;
using System.Collections.Generic;
using ChaosPoppycarsCards.Cards;

namespace ChaosPoppycarsCards.MonoBehaviours
{
    internal class TestMono : ReversibleEffect
    {
        
        public override void OnUpdate()
        {
            if (gun.projectileSpeed == 0f)
            {
                gun.projectileSpeed = 1f;
            }
            if (gun.projectileSpeed != 1f)
            {
                gun.projectileSpeed *= 0f;
            }
            if (gun.projectielSimulatonSpeed == 0f)
            {
                gun.projectielSimulatonSpeed = 1f;
            }
            if (gun.projectielSimulatonSpeed != 1f)
            {
                gun.projectielSimulatonSpeed *= 0f;
            }
        }
        public override void OnLateUpdate()
        {
            if (gun.projectileSpeed == 0f)
            {
                gun.projectileSpeed = 1f;
            }
            if (gun.projectileSpeed != 1f)
            {
                gun.projectileSpeed *= 0f;
            }
            if (gun.projectielSimulatonSpeed == 0f)
            {
                gun.projectielSimulatonSpeed = 1f;
            }
            if (gun.projectielSimulatonSpeed != 1f)
            {
                gun.projectielSimulatonSpeed *= 0f;
            }
        }
    }
}
