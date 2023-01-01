using UnityEngine;
using ModdingUtils.MonoBehaviours;
using UnboundLib;

namespace ChaosPoppycarsCards.MonoBehaviours
{
    internal class InvisEffect : ReversibleEffect
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
            effect.SetColor(Color.clear);
        }

        public override void OnStart()
        {
            gunStatModifier.projectileColor = Color.clear;
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
