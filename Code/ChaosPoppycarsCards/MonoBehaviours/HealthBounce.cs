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

namespace ChaosPoppycarsCards.MonoBehaviours
{
    class HealthBounce : BounceEffect
    {
        
        public void Start()
        {
            InterfaceGameModeHooksManager.instance.RegisterHooks(this);
            player = GetComponentInParent<ProjectileHit>().ownPlayer;

        }
        private void Awake()
        {
            gameObject.AddComponent<BounceTrigger>();
        }
        public override void DoBounce(HitInfo hit)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Test] Bullet Has hit");
            player.data.maxHealth += 0.25f;
            player.data.health += 0.25f;
           
            
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Test] {player.data.maxHealth} max hp");
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Test] {player.data.health} hp");
        }


       
        public void OnGameStart()
        {
            UnityEngine.GameObject.Destroy(this);
        }
        public void OnDestroy()
        {
            InterfaceGameModeHooksManager.instance.RemoveHooks(this);
        }
        public Player player;
    }
}
