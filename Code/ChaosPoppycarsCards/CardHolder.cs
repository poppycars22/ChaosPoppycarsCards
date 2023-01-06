using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHolder : MonoBehaviour
{
    public List<GameObject> Cards;
    public List<GameObject> HiddenCards;

    internal void RegisterCards()
    {
        foreach (var Card in Cards)
        {
            //CustomCard.RegisterUnityCard(Card, "CPC", Card.GetComponent<CardInfo>().cardName, true, null);
        }
        foreach (var Card in HiddenCards)
        {
            //CustomCard.RegisterUnityCard(Card, "CPC", Card.GetComponent<CardInfo>().cardName, false, null);
            //ModdingUtils.Utils.Cards.instance.AddHiddenCard(Card.GetComponent<CardInfo>());
        }
    }
}
