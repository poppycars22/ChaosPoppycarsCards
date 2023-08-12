using UnityEngine;
using HarmonyLib;
using ModdingUtils.MonoBehaviours;
using CPC.Extensions;
using UnboundLib;

namespace ChaosPoppycarsCards.MonoBehaviours
{
    public class TotemEffect : ReversibleEffect
    {
        ColorFlash colorFlash = null;
        HealthHandler healthHandler;
        public override void OnStart()
        {
            base.SetLivesToEffect(int.MaxValue);
            healthHandler = player.data.healthHandler;
        }
      
        public void UseMulligan()
        {
            // if there are no mulligans left, just return
            if (base.stats.GetAdditionalData().remainingTotems <= 0)
            {
                return;
            }

            // force the player to block (for free)
            //base.block.CallDoBlock(false, true, BlockTrigger.BlockTriggerType.Default);
            ChaosPoppycarsCards.Instance.ExecuteAfterFrames(5, () =>
            {
                //healthHandler.Heal((player.data.maxHealth / 4));
                this.player.gameObject.GetOrAddComponent<TotemRegenEffect>();
            });


            // stop DoT effects
            ((DamageOverTime)Traverse.Create(base.health).Field("dot").GetValue()).StopAllCoroutines();
            this.colorFlash = base.player.gameObject.GetOrAddComponent<ColorFlash>();
            this.colorFlash.SetNumberOfFlashes(1);
            this.colorFlash.SetDuration(0.25f);
            this.colorFlash.SetDelayBetweenFlashes(0.25f);
            this.colorFlash.SetColorMax(Color.white);
            this.colorFlash.SetColorMin(Color.white);

            // use up a single mulligan
            base.stats.GetAdditionalData().remainingTotems--;
        }
        public override void OnOnDestroy()
        {
        }
    }

}