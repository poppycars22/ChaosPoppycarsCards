﻿using UnityEngine;
using ModdingUtils.MonoBehaviours;
using UnboundLib.Cards;
using UnboundLib;
using ModdingUtils.Extensions;
using System;
using System.Collections.Generic;
using ChaosPoppycarsCards.Cards;
using On;
using IL;

namespace ChaosPoppycarsCards.MonoBehaviours
{
    internal class JumpBurstEffect : ReversibleEffect
    {
        private float duration = 0;
        public override void OnOnDestroy()
        {
            data.jump.JumpAction -= OnJump;
        }
        private void OnJump()
        {
            if (duration <= 0)
            {
                ApplyModifiers();
            }
            duration = 0.65f;
            
            ColorEffect effect = player.gameObject.AddComponent<ColorEffect>();
            effect.SetColor(new Color(0.6f, 0f, 0.923f));
        }

        public override void OnStart()
        {

            gunStatModifier.bursts_add = 10;
            gunStatModifier.timeBetweenBullets_add = 0.3f;
            data.jump.JumpAction += OnJump;
            SetLivesToEffect(int.MaxValue);
        }
        public override void OnUpdate()
        {
            if (!(duration <= 0))
            {
                duration -= TimeHandler.deltaTime;
            }
            else
            {
                ClearModifiers();
                Destroy(gameObject.GetOrAddComponent<ColorEffect>());
            }
        }
        public override void OnOnDisable()
        {
            duration = 0;
            ClearModifiers();
            Destroy(gameObject.GetOrAddComponent<ColorEffect>());
        }
    }
}
