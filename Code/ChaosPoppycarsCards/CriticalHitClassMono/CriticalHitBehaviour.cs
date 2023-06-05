using Photon.Pun;
using UnityEngine;
using CPC.Extensions;
using ChaosPoppycarsCards.Utilities;
using SimulationChamber;

public class CriticalHitBehaviour : MonoBehaviour/*Pun*/
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
        //photonView.RPC("SyncRandomValue", RpcTarget.All, gun.GetAdditionalData().criticalHitChance);

        ProjectileHit bullet = obj.GetComponent<ProjectileHit>();
        
        UnityEngine.Debug.Log("You shot lmao");


        isCriticalHit = Random.value < this.gun.GetAdditionalData().criticalHitChance1;

        critMultiplier = 1f;


        if (isCriticalHit)
        {
            UnityEngine.Debug.Log("was a crit");
            critMultiplier = Mathf.Floor(this.gun.GetAdditionalData().criticalHitChance1 / 1f); // Get the integer part of the critical hit chance

            // Check for double crit and higher crit multipliers
            if (this.gun.GetAdditionalData().criticalHitChance1 >= 1f)
            {
                float additionalCrits = Mathf.Floor((this.gun.GetAdditionalData().criticalHitChance1 - 1f) / 1f);
                float doubleCritChance = (this.gun.GetAdditionalData().criticalHitChance1 - 1f) % 1f;
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
        }
    }
    public void OnDestroy()
    {
        // Remove our action when the mono is removed
        gun.ShootPojectileAction -= OnShootProjectileAction;

    }
    //[PunRPC]
    /* private void SyncRandomValue(Gun gun)
     {
         isCriticalHit = Random.value < gun.GetAdditionalData().criticalHitChance;
         critMultiplier = 1f;

         if (isCriticalHit)
         {
             critMultiplier = Mathf.Floor(gun.GetAdditionalData().criticalHitChance / 1f); // Get the integer part of the critical hit chance

             // Check for double crit and higher crit multipliers
             if (gun.GetAdditionalData().criticalHitChance >= 1f)
             {
                 float additionalCrits = Mathf.Floor((gun.GetAdditionalData().criticalHitChance - 1f) / 1f);
                 float doubleCritChance = (gun.GetAdditionalData().criticalHitChance - 1f) % 1f;
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
     } */
}
