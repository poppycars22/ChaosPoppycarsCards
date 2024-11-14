using System;
using HarmonyLib;
using ChaosPoppycarsCards.MonoBehaviours;
using UnityEngine;
using ChaosPoppycarsCards.Extensions;
using UnboundLib;
using SimulationChamber;
using Photon.Pun;

namespace ChaosPoppycarsCards.Patches
{
    [Serializable]
    [HarmonyPatch(typeof(ProjectileHit), "Hit")]
    class ProjectileHitPatchHit
    {
        // patch for block mover
        private static void Postfix(ProjectileHit __instance, ref HitInfo hit, ref Player ___ownPlayer, ref float ___damage, bool forceCall = false)
        {
            if(hit.collider.GetComponent<HealthHandler>() !=null)
            {
                if (___ownPlayer != null && ___ownPlayer.data.stats.GetAdditionalData().firstDamage == true)
                {
                    ___ownPlayer.data.stats.GetAdditionalData().damageMult = ___ownPlayer.data.stats.GetAdditionalData().damageMultMax;
                    ___ownPlayer.data.stats.GetAdditionalData().firstDamage = false;
                }
                if (___ownPlayer != null && ___ownPlayer.data.stats.GetAdditionalData().reducingDmg)
                {
                    ___damage *= ___ownPlayer.data.stats.GetAdditionalData().damageMult;
                    if (___ownPlayer.data.stats.GetAdditionalData().damageMult > 0.05f)
                        ___ownPlayer.data.stats.GetAdditionalData().damageMult -= ___ownPlayer.data.stats.GetAdditionalData().reducingDmgAmt;
                    if (___ownPlayer.data.stats.GetAdditionalData().damageMult < 0.05f)
                        ___ownPlayer.data.stats.GetAdditionalData().damageMult = 0.05f;
                }
            }
        }
    }
    [Serializable]
    [HarmonyPatch(typeof(ProjectileHit), "RPCA_DoHit")]
    class ProjectileHitPatchRPCA_DoHit
    {
        private static void Postfix(ProjectileHit __instance, Vector2 hitPoint, Vector2 hitNormal, Vector2 vel, ref Player ___ownPlayer, ref float ___damage, int viewID = -1, int colliderID = -1, bool wasBlocked = false)
        {
            HitInfo hitInfo = new HitInfo();

            hitInfo.point = hitPoint;
            hitInfo.normal = hitNormal;
            hitInfo.collider = null;
            if (viewID != -1)
            {
                PhotonView photonView = PhotonNetwork.GetPhotonView(viewID);
                hitInfo.collider = photonView.GetComponentInChildren<Collider2D>();
                hitInfo.transform = photonView.transform;
            }
            else if (colliderID != -1)
            {
                hitInfo.collider = MapManager.instance.currentMap.Map.GetComponentsInChildren<Collider2D>()[colliderID];
                hitInfo.transform = hitInfo.collider.transform;
            }
            if (hitInfo.collider.GetComponent<HealthHandler>() == null && ___ownPlayer != null && hitInfo.transform != null && ___ownPlayer.GetComponent<CooldownBlock>() != null && ___ownPlayer.GetComponent<CooldownBlock>().duration <= 0)
            {
                if (___ownPlayer.data.stats.GetAdditionalData().blockMover && !___ownPlayer.data.stats.GetAdditionalData().blockPush)
                {
                    var move = hitInfo.transform.gameObject.GetOrAddComponent<Smooth>();
                    move.currentPos = hitInfo.transform.position;
                    move.targetPos = hitInfo.transform.position+((___ownPlayer.transform.position - hitInfo.transform.position).normalized * ___ownPlayer.data.stats.GetAdditionalData().blockMoveStrength * 3);
                    move.speed = 2;
                    //hitInfo.transform.position += ((___ownPlayer.transform.position - hitInfo.transform.position).normalized * ___ownPlayer.data.stats.GetAdditionalData().blockMoveStrength*3);
                    ___ownPlayer.GetComponent<CooldownBlock>().duration = 3;
                }
                else if (___ownPlayer.data.stats.GetAdditionalData().blockMover)
                {
                    var move = hitInfo.transform.gameObject.GetOrAddComponent<Smooth>();
                    move.currentPos = hitInfo.transform.position;
                    move.targetPos = hitInfo.transform.position + ((hitInfo.transform.position - ___ownPlayer.transform.position).normalized * ___ownPlayer.data.stats.GetAdditionalData().blockMoveStrength * 3);
                    move.speed = 2;
                    //hitInfo.transform.position += ((hitInfo.transform.position - ___ownPlayer.transform.position).normalized * ___ownPlayer.data.stats.GetAdditionalData().blockMoveStrength*3);
                    ___ownPlayer.GetComponent<CooldownBlock>().duration = 3;
                }
            }
        }
    }
}