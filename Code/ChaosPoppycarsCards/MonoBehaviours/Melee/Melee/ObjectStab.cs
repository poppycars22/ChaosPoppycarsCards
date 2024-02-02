using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib.Networking;
using UnboundLib;
using UnboundLib.Utils;
using UnityEngine;
using ChaosPoppycarsCards.Lightsaber.Extensions;
using System.Collections;

namespace ChaosPoppycarsCards.Lightsaber
{
    /*class ObjectStab : MonoBehaviour
    {
        public static string ObjectName;

        private A_HoldableObject HoldableObj;
        private const float StabDuration = 0.25f;
        private float StabTimer = 0f;
        Holdable holdable;
        Player Player;
        GeneralInput Input;

        void OnEnable()
        {
            this.Player = this.transform.root.GetComponent<Holdable>().holder.GetComponent<Player>();
            this.HoldableObj = this.Player.GetComponentInChildren<A_HoldableObject>();
            this.Input = this.Player.data.input;
        }
        void DoStab()
        {
            if (!this.HoldableObj.IsOut) { return; }
            this.StabTimer = StabDuration;
            this.DoStabAnim();
            if (this.Player.data.view.IsMine)
            {
                NetworkingManager.RPC_Others(typeof(ObjectStab), nameof(RPCO_DoStabAnim), this.Player.playerID);
            }
        }
        [UnboundRPC]
        private static void RPCO_DoStabAnim(int playerID)
        {
            Player stabber = PlayerManager.instance.GetPlayerWithID(playerID);
            if (stabber is null) { return; }
            ObjectStab stabObject = stabber.data.weaponHandler.gun.transform.GetChild(1).Find($"Lightsaber(Clone)").GetComponent<ObjectStab>();
            stabObject.DoStabAnim();
        }
        internal void DoStabAnim()
        {
            if (this.holdable is null) { this.holdable = base.transform.root.GetComponent<Holdable>(); }
            bool left = this.transform.root.position.x - 0.1f < this.holdable.holder.transform.position.x;
            this.transform.root.position += (-4f * this.transform.right + (left ? 3f : -3f) * this.transform.up) / 5f;
        }
        void Update()
        {
            this.StabTimer -= TimeHandler.deltaTime;
            if (this.Player.data.view.IsMine /*&& this.HoldableObj.SwitchTimer <= 0f*/
    /* && this.StabTimer <= 0f && this.Input.shootWasPressed && this.HoldableObj.IsOut)
            {
                this.DoStab();
            }
        }
        internal void TryStab(Collider2D collider2D)
        {
            if (this.StabTimer <= 0f || !this.Player.data.view.IsMine) { return; }
            if (collider2D?.GetComponent<Player>() != null
                && !collider2D.GetComponent<Player>().data.dead
                && collider2D.GetComponent<Player>().playerID != this.Player.playerID)
            {
                //HoldableObj.HasStabbed = true;
                NetworkingManager.RPC(typeof(ObjectStab), nameof(RPCA_StabPlayer),
                    this.Player.playerID, collider2D.GetComponent<Player>().playerID);
            }
        }
        [UnboundRPC]
        private static void RPCA_StabPlayer(int stabbingPlayerID, int stabbedPlayerID)
        {
            if (stabbedPlayerID == -1) { return; }
            Player stabbingPlayer = PlayerManager.instance.players.FirstOrDefault(p => p.playerID == stabbingPlayerID);
            Player stabbedPlayer = PlayerManager.instance.players.FirstOrDefault(p => p.playerID == stabbedPlayerID);
            if (stabbedPlayer is null || stabbingPlayer is null) { return; }
            stabbedPlayer.data.healthHandler.DoDamage(stabbingPlayer.data.weaponHandler.gun.damage * stabbingPlayer.data.weaponHandler.gun.shootPosition.forward,
                stabbedPlayer.transform.position, Color.white,
                stabbingPlayer.data?.weaponHandler?.gun?.transform?.GetChild(1)?.Find($"Lightsaber(Clone)")?.gameObject,
                stabbingPlayer, false, true, true);
        }
    }*/
    class ObjectSlash : MonoBehaviour
    {
        private A_HoldableObject holdableObj;
        private ObjectCollider objCollider;
        private const float slashAnimDuration = 0.25f;
        private const float slashCooldown = slashAnimDuration + 0.05f;
        private float sinceSlash = slashCooldown;
        private bool canDoDamage = false;
        private bool slashing = false;
        private Holdable holdable;
        internal Player player;
        private GeneralInput input;
        internal ObjectMirror mirror;
        private static AnimationCurve[] slashCurves = new AnimationCurve[] {
            new AnimationCurve(new Keyframe[] { // X-Axis, which is Y-Axis due to rotation
                new Keyframe(0,0,1,1,1,1),
                new Keyframe(1f/16f, 0.1f,1,1,1,1),
                new Keyframe(1f/4f, 0.35f,2,2,1,1),
                new Keyframe(2f/3f, 0f,1,1,1,1),
                new Keyframe(5f/6f, -0.2f,1,1,1,1),
                new Keyframe(7f/8f, -0.1f,1,1,1,1),
                new Keyframe(1, 0,1,1,1,1)
            }),
            new AnimationCurve(new Keyframe[] { // Y-Axis, which is X-Axis due to rotation
                new Keyframe(0,0,1,1,1,1),
                new Keyframe(1f/16f, -0.15f,1,1,1,1),
                new Keyframe(1f/4f, 0.2f,1,1,1,1),
                new Keyframe(2f/3f, 0.4f,1,1,1,1),
                new Keyframe(5f/6f, 0.25f,1,1,1,1),
                new Keyframe(7f/8f, -0.05f,1,1,1,1),
                new Keyframe(1, 0,1,1,1,1)
            }),
            new AnimationCurve(new Keyframe[] { // Rotation
                new Keyframe(0,0,1,1,1,1),
                new Keyframe(1f/16f, 5f,1,1,1,1),
                new Keyframe(1f/4f, 40f,1,1,1,1),
                new Keyframe(2f/3f, 0f,1,1,1,1),
                new Keyframe(5f/6f, -20f,1,1,1,1),
                new Keyframe(7f/8f, -10f,1,1,1,1),
                new Keyframe(1, 0f,1,1,1,1)
            })
        };
        
