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
    [HarmonyPatch(typeof(HealthHandler), "DoDamage")]
    class HealtHandlerPatchDoDamage
    {
        // patch for Totem and Damage Reduction
        private static void Prefix(HealthHandler __instance, ref Vector2 damage, Vector2 position, Color blinkColor, GameObject damagingWeapon, Player damagingPlayer, bool healthRemoval, ref bool lethal, bool ignoreBlock)
        {
            CharacterData data = (CharacterData)Traverse.Create(__instance).Field("data").GetValue();
            Player player = data.player;
            if (!data.isPlaying)
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
            }
            if (damagingPlayer!= null && damagingPlayer.data.stats.GetAdditionalData().firstDamage == true)
            {
                damagingPlayer.data.stats.GetAdditionalData().damageMult = damagingPlayer.data.stats.GetAdditionalData().damageMultMax;
                damagingPlayer.data.stats.GetAdditionalData().firstDamage = false;
            }  
            if (damagingPlayer!=null && damagingPlayer.data.stats.GetAdditionalData().reducingDmg)
            {
                damage *= damagingPlayer.data.stats.GetAdditionalData().damageMult;
                if (damagingPlayer.data.stats.GetAdditionalData().damageMult > 0.05f)
                    damagingPlayer.data.stats.GetAdditionalData().damageMult -= damagingPlayer.data.stats.GetAdditionalData().reducingDmgAmt;
                if (damagingPlayer.data.stats.GetAdditionalData().damageMult < 0.05f)
                    damagingPlayer.data.stats.GetAdditionalData().damageMult = 0.05f;
            }

            if (player.data.stats.GetAdditionalData().firstHit == true)
            {
                player.data.stats.GetAdditionalData().firstHit = false;
                if(player.data.stats.GetAdditionalData().firstHitdmgReduction >0)
                    damage /= player.data.stats.GetAdditionalData().firstHitdmgReduction;
                /*if (player.data.stats.GetAdditionalData().firstHitdmgReduction > 1f && player.data.stats.GetAdditionalData().firstHitdmgReduction < 2f)
                    damage -= damage * (player.data.stats.GetAdditionalData().firstHitdmgReduction - 1f);
                else if (player.data.stats.GetAdditionalData().firstHitdmgReduction >= 2f)
                    damage -= damage;
                if (player.data.stats.GetAdditionalData().firstHitdmgReduction < 1f && player.data.stats.GetAdditionalData().firstHitdmgReduction >= 0)
                    damage += damage * (1 - player.data.stats.GetAdditionalData().firstHitdmgReduction);*/
            }
            // if the damage is lethal and would've killed the player, check for mulligans
            if (lethal && data.health < damage.magnitude && data.stats.GetAdditionalData().remainingTotems > 0)
            {
                // if lethal, whould've killed, and there are mulligans available, use a mulligan
                if (player.GetComponent<TotemEffect>() != null)
                { player.GetComponent<TotemEffect>().UseMulligan(); }
                else { return; }

                lethal = false;
            }
           
        }
    }

}