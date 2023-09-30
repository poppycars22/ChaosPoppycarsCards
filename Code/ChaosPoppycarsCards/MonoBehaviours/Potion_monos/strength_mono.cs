using UnityEngine;
using ModdingUtils.MonoBehaviours;
using UnboundLib.Cards;
using UnboundLib;
using ChaosPoppycarsCards.Extensions;
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
            data.block.BlockAction -= OnBlock;
        }
        private void OnBlock(BlockTrigger.BlockTriggerType trigger)
        {
            if (duration <= 0)
            {
                gunStatModifier.damage_mult = 1.5f + (stats.GetAdditionalData().Glowstone * 0.25f);
                ApplyModifiers();
            }
            if (!stats.GetAdditionalData().InvisPot)
            {

                if (ChaosPoppycarsCards.MC_Particles.Value)
                {
                    characterStatModifiersModifier.objectsToAddToPlayer.Add(ChaosPoppycarsCards.Bundle.LoadAsset<GameObject>("PotionMCParticle_Strength"));
                }

            }
            else if (stats.GetAdditionalData().InvisPot && data.view.IsMine && ChaosPoppycarsCards.MC_Particles.Value)
            {
                characterStatModifiersModifier.objectsToAddToPlayer.Add(ChaosPoppycarsCards.Bundle.LoadAsset<GameObject>("PotionMCParticle_Strength"));
            }
            duration = 3f + (stats.GetAdditionalData().Redstone * 1.5f);
            ColorEffect effect = player.gameObject.AddComponent<ColorEffect>();
            effect.SetColor(Color.red);
        }

        public override void OnStart()
        {
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
                Destroy(gameObject.GetOrAddComponent<ColorEffect>());
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
