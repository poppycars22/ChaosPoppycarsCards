using Photon.Pun;
using SimulationChamber;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using System.Linq;
using ModdingUtils.MonoBehaviours;
using UnboundLib;
namespace ChaosPoppycarsCards.MonoBehaviours
{
    public class BlockBullets : MonoBehaviour
    {
        Block block;
        Player player;
        Gun gun;
        public SimulatedGun[] savedGuns = new SimulatedGun[1];
        public void OnDestroy()
        {
            block.BlockAction -= OnBlock;
        }
        private void OnBlock(BlockTrigger.BlockTriggerType trigger)
        {
            SimulatedGun BlockGun = savedGuns[0];
            BlockGun.CopyGunStatsExceptActions(this.gun);
            BlockGun.CopyAttackAction(this.gun);
            BlockGun.CopyShootProjectileAction(this.gun);
            BlockGun.bursts = 3;
            BlockGun.timeBetweenBullets = 0.15f;
            BlockGun.damage = 65f/55;
            Vector3 temp = player.transform.position;
            Vector3 aim = new Vector3(player.data.input.aimDirection.x, player.data.input.aimDirection.y, 0);
            if (!(player.data.view.IsMine || PhotonNetwork.OfflineMode))
            {
                return;
            }
            ChaosPoppycarsCards.Instance.ExecuteAfterSeconds(1.5f, () => { 
              BlockGun.SimulatedAttack(this.player.playerID, temp, aim, 1f, 1);
            });
        }
        public void Start()
        {
            this.player = gameObject.GetComponent<Player>();
            this.gun = this.player.data.weaponHandler.gun;
            this.block = this.player.GetComponent<Block>();
            if (savedGuns[0] == null)
            {
                savedGuns[0] = new GameObject("BlockBullets").AddComponent<SimulatedGun>();
            }
            block.BlockAction += OnBlock;
        }
        public void Update()
        {

        }
    }
}
