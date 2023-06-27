﻿using Photon.Pun;
using UnityEngine;
using CPC.Extensions;
using ChaosPoppycarsCards.Utilities;
using SimulationChamber;
using System.Collections;
using Photon.Realtime;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System;
using UnboundLib.Networking;
using UnboundLib;
using static UnboundLib.NetworkingManager;
using ModdingUtils.MonoBehaviours;

namespace ChaosPoppycarsCards.MonoBehaviours
{
    public class CriticalHitBehaviour : MonoBehaviour//Pun
    {
        private float critMultiplier = 1f; // Current critical hit multiplier
        private static bool isCriticalHit; // Flag indicating if it's a critical hit
        private static bool isDoubleCrit;
        private int shoots;
        private float consecutiveCritDamage = 0f;
        Gun gun;
        Player player;
        SpawnedAttack spawnedAttack;
        private const string MessageEvent = "YourMod_MessageEvent";

        public void Start()
        {

            // Get Player
            this.player = this.GetComponentInParent<Player>();
            // Get Gun
            this.gun = this.player.data.weaponHandler.gun;
            // Hook up our action.
            this.gun.ShootPojectileAction += this.OnShootProjectileAction;

            isCriticalHit = false;
            isDoubleCrit = false;
            // Checks to see if we have a saved gun already, if not, we make one.

        }

        [UnboundRPC]
        public static void SyncCriticalHit(bool ISCRIT)
        {
            isCriticalHit = ISCRIT;

           // UnityEngine.Debug.Log("Sync Critical Hit was called");
           // UnityEngine.Debug.Log(isCriticalHit);
        }
        [UnboundRPC]
        public static void SyncDoubleCrit(bool ISDOUBLECRIT)
        {
           isDoubleCrit = ISDOUBLECRIT;
        }
     