        //public bool IsBlade => this.holdableObj.IsBlade;

       // private float sinceBlade = slashCooldown;

        void Start()
        {   
            this.objCollider = this.gameObject.transform.Find("Blade").GetComponentInChildren<ObjectCollider>();
        }

        void OnEnable()
        {
            this.player = this.transform.root.GetComponent<Holdable>().holder.GetComponent<Player>();
            this.holdableObj = this.player.GetComponentInChildren<A_HoldableObject>();
            this.input = this.player.data.input;
            this.slashing = false;
        }

        void DoSlash()
        {
            if (!this.holdableObj.IsOut /*&& !this.IsBlade*/) { return; }
            this.sinceSlash = 0f;
            this.canDoDamage = true;
            this.DoSlashAnim();
            if (this.player.data.view.IsMine)
                NetworkingManager.RPC_Others(typeof(ObjectSlash), nameof(RPCO_DoSlashAnim), this.player.playerID);
        }
        [UnboundRPC]
        private static void RPCO_DoSlashAnim(int playerID)
        {
            Player slasher = PlayerManager.instance.GetPlayerWithID(playerID);
            if (slasher is null) { return; }
            ObjectSlash objSlash = slasher.data.weaponHandler.gun.transform.GetChild(1).Find("Lightsaber(Clone)").GetComponent<ObjectSlash>();
            objSlash.DoSlashAnim();
        }
        internal void DoSlashAnim()
        {
            this.StartCoroutine(this.IDoSlashAnim());
        }
        IEnumerator IDoSlashAnim()
        {
            this.slashing = true;
            this.hitPlayers = new List<Player>();
            if (this.holdable is null) { this.holdable = base.transform.root.GetComponent<Holdable>(); }
            float time = 0f;
            float c = Mathf.Clamp(time / slashAnimDuration, 0, 1);
            float prev;
            bool left = this.transform.root.position.x - 0.1f < this.holdable.holder.transform.position.x;
            this.mirror.rotMod = slashCurves[2].Evaluate(c);

            while (time < slashAnimDuration)
            {
                yield return null;
                time += TimeHandler.deltaTime;
                prev = c;
                c = Mathf.Clamp(time / slashAnimDuration, 0, 1);
                left = this.transform.root.position.x - 0.1f < this.holdable.holder.transform.position.x;
                this.transform.root.position = player.data.hand.position;
                var prevHeading = new Vector3(slashCurves[1].Evaluate(prev), slashCurves[0].Evaluate(prev) * (left ? -1f : 1f), 0);
                var heading = new Vector3(slashCurves[1].Evaluate(c), slashCurves[0].Evaluate(c) * (left ? -1f : 1f), 0);
                //var trueHeading = heading - prevHeading;
                this.transform.root.position += ((Quaternion.Euler(0, 0, Vector2.Angle((Vector2)this.player.data.aimDirection.normalized, (Vector2)heading.normalized)) * heading));
                this.mirror.rotMod = slashCurves[2].Evaluate(c);
                //UnityEngine.Debug.Log($"Time: {c}\nPosition: {slashCurves[0].Evaluate(c)}, {slashCurves[1].Evaluate(c)}\nRotation: {slashCurves[2].Evaluate(c)}");
            }
            this.mirror.rotMod = 0;
            this.slashing = false;
            yield break;
        }
        void Update()
        {
            this.sinceSlash += TimeHandler.deltaTime;
            if (this.player.data.view.IsMine && this.holdableObj.switchTimer <= 0f
                && this.sinceSlash >= slashCooldown && this.input.shootWasPressed
                && this.holdableObj.IsOut)
                //if (this.IsBlade || sinceBlade >= slashCooldown)
                this.DoSlash();

            if (!this.slashing)
                this.transform.root.position = player.data.hand.position;
        }
        List<Collider2D> hitColliders = new List<Collider2D>();
        List<Player> hitPlayers = new List<Player>();
        internal void TrySlash(Collider2D collider2D)
        {
            if (!this.player.data.view.IsMine) { return; }

            if (collider2D?.GetComponent<Player>() != null
                && !collider2D.GetComponent<Player>().data.dead
                && collider2D.GetComponent<Player>().playerID != this.player.playerID
                && this.canDoDamage)
            {
                this.canDoDamage = false;
                NetworkingManager.RPC(typeof(ObjectSlash), nameof(RPCA_SlashPlayer), this.player.playerID, collider2D.GetComponent<Player>().playerID);
            }
        }

