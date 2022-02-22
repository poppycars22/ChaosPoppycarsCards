using UnityEngine;
using ModdingUtils.MonoBehaviours;
using UnboundLib.Cards;
using UnboundLib;
using ModdingUtils.Extensions;
using System;
using System.Collections.Generic;
using ChaosPoppycarsCards.Cards;
using PlayerJumpPatch;

namespace ChaosPoppycarsCards.MonoBehaviours
{
    internal class BouncyMono : ReversibleEffect
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
        }
        public override void OnStart()
        {
            gunStatModifier.reflects_mult = 3;
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

            }
        }
    }
}