        public void OnShootProjectileAction(GameObject obj)
        {
            this.spawnedAttack = obj.GetComponent<SpawnedAttack>();
            if (!this.spawnedAttack)
            {
                this.spawnedAttack = obj.AddComponent<SpawnedAttack>();
            }
            ProjectileHit bullet = obj.GetComponent<ProjectileHit>();
            MoveTransform move = obj.GetComponent<MoveTransform>();

            if (PhotonNetwork.IsMasterClient)
            {
                isCriticalHit = UnityEngine.Random.value < gun.GetAdditionalData().criticalHitChance1;
                
                if (gun.GetAdditionalData().guranteedCrits == true)
                {
                    isCriticalHit = true;
                    critMultiplier = 0f;
                    //consecutiveCritDamage = 0f;
                }






                NetworkingManager.RPC(typeof(CriticalHitBehaviour), nameof(SyncCriticalHit), new object[] { isCriticalHit });

                if (gun.GetAdditionalData().guranteedCrits == false && gun.GetAdditionalData().criticalHitChance1 > 1f)
                {
                    float doubleCritChance = (gun.GetAdditionalData().criticalHitChance1 - 1f) % 1f;
                    isDoubleCrit = UnityEngine.Random.value < doubleCritChance;
                    NetworkingManager.RPC(typeof(CriticalHitBehaviour), nameof(SyncDoubleCrit), new object[] { isDoubleCrit });
                }
               // UnityEngine.Debug.Log("Master Client");
               // UnityEngine.Debug.Log(isCriticalHit);

            }
            //UnityEngine.Debug.Log("You shot lmao");
            if (shoots > 0)
            {
                shoots -= 1;
            }


            //isCriticalHit = Random.value < gun.GetAdditionalData().criticalHitChance1;
            critMultiplier = 0f;
            if (isCriticalHit)
            {
                RayCastTrail trail = obj.GetComponent<RayCastTrail>();
                if (gun.GetAdditionalData().CritColor == Color.clear)
                {
                    trail.mask = trail.ignoreWallsMask;
                }
               
                bullet.unblockable = gun.GetAdditionalData().unblockableCrits;

                if (critMultiplier > 1.1f)
                {
                    if (gun.GetAdditionalData().criticalBulletSpeed > 1f)
                    {
                        move.localForce *= gun.GetAdditionalData().criticalBulletSpeed * critMultiplier;
                    }
                }
                else
                {

                    move.localForce *= gun.GetAdditionalData().criticalBulletSpeed;

                }

                if (gun.GetAdditionalData().criticalHitBounces >= 1)
                {
                    
                    if (gun.reflects <= 0)
                    {
                        RayHitReflect rayHitReflect = obj.gameObject.AddComponent<RayHitReflect>();

                        if (critMultiplier > 1.1f)
                        {
                            rayHitReflect.reflects += (gun.GetAdditionalData().criticalHitBounces * (int)Math.Round(critMultiplier)) - 1;
                            rayHitReflect.dmgM += gun.GetAdditionalData().criticalHitDamageOnBounce;
                        }
                        else
                        {
                            rayHitReflect.reflects += (gun.GetAdditionalData().criticalHitBounces - 1);
                            rayHitReflect.dmgM += gun.GetAdditionalData().criticalHitDamageOnBounce;
                        }

                    }


                    if (gun.reflects >= 1)
                    {
                        RayHitReflect rayHitReflect2 = obj.gameObject.GetComponent<RayHitReflect>();

                        if (critMultiplier > 1.1f)
                        {
                            rayHitReflect2.reflects += (gun.GetAdditionalData().criticalHitBounces * (int)Math.Round(critMultiplier));
                            rayHitReflect2.dmgM += gun.GetAdditionalData().criticalHitDamageOnBounce;
                        }
                        else
                        {
                            rayHitReflect2.reflects += (gun.GetAdditionalData().criticalHitBounces);
                            rayHitReflect2.dmgM += gun.GetAdditionalData().criticalHitDamageOnBounce;
                        }
                    }

                }
            }
            ChaosPoppycarsCards.Instance.ExecuteAfterFrames(3, () =>
            {
                if (isCriticalHit)
                {
                   // UnityEngine.Debug.Log("Crit");
                    spawnedAttack.SetColor(gun.GetAdditionalData().CritColor);
                    //UnityEngine.Debug.Log("was a crit");
                    //critMultiplier = Mathf.Floor(gun.GetAdditionalData().criticalHitChance1 / 1f); // Get the integer part of the critical hit chance

                    // Check for double crit and higher crit multipliers
                    if (gun.GetAdditionalData().criticalHitChance1 >= 1.1f & gun.GetAdditionalData().guranteedCrits == false)
                    {
                        int additionalCrits = (int)Math.Round(gun.GetAdditionalData().criticalHitChance1);
                        //UnityEngine.Debug.Log(additionalCrits + "addition crits");
                       
                        //UnityEngine.Debug.Log(doubleCritChance + "double crit chance");

                        if (isDoubleCrit)
                        {
                            critMultiplier += additionalCrits + 1f;
                            //UnityEngine.Debug.Log(critMultiplier + "crit mult");
                            spawnedAttack.SetColor(gun.GetAdditionalData().DoubleCritColor);
                        }
                        else
                        {
                            critMultiplier += additionalCrits;
                            if (gun.GetAdditionalData().consecutiveCrits == true)
                            {
                                consecutiveCritDamage = 0f;
                            }
                        }
                    }
                    else
                    {
                        critMultiplier = 0f;
                    }
                }
                if (isCriticalHit)
                {


                   


                    if (critMultiplier > 1.1f)
                    {
                        if (gun.GetAdditionalData().criticalHitDamage1 > 0f)
                        {
                            bullet.damage *= (gun.GetAdditionalData().criticalHitDamage1 * critMultiplier)/* / ((critMultiplier / 2f) + 0.75f)*/;
                        }
                        if (gun.GetAdditionalData().consecutiveCrits == true)
                        {
                            bullet.damage += consecutiveCritDamage;
                        }
                    }
                    else
                    {
                        if (gun.GetAdditionalData().criticalHitDamage1 > 0f)
                        {
                            bullet.damage *= (gun.GetAdditionalData().criticalHitDamage1);
                        }
                        if (gun.GetAdditionalData().consecutiveCrits == true)
                        {
                            bullet.damage += consecutiveCritDamage;
                        }
                    }

                    if (gun.GetAdditionalData().consecutiveCrits == true)
                    {
                        consecutiveCritDamage += gun.GetAdditionalData().consecutiveCritsDamage;
                    }

                    if (critMultiplier > 1.1f)
                    {
                        if (gun.GetAdditionalData().CritSlow > 0f)
                        {
                            bullet.movementSlow += gun.GetAdditionalData().CritSlow * critMultiplier;
                        }
                        if (gun.GetAdditionalData().criticalBulletSpeed > 1f)
                        {
                            move.localForce *= gun.GetAdditionalData().criticalBulletSpeed * critMultiplier;
                        }
                        if (gun.GetAdditionalData().criticalSimulationSpeed > 1f)
                        {
                            typeof(MoveTransform).GetField("simulationSpeed", BindingFlags.Default | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.SetField).SetValue(move, gun.GetAdditionalData().criticalSimulationSpeed * critMultiplier);
                        }
                    }
                    else
                    {
                        bullet.movementSlow += gun.GetAdditionalData().CritSlow;

                        move.localForce *= gun.GetAdditionalData().criticalBulletSpeed;

                        typeof(MoveTransform).GetField("simulationSpeed", BindingFlags.Default | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.SetField).SetValue(move, gun.GetAdditionalData().criticalSimulationSpeed);
                    }

                   

                   
                    if (gun.GetAdditionalData().criticalHeal > 0)
                    {
                        if (player.data.health <= player.data.maxHealth - 10)
                        {
                            player.data.health += gun.GetAdditionalData().criticalHeal;
                        }
                       
                    }
                    if (gun.GetAdditionalData().criticalBlockCDReduction > 0)
                    {
                        player.data.block.counter += gun.GetAdditionalData().criticalBlockCDReduction;
                    }
                    if (PhotonNetwork.IsMasterClient)
                    {
                        if (gun.GetAdditionalData().guranteedCrits == false && gun.GetAdditionalData().BlockingCrits == true && gun.GetAdditionalData().criticalHitChance1 <= 1f && shoots <= 0)
                        {
                            player.data.block.CallDoBlock(false, true, BlockTrigger.BlockTriggerType.Default, default, true);
                            shoots = 5 + (gun.numberOfProjectiles - 1) + (gun.bursts - 1);
                        }
                        else if (gun.GetAdditionalData().guranteedCrits == false && gun.GetAdditionalData().BlockingCrits == true && isDoubleCrit == true && shoots <= 0)
                        {
                            player.data.block.CallDoBlock(false, true, BlockTrigger.BlockTriggerType.Default, default, true);
                            shoots = 5 + (gun.numberOfProjectiles - 1) + (gun.bursts - 1);
                        }
                    }

                    // UnityEngine.Debug.Log("Bullet dmg " + bullet.damage + " Crit mult" + critMultiplier + " Crit hit dmg 1" + gun.GetAdditionalData().criticalHitDamage1);
                }

                else
                {

                    if (gun.GetAdditionalData().consecutiveCrits == true)
                    {
                        consecutiveCritDamage = 0f;
                    }
                }
            });
        }
       
        /*  public void SyncCriticalHit(bool isCriticalHitValue, bool isDoubleCritValue)
        {
            isCriticalHit = isCriticalHitValue;
            isDoubleCrit = isDoubleCritValue;
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(isCriticalHit);
                stream.SendNext(isDoubleCrit);
            }
            else
            {
                isCriticalHit = (bool)stream.ReceiveNext();
                isDoubleCrit = (bool)stream.ReceiveNext();
            }
        }

        public void OnPhotonCustomRoomPropertiesChanged(Hashtable propertiesThatChanged)
        {
            if (propertiesThatChanged.ContainsKey(IsCriticalHitKey))
            {
                isCriticalHit = (bool)propertiesThatChanged[IsCriticalHitKey];
            }
            if (propertiesThatChanged.ContainsKey(IsDoubleCritKey))
            {
                isDoubleCrit = (bool)propertiesThatChanged[IsDoubleCritKey];
            }
        }*/
        public void OnDestroy()
        {
            // Remove our action when the mono is removed
            isCriticalHit = false;
            consecutiveCritDamage = 0f;
            gun.ShootPojectileAction -= OnShootProjectileAction;
           // PhotonNetwork.RemoveCallbackTarget(this);
            
        }

    }
}