        internal void TrySlash(Collision2D collision)
        {
            Collider2D collider2D = collision.collider;
            if (!this.player.data.view.IsMine || collider2D is null || this.hitColliders.Contains(collider2D) || collision.contactCount <= 0) { return; }

            hitColliders.Add(collider2D);
            ChaosPoppycarsCards.Instance.ExecuteAfterSeconds(slashAnimDuration, () => { hitColliders.Remove(collider2D); });

            if (collider2D.GetComponentInParent<Player>())
            {
                if (collider2D.GetComponentInParent<Player>().playerID == this.player.playerID)
                    return;
                Player target = collider2D.GetComponentInParent<Player>();
                if (hitPlayers.Contains(target))
                    return;
                hitPlayers.Add(target);

                HandlePlayer(target, collision);
            }
            else if (collider2D.GetComponentInParent<ProjectileHit>())
                HandleBullet(collider2D.GetComponentInParent<ProjectileHit>(), collision);
            else if (collider2D.attachedRigidbody != null)
                HandleBox(collider2D.attachedRigidbody, collision);
            else if (collider2D.transform.root.GetComponentInChildren<Damagable>())
                if (this.slashing)
                    collider2D.transform.root.GetComponentInChildren<Damagable>().CallTakeDamage(Vector2.up * 55f, this.gameObject.transform.position, damagingPlayer: player);
        }
        internal void HandlePlayer(Player target, Collision2D collision)
        {
            if (slashing)
            {
                NetworkingManager.RPC(typeof(ObjectSlash), nameof(RPCA_SlashPlayer), this.player.playerID, target.playerID);

                var knockbackForce = Mathf.Pow(player.data.weaponHandler.gun.damage, 2f) * player.data.weaponHandler.gun.knockback * Mathf.Clamp((float)target.data.playerVel.GetFieldValue("mass") / 100f, 0f, 1f);
                target.data.healthHandler.CallTakeForce(knockbackForce * collision.contacts[0].point);
            }
        }
        private float slashForceMult = 1500f;
        private float nonslashForceMult = 500f;
        internal void HandleBox(Rigidbody2D box, Collision2D collision)
        {
            if (box.GetComponent<Damagable>() && this.slashing)
                box.GetComponent<Damagable>().CallTakeDamage(55f * player.data.weaponHandler.gun.damage * player.data.weaponHandler.gun.bulletDamageMultiplier * (collision.contacts[0].point - (Vector2)this.transform.position).normalized, collision.contacts[0].point);
            ContactPoint2D[] contacts = new ContactPoint2D[collision.contactCount];
            int count = collision.GetContacts(contacts);

            for (int i = 0; i < count; i++)
                box.AddForceAtPosition(this.player.data.weaponHandler.gun.knockback * box.mass * -1 * contacts[i].normal * (slashing ? slashForceMult : nonslashForceMult) / (float)count, contacts[i].point);
                //UnityEngine.Debug.Log($"Adding {this.player.data.weaponHandler.gun.knockback * box.mass * -1 * contacts[i].normal * (slashing ? slashForceMult : nonslashForceMult) / (float)count} force to {box.gameObject}");
        }
        internal void HandleBullet(ProjectileHit bullet, Collision2D collision)
        {
            //var move = bullet.GetComponentInChildren<MoveTransform>();
            //move.velocity = Vector2.Reflect(move.velocity, collision.contacts[0].normal);
            player.data.block.blocked(bullet.gameObject, bullet.gameObject.transform.forward, collision.transform.position);
        }
        [UnboundRPC]
        private static void RPCA_SlashPlayer(int slashingPlayerID, int slashedPlayerID)
        {
            if (slashedPlayerID == -1) { return; }
            Player slashingPlayer = PlayerManager.instance.players.FirstOrDefault(p => p.playerID == slashingPlayerID);
            Player slashedPlayer = PlayerManager.instance.players.FirstOrDefault(p => p.playerID == slashedPlayerID);
            if (slashedPlayer is null || slashingPlayer is null) { return; }
            // 51 damage instead of 50 because players dont die until they are less than 0 hp
            slashedPlayer.data.healthHandler.DoDamage((slashingPlayer.data.weaponHandler.gun.damage * slashingPlayer.data.weaponHandler.gun.bulletDamageMultiplier * 55f) * slashingPlayer.data.weaponHandler.gun.shootPosition.forward, slashedPlayer.transform.position, Color.white, slashingPlayer.data?.weaponHandler?.gun?.transform?.GetChild(1)?.Find("Lightsaber(Clone)")?.gameObject, slashingPlayer, false, true, true);
        }
    }
}


