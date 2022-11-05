using ClassesManagerReborn;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using UnboundLib.Cards;
using UnboundLib.GameModes;
using ChaosPoppycarsCards.Cards.Minecrafter;

namespace ChaosPoppycarsCards.Cards
{
    class SpeedClass : ClassHandler
    {
        internal static string name = "Speedster";

        public override IEnumerator Init()
        {
            UnityEngine.Debug.Log("Regestering: " + name);
            while (!(SpeedDemon.Card && MomentumShots.Card && SpeedyHands.Card && Tricky.Card && TriggerFinger.Card && Swifter.Card && Stretches.Card && LegDay.Card && SpeedstersGun.Card && AirHops.Card)) yield return null;
            ClassesRegistry.Register(SpeedDemon.Card, CardType.Entry);
            ClassesRegistry.Register(MomentumShots.Card, CardType.Card, SpeedDemon.Card);
            ClassesRegistry.Register(SpeedyHands.Card, CardType.Card, SpeedDemon.Card);
            ClassesRegistry.Register(Tricky.Card, CardType.Card, SpeedDemon.Card);
            ClassesRegistry.Register(TriggerFinger.Card, CardType.Card, SpeedDemon.Card);
            ClassesRegistry.Register(Swifter.Card, CardType.Card, SpeedDemon.Card);
            ClassesRegistry.Register(Stretches.Card, CardType.Card, SpeedDemon.Card);
            ClassesRegistry.Register(LegDay.Card, CardType.Card, SpeedDemon.Card);
            ClassesRegistry.Register(SpeedstersGun.Card, CardType.Card, SpeedDemon.Card);
            ClassesRegistry.Register(AirHops.Card, CardType.Card, SpeedDemon.Card);
        }
        public override IEnumerator PostInit()
        {
            ClassesRegistry.Get(SpeedyHands.Card).Blacklist(TriggerFinger.Card);
            ClassesRegistry.Get(TriggerFinger.Card).Blacklist(SpeedyHands.Card);
            yield break;
        }
    }
}