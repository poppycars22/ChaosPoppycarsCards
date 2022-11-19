using ClassesManagerReborn;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using UnboundLib.Cards;
using UnboundLib.GameModes;
using ChaosPoppycarsCards.Cards;

namespace ChaosPoppycarsCards.Cards
{
    class GeeseThing : ClassHandler
    {
        

        public override IEnumerator Init()
        {
            
            while (!(GeeseSwarm.Card && Goose.Card)) yield return null;
            ClassesRegistry.Register(GeeseSwarm.Card, CardType.Entry | CardType.NonClassCard);
            ClassesRegistry.Register(Goose.Card, CardType.Card | CardType.NonClassCard, GeeseSwarm.Card);
            
        }
        
    }
}