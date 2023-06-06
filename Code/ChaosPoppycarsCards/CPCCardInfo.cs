using System.Linq;
using Nullmanager;
using TMPro;
using UnityEngine;
using UnboundLib;
using CPC.Extensions;
using HarmonyLib;


namespace CPCCardInfostuffs
{
    public class CPCCardInfo : MonoBehaviour
    {
        [Header("CPC Settings")]
        //public bool Nullable = true;
        public float GunCritDamage2 = 0f;
        public float GunCritChance2 = 0f;
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


            UnityEngine.Debug.Log($"[CPCCardInfo Test GunCritDamage {GunCritDamage2}, GunCritChance {GunCritChance2} ]");
        }
    }
    
    
   
}
