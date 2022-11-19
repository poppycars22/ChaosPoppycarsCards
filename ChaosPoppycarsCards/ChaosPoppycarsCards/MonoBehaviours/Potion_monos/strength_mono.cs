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
    internal class StrengthEffect : ReversibleEffect
    {
        private float duration = 0;
        public override void OnOnDestroy()
        {
            block.BlockAction = (Action<BlockTrigger.BlockTriggerType>)Delegate.Remove(block.BlockAction, new Action<BlockTrigger.BlockTriggerType>(OnBlock));
        }
        private void OnBlock(BlockTrigger.BlockTriggerType trigger)
        {
            if (duration <= 0)
            {
                ApplyModifiers();
            }
            duration = 5f;
            ColorEffect effect = player.gameObject.AddComponent<ColorEffect>();
            effect.SetColor(Color.red);
        }

        public override void OnStart()
        {
            gunStatModifier.damage_mult = 2f;
            block.BlockAction = (Action<BlockTrigger.BlockTriggerType>)Delegate.Combine(block.BlockAction, new Action<BlockTrigger.BlockTriggerType>(OnBlock));
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
        }
    }
}
