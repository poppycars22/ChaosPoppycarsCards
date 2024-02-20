using Sonigon.Internal;
using Sonigon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib.GameModes;
using UnboundLib.Networking;
using UnboundLib;
using UnityEngine;
using InControl;
using ChaosPoppycarsCards.Lightsaber.Extensions;
using UnityEngine.EventSystems;
using SoundImplementation;
using UnityEngine.Events;
//using PlayerActionsHelper.Extensions;
using UnityEditor;
using System.Runtime.Serialization;

namespace ChaosPoppycarsCards.Lightsaber
{
    public class A_HoldableObject : MonoBehaviour
    {
        internal static IEnumerator RemoveAllHoldableObjects(IGameModeHandler gm)
        {
            PlayerManager.instance.ForEachPlayer(p =>
            {
                // hide knife just to be sure
                try
                {
                    A_HoldableObject.MakeGunHoldable(p.playerID, false);
                }
                catch { }
            });
            yield break;
        }
        public static GameObject weapon;

        public UnityEvent eventTrigger;

        public bool IsOut { get; private set; } = false;
        
        public const float Volume = 0.5f;
        
        internal const float SwitchDelay = 0.25f;

        public float switchTimer { get; private set; } = 0f;
        
        private float StabTimer = 0f;

        private Player Player;

        private bool wasOut = false;

        void Start()
        {
            this.IsOut = false;
            this.StabTimer = 0f;
            this.switchTimer = 0f;
            this.Player = this.GetComponentInParent<Player>();   
        }

        void OnDisable()
        {
            if (this.Player is null || !this.Player.data.view.IsMine) { return; }
            if (this.IsOut && !this.wasOut)
            {
                this.IsOut = false;
                this.wasOut = true;
                NetworkingManager.RPC(typeof(A_HoldableObject), nameof(RPCA_Switch_To_Holdable), this.Player.playerID, false);
            }
        }
        void OnEnable()
        {
            if (this.Player is null || !this.Player.data.view.IsMine) { return; }
            if (!this.IsOut && this.wasOut)
            {
                this.IsOut = true;
                this.wasOut = false;
                NetworkingManager.RPC(typeof(A_HoldableObject), nameof(RPCA_Switch_To_Holdable), this.Player.playerID, true);
            }
        }

        void Update()
        {
            if (this.Player is null || !this.Player.data.view.IsMine && this.Player.data.dead) { return; }
            this.StabTimer -= TimeHandler.deltaTime;
            this.switchTimer -= TimeHandler.deltaTime;
            if (this.switchTimer <= 0f /*&& this.Player.data.playerActions.ActionWasPressed("SwitchHoldable")*/)
            {
                this.switchTimer = this.IsOut ? 0f : SwitchDelay;
                this.IsOut = !this.IsOut;
                
                if (this.IsOut)
                {
                    eventTrigger.Invoke();
                }
                NetworkingManager.RPC(typeof(A_HoldableObject), nameof(RPCA_Switch_To_Holdable), this.Player.playerID, this.IsOut);
            }
        }
        [UnboundRPC]
        internal static void RPCA_Switch_To_Holdable(int stabbingPlayerID, bool holdableObj)
        {
            MakeGunHoldable(stabbingPlayerID, holdableObj);
        }
        static void MoveToHide(Transform transform, bool hide)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, hide ? -10000f : 0f);
        }
        internal static void MakeGunHoldable(int playerID, bool holdableObj)
        {
            Player player = PlayerManager.instance.players.FirstOrDefault(p => p.playerID == playerID);
            if (player is null) { return; }
            A_HoldableObject HoldableObjHandler = player.GetComponentInChildren<A_HoldableObject>();
            Gun gun = player.GetComponent<Holding>().holdable.GetComponent<Gun>();
            GameObject springObj = gun.transform.Find("Spring").gameObject;
            RightLeftMirrorSpring spring = springObj.transform.GetChild(2).GetComponent<RightLeftMirrorSpring>();
            GameObject HoldableObj = springObj.transform.Find($"Lightsaber(Clone)")?.gameObject;
            if (HoldableObj is null)
            {
                HoldableObj = Instantiate(ChaosPoppycarsCards.ArtAssets.LoadAsset<GameObject>($"Lightsaber"), springObj.transform);
                ObjectMirror objMirror = HoldableObj.GetOrAddComponent<ObjectMirror>();
                ObjectSlash objStab = HoldableObj.GetOrAddComponent<ObjectSlash>();
                objStab.mirror = objMirror;
                GameObject bladeObj = HoldableObj.transform.Find("Blade").gameObject;
                ObjectCollider objCollider = bladeObj.transform.Find("Collider").gameObject.GetOrAddComponent<ObjectCollider>();
                BladeColor blade = bladeObj.GetOrAddComponent<BladeColor>();
                blade.playerID = playerID;
            }

            HoldableObj.SetActive(holdableObj);

            MoveToHide(springObj.transform.Find("Ammo/Canvas"), holdableObj);
            MoveToHide(springObj.transform.GetChild(2), holdableObj);
            MoveToHide(springObj.transform.GetChild(3), holdableObj);
            springObj.transform.GetChild(2).GetComponent<RightLeftMirrorSpring>().enabled = !holdableObj;
            springObj.transform.GetChild(3).GetComponent<RightLeftMirrorSpring>().enabled = !holdableObj;

            gun.GetData().disabled = holdableObj;
        }
        private void OnDestroy()
        {
            if (Player is null) return;
            RPCA_Switch_To_Holdable(Player.playerID, false);
            Gun gun = Player.GetComponent<Holding>().holdable.GetComponent<Gun>();
            GameObject springObj = gun.transform.Find("Spring").gameObject;
            GameObject HoldableObj = springObj.transform.Find($"Lightsaber(Clone)")?.gameObject;
            if (HoldableObj is null)
                return;

            UnityEngine.GameObject.Destroy(HoldableObj);
        }
    }
}
