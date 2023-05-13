using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using UnboundLib.Cards;
using UnboundLib.Utils;
using UnityEngine;
using BepInEx;
using ChaosPoppycarsCards.Cards;
using ChaosPoppycarsCards.Utilities;
using HarmonyLib;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using ModdingUtils.RoundsEffects;
using System.ComponentModel;
using ModdingUtils.GameModes;
using ModdingUtils.MonoBehaviours;

namespace ChaosPoppycarsCards.MonoBehaviours
{
   public class RainbowLeaf : MonoBehaviour, IGameStartHookHandler, IPointStartHookHandler
    {
        public static float rainbowleafHealth = 0;
        public void Start()
        {
            InterfaceGameModeHooksManager.instance.RegisterHooks(this);
            
            player = this.GetComponentInParent<Player>();
        }

        public void OnPointStart()
        {
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Test] Round Ended");
            player.data.maxHealth += 25;
            player.data.health += 25;
            rainbowleafHealth += 25;
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}]{rainbowleafHealth}");
        }
        public void OnGameStart()
        {
            UnityEngine.GameObject.Destroy(this);
            rainbowleafHealth = 0;
        }
        public void OnDestroy()
        {
            InterfaceGameModeHooksManager.instance.RemoveHooks(this);
            rainbowleafHealth = 0;
        }
        public Player player;
    }
}
