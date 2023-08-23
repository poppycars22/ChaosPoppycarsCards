﻿using UnityEngine;
using ModdingUtils.MonoBehaviours;
using UnboundLib;
namespace ChaosPoppycarsCards.MonoBehaviours
{
    internal class JumpEffect : ReversibleEffect
    {
        private float duration = 0;
        
        public override void OnOnDestroy()
        {
            data.block.BlockAction -= OnBlock;
        }
        private void OnBlock(BlockTrigger.BlockTriggerType trigger)
        {
            if (duration <= 0)
            {
                ApplyModifiers();
            }
            duration = 5f;
            ColorEffect effect = player.gameObject.AddComponent<ColorEffect>();
            effect.SetColor(Color.green);
        }
        public override void OnStart()
        {
            characterStatModifiersModifier.jump_add = 0.5f;
            gravityModifier.gravityForce_mult = 0.5f;
            characterDataModifier.numberOfJumps_add = 3;
            if (ChaosPoppycarsCards.MC_Particles.Value)
            {
                characterStatModifiersModifier.objectsToAddToPlayer.Add(ChaosPoppycarsCards.Bundle.LoadAsset<GameObject>("PotionMCParticle_Jump"));
            }
            data.block.BlockAction += OnBlock;
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
                UnityEngine.GameObject.Destroy(this.gameObject.GetOrAddComponent<ColorEffect>());
            }
        }
        public override void OnOnDisable()
        {
            duration = 0;
            Destroy(gameObject.GetOrAddComponent<ColorEffect>());
            ClearModifiers();
        }
    }
}
