using UnityEngine;
using BepInEx;
using UnboundLib;
using UnboundLib.Cards;
using ChaosPoppycarsCards;
using ChaosPoppycarsCards.Cards;
using ChaosPoppycarsCards.Cards.Minecrafter;
using HarmonyLib;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using ModdingUtils;
using ModdingUtils.Extensions;
using System.Collections;
using UnboundLib.GameModes;
using Jotunn.Utils;
using System.Linq;
using System.Collections.ObjectModel;
using System;
using WillsWackyManagers.Utils;
using RarityLib.Utils;
using System.Collections.Generic;

public class CardHolder : MonoBehaviour
{
    public List<GameObject> Cards;
    public List<GameObject> HiddenCards;

        internal void RegisterCards()
    {
        foreach (var Card in Cards)
        {
            CustomCard.RegisterUnityCard(Card, ChaosPoppycarsCards.ChaosPoppycarsCards.ModInitials, Card.GetComponent<CardInfo>().cardName, true, null);
        }
        foreach (var Card in HiddenCards)
        {
            //CustomCard.RegisterUnityCard(Card, "CPC", Card.GetComponent<CardInfo>().cardName, false, null);
            //ModdingUtils.Utils.Cards.instance.AddHiddenCard(Card.GetComponent<CardInfo>());
        }
    }
}
