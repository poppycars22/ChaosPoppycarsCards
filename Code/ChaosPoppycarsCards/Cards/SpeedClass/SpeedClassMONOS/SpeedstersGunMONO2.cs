using System;
using ChaosPoppycarsCards.MonoBehaviours;
using ModdingUtils.MonoBehaviours;
using UnboundLib;
using UnityEngine;

namespace ChaosPoppycarsCards.MonoBehaviours
{
    public class SpeedstersGunMono : MonoBehaviour
    {

        private CharacterData data;
        private Player player;
        private Block block;
        private WeaponHandler weaponHandler;
        private Gun gun;
        private void Start()
        {
            this.data = this.gameObject.GetComponentInParent<CharacterData>();
        }
        private void Update()
        {
            

            if (!player)
            {
                if (!(data is null))
                {
                    player = data.player;
                    block = data.block;
                    weaponHandler = data.weaponHandler;
                    gun = weaponHandler.gun;

                    gun.ShootPojectileAction += OnShootProjectileAction;
                }

            }
        }
        private void OnShootProjectileAction(GameObject obj)
        {
            MoveTransform move = obj.GetComponentInChildren<MoveTransform>();
            Vector2 velocity = (Vector2)this.data.playerVel.GetFieldValue("velocity");
            ChaosPoppycarsCards.Instance.ExecuteAfterFrames(1, () =>
            {
                move.velocity += (Vector3)(Vector2)this.data.playerVel.GetFieldValue("velocity");
            });
        }
        private void OnDestroy()
        {
            gun.ShootPojectileAction -= OnShootProjectileAction;
        }
    }
}