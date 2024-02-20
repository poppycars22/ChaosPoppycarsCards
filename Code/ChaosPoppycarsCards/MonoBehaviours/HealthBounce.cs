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
using ChaosPoppycarsCards.Extensions;
using Photon.Pun;
using SimulationChamber;

namespace ChaosPoppycarsCards.MonoBehaviours
{
    class HealthBounce : BounceEffect
    {
        public void Start()
        {
            InterfaceGameModeHooksManager.instance.RegisterHooks(this);
            player = GetComponentInParent<ProjectileHit>().ownPlayer;
            characterStatModifiers = player.GetComponentInParent<CharacterStatModifiers>();
        }
        private void Awake()
        {
            gameObject.AddComponent<BounceTrigger>();
        }
        public override void DoBounce(HitInfo hit)
        {
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Test] Bullet Has hit");
            player.data.maxHealth += 1f;
            player.data.health += 1f;
            characterStatModifiers.GetAdditionalData().HealthBouncesBounced += 1f;
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
        public CharacterStatModifiers characterStatModifiers;
    }
    class HealthBounceMono : MonoBehaviour, IRoundEndHookHandler
    {
        public Player player;
        public CharacterStatModifiers characterStatModifiers;
        public void Start()
        {
            InterfaceGameModeHooksManager.instance.RegisterHooks(this);
            player = GetComponentInParent<Player>();
            characterStatModifiers = GetComponentInParent<CharacterStatModifiers>();

        }
        public void OnRoundEnd()
        {
            player.data.maxHealth -= characterStatModifiers.GetAdditionalData().HealthBouncesBounced;
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Test] Round Ended");
            characterStatModifiers.GetAdditionalData().HealthBouncesBounced = 0f;
        }
        /*public void OnPointEnd()
        {
            player.data.maxHealth -= characterStatModifiers.GetAdditionalData().HealthBouncesBounced;
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Test] Point End");
            characterStatModifiers.GetAdditionalData().HealthBouncesBounced = 0f;
        }*/
        public void OnGameStart()
        {
            UnityEngine.GameObject.Destroy(this);
        }
        public void OnDestroy()
        {
            InterfaceGameModeHooksManager.instance.RemoveHooks(this);
        }
    }
}
