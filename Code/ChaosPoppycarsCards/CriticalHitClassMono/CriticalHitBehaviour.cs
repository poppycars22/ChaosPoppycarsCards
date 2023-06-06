using Photon.Pun;
using UnityEngine;
using CPC.Extensions;
using ChaosPoppycarsCards.Utilities;
using SimulationChamber;
using UnboundLib.Networking;

public class CriticalHitBehaviour : MonoBehaviourPun
{
    private float critMultiplier = 1f; // Current critical hit multiplier
    private bool isCriticalHit; // Flag indicating if it's a critical hit
    Gun gun;
    Player player;
    public void Start()
    {
        UnityEngine.Debug.Log("mono was added/start");
        // Get Player
        this.player = this.GetComponentInParent<Player>();
        // Get Gun
        this.gun = this.player.data.weaponHandler.gun;
        // Hook up our action.
        this.gun.ShootPojectileAction += this.OnShootProjectileAction;

        // Checks to see if we have a saved gun already, if not, we make one.
        
    }
    public void OnShootProjectileAction(GameObject obj)
    {
        photonView.RPC("SyncRandomValue", RpcTarget.All, gun.GetAdditionalData().criticalHitChance1);
        ProjectileHit bullet = obj.GetComponent<ProjectileHit>();
        


        UnityEngine.Debug.Log("You shot lmao");

        

       //isCriticalHit = Random.value < gun.GetAdditionalData().criticalHitChance1;
        critMultiplier = 1f;

        if (isCriticalHit)
        {
            UnityEngine.Debug.Log("was a crit");
            critMultiplier = Mathf.Floor(gun.GetAdditionalData().criticalHitChance1 / 1f); // Get the integer part of the critical hit chance

            // Check for double crit and higher crit multipliers
            if (gun.GetAdditionalData().criticalHitChance1 >= 1f)
            {
                float additionalCrits = Mathf.Floor((gun.GetAdditionalData().criticalHitChance1 - 1f) / 1f);
                float doubleCritChance = (gun.GetAdditionalData().criticalHitChance1 - 1f) % 1f;
                if (Random.value < doubleCritChance)
                {
                    critMultiplier += additionalCrits + 1f;
                }
                else
                {
                    critMultiplier += additionalCrits;
                }
            }
            else
            {
                critMultiplier = 1f;
            }
        }
        if (isCriticalHit)
        {
           
           
            if (critMultiplier > 1f)
            {
                bullet.damage *= (this.gun.GetAdditionalData().criticalHitDamage1 * critMultiplier)/((critMultiplier/2f)+0.75f);
            }
            else
            {
                bullet.damage *= this.gun.GetAdditionalData().criticalHitDamage1;
            }
            UnityEngine.Debug.Log("Bullet dmg " + bullet.damage + " Crit mult" + critMultiplier + " Crit hit dmg 1" + gun.GetAdditionalData().criticalHitDamage1);
        }
        else
        {
           
        }
    }
    public void OnDestroy()
    {
        // Remove our action when the mono is removed
        gun.ShootPojectileAction -= OnShootProjectileAction;

    }
      [PunRPC]
      private void SyncRandomValue(/*Gun gun, GameObject obj*/)
       {
          //ProjectileHit bullet = obj.GetComponent<ProjectileHit>();
          isCriticalHit = Random.value < gun.GetAdditionalData().criticalHitChance1;
         /*  critMultiplier = 1f;

          if (isCriticalHit)
          {
              UnityEngine.Debug.Log("was a crit");
              critMultiplier = Mathf.Floor(gun.GetAdditionalData().criticalHitChance1 / 1f); // Get the integer part of the critical hit chance

              // Check for double crit and higher crit multipliers
              if (gun.GetAdditionalData().criticalHitChance1 >= 1f)
              {
                  float additionalCrits = Mathf.Floor((gun.GetAdditionalData().criticalHitChance1 - 1f) / 1f);
                  float doubleCritChance = (gun.GetAdditionalData().criticalHitChance1 - 1f) % 1f;
                  if (Random.value < doubleCritChance)
                  {
                      critMultiplier += additionalCrits + 1f;
                  }
                  else
                  {
                      critMultiplier += additionalCrits;
                  }
              }
          }
          if (isCriticalHit)
          {
              bullet.projectileColor = Color.yellow;
              bullet.damage *= this.gun.GetAdditionalData().criticalHitDamage1 * critMultiplier;
              UnityEngine.Debug.Log("Bullet dmg" + bullet.damage);
          } */
      } 
}
