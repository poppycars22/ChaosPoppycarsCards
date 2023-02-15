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
    class HealthBounce1 : BounceEffect, IRoundEndHookHandler, IPickStartHookHandler
    {
        private float timesHit = 0;
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
            player.data.maxHealth += 0.5f;
            player.data.health += 0.5f;
            timesHit += 0.5f;
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Test] {timesHit} times hit");
        }
        
        public void OnRoundEnd()
        {
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Test] Round Ended");
            player.data.maxHealth -= timesHit;
            player.data.health -= timesHit;
            timesHit *= 0;
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Test] {timesHit} times hit");
        }
        public void OnPickStart()
        {
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Test] Pick Started");
            player.data.maxHealth -= timesHit;
            player.data.health -= timesHit;
            timesHit *= 0;
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Test] {timesHit} times hit");
        }
        public void OnGameStart()
        {
            UnityEngine.GameObject.Destroy(this);
        }
        public Player player;
    }
}
