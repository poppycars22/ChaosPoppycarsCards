using UnityEngine;
using ModdingUtils.MonoBehaviours;
using UnboundLib;
namespace ChaosPoppycarsCards.MonoBehaviours
{
    internal class RegenEffect : ReversibleEffect
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
            duration = 3f;
            ColorEffect effect = player.gameObject.AddComponent<ColorEffect>();
            effect.SetColor(Color.magenta);
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
                data.healthHandler.Heal(20f * TimeHandler.deltaTime);
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
