using UnityEngine;
using ModdingUtils.MonoBehaviours;
using UnboundLib.Cards;
using UnboundLib;
using ModdingUtils.Extensions;
using System;
using System.Collections.Generic;
using ChaosPoppycarsCards.Cards;
using PlayerJumpPatch;
using System.Collections;
using UnboundLib.GameModes;
using ModdingUtils.GameModes;
using System.Linq;

namespace ChaosPoppycarsCards.MonoBehaviours
{
    

    public class LightsaberRange : ReversibleEffect, IPointEndHookHandler, IPointStartHookHandler, IGameStartHookHandler
    {

        public void Start()
        {
            InterfaceGameModeHooksManager.instance.RegisterHooks(this);
            data = GetComponentInParent<CharacterData>();
            this.applyImmediately = false;
            this.SetLivesToEffect(int.MaxValue);

        }
        public void OnPointStart()
        {
            float Thing = gun.projectileSpeed - 1;
            float Thing2 = gun.projectielSimulatonSpeed - 1;
            float Thing3 = gun.destroyBulletAfter - 0.066f;
            this.gunStatModifier.projectileSpeed_add = -Thing;
            this.gunStatModifier.projectielSimulatonSpeed_add = -Thing2;
            this.gunStatModifier.destroyBulletAfter_add = -Thing3;
            this.ApplyModifiers();
        }
        public void OnPointEnd()
            {

            this.ClearModifiers();
        }
        public void OnGameStart()
        {
            UnityEngine.GameObject.Destroy(this);
        }

        public override void OnOnDestroy()
        {
            InterfaceGameModeHooksManager.instance.RemoveHooks(this);
        }


       
    }
}
