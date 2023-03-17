﻿using System.Linq;
using ModdingUtils.Extensions;
using Nullmanager;
using TMPro;
using UnityEngine;
using UnboundLib;

public class CPCCardInfo : MonoBehaviour
{
    [Header("CPC Settings")]
    //public bool Nullable = true;
    //public bool NeedsNull = false; 
    public string Tag = "CPC";

   

    /*public void Setup()
    {
        if (!Nullable)
            GetComponent<CardInfo>().MarkUnNullable();
        if (NeedsNull)
            GetComponent<CardInfo>().NeedsNull();
    }*/

    public void Start()
    {
        RectTransform[] allChildrenRecursive = gameObject.GetComponentsInChildren<RectTransform>();
        GameObject modNameObj = new GameObject("ModIDText");
        var edgeTransform = allChildrenRecursive.FirstOrDefault(obj => obj.gameObject.name == "EdgePart (2)");
        if (edgeTransform != null)
        {
            GameObject bottomLeftCorner = edgeTransform.gameObject;
            modNameObj.gameObject.transform.SetParent(bottomLeftCorner.transform);
        }

        var modText = modNameObj.gameObject.AddComponent<TextMeshProUGUI>();
        modText.text = Tag;
        modText.autoSizeTextContainer = true;
        modNameObj.transform.localEulerAngles = new Vector3(0f, 0f, 135f);

        modNameObj.transform.localScale = Vector3.one;
        modNameObj.transform.localPosition = new Vector3(-75f, -75f, 0f);
        modText.alignment = TextAlignmentOptions.Bottom;
        modText.alpha = 0.1f;
        modText.fontSize = 54;
        

       



    }
}