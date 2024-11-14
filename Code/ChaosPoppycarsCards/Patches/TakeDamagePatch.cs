using System;
using HarmonyLib;
using ChaosPoppycarsCards.MonoBehaviours;
using UnityEngine;
using ChaosPoppycarsCards.Extensions;
using UnboundLib;
using SimulationChamber;
using Photon.Pun;
using System.Collections;
using System.Linq;
using Photon.Realtime;

namespace ChaosPoppycarsCards.Patches
{
    [Serializable]
    [HarmonyPatch(typeof(HealthHandler), "RPCA_SendTakeDamage")]
    class HealtHandlerPatchRPCASendTakeDamage
    {
        internal static Player GetPlayerWithID(int playerID)
        {
            for (int i = 0; i < PlayerManager.instance.players.Count; i++)
            {
                if (PlayerManager.instance.players[i].playerID == playerID)
                {
                    return PlayerManager.instance.players[i];
                }
            }

            return null;
        }
        private static void SimGun(Player player, Vector2 damage)
        {
            SimulatedGun[] savedGuns = new SimulatedGun[1];
            if (savedGuns[0] == null)
            {
                savedGuns[0] = new GameObject("DamageGun").AddComponent<SimulatedGun>();
            }
            SimulatedGun DmgBullet = savedGuns[0];
            Gun gun = player.data.weaponHandler.gun;
            DmgBullet.CopyGunStatsExceptActions(gun);
            DmgBullet.CopyAttackAction(gun);
            DmgBullet.CopyShootProjectileAction(gun);
            DmgBullet.objectsToSpawn = DmgBullet.objectsToSpawn.Concat(SimulatedGunTest.StopRecursionSpawn).ToArray();
            DmgBullet.bursts = 0;
            DmgBullet.numberOfProjectiles = 1;
            DmgBullet.damage = Mathf.Clamp(damage.magnitude / 110f, 0, player.data.health/55f);
            DmgBullet.reflects = 0;
            DmgBullet.chargeNumberOfProjectilesTo = 0;
            if (!(player.data.view.IsMine || PhotonNetwork.OfflineMode))
            {
                return;
            }
            Vector3 aim = new Vector3(player.data.input.aimDirection.x, player.data.input.aimDirection.y, 0);
            UnityEngine.Debug.Log("X " + aim.x + " Y " + aim.y + " Damage " + damage.magnitude);
            DmgBullet.SimulatedAttack(player.playerID, new Vector2(player.transform.position.x + 1.5f, player.transform.position.y), new Vector3(1, 0, 0), 1f, 1);
            DmgBullet.SimulatedAttack(player.playerID, new Vector2(player.transform.position.x - 1.5f, player.transform.position.y), new Vector3(-1, 0, 0), 1f, 1);
            DmgBullet.SimulatedAttack(player.playerID, new Vector2(player.transform.position.x + 1.5f, player.transform.position.y + 0.5f), new Vector3(0.5f, 1, 0), 1f, 1);
            DmgBullet.SimulatedAttack(player.playerID, new Vector2(player.transform.position.x - 1.5f, player.transform.position.y + 0.5f), new Vector3(-0.5f, 1, 0), 1f, 1);
        }
        private static void Prefix(HealthHandler __instance, ref Vector2 damage, Vector2 position, bool lethal, int playerID)
        {
            CharacterData data = (CharacterData)Traverse.Create(__instance).Field("data").GetValue();
            Player player = data.player;
            Player damagingPlayer = GetPlayerWithID(playerID);
            /*if (!data.isPlaying)
            {
                return;
            }
            if (data.dead)
            {
                return;
            }
            if (__instance.isRespawning)
            {
                return;
            }*/

            if (player.data.stats.GetAdditionalData().damagingBullet)
                SimGun(player, damage);
        }

    }
}