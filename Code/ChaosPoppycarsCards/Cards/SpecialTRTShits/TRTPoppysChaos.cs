using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;
using Photon.Pun;
using BepInEx;
using ChaosPoppycarsCards.Cards;
using ChaosPoppycarsCards.Utilities;
using HarmonyLib;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using ChaosPoppycarsCards.MonoBehaviours;
using UnboundLib.Networking;
using System.Reflection;
using System.Collections.ObjectModel;
using ModdingUtils.MonoBehaviours;
using UnboundLib.Utils;
using WillsWackyManagers.Utils;
using RarityLib.Utils;
using Nullmanager;
using ChaosPoppycarsCards.MonoBehaviours.SodaMonos;
using ModdingUtils.Extensions;
using ChaosPoppycarsCards.Cards.Minecrafter;

namespace ChaosPoppycarsCards.Cards
{
    class TRTPoppysChaos : CustomCard
    {
        private System.Random randomNumberGenerator;
        private PhotonView photonView;
        private int previousRandomNumber;
        public void Start()
        { 
        photonView = GetComponent<PhotonView>();
            // If this is the master client, initialize the random number generator seed
            if (PhotonNetwork.IsMasterClient)
            {
                randomNumberGenerator = new System.Random((int)System.DateTime.Now.Ticks);
                photonView.RPC("InitializeRandomNumberGenerator", RpcTarget.Others, (int)System.DateTime.Now.Ticks);
            }
            
        }
            [PunRPC]
    public void InitializeRandomNumberGenerator(int seed)
    {
        randomNumberGenerator = new System.Random(seed);
            
    }
        private bool LegendCondition(CardInfo card, Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            return card.rarity == RarityUtils.GetRarity("Legendary") && card.cardName != "Peptide" && !card.categories.Intersect(ScareJackpot.noLotteryCategories).Any();

        }

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            cardInfo.categories = new CardCategory[] { CurseManager.instance.curseSpawnerCategory, CustomCardCategories.instance.CardCategory("cantEternity") };
            cardInfo.categories = new CardCategory[] { CustomCardCategories.instance.CardCategory("TRT_Enabled") };
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been setup.");
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            //Edits values on player when card is selected
            
            void GenerateAndPerformEffect()
            {
                int randomNumber = randomNumberGenerator.Next(1, 31);

                while (randomNumber == previousRandomNumber)
                {
                    randomNumber = randomNumberGenerator.Next(1, 31);   //add 1 to max value (want 4 results? max num = 5)
                }

                previousRandomNumber = randomNumber;

                switch (randomNumber)
                { 
                    case 0:

                        //UnityEngine.Debug.Log("case 0");
                        //example case
                        break;
                    case 1:
                        
                        //UnityEngine.Debug.Log("case 1");
                        gun.damage *= 1.5f;
                        break;
                    case 2:
                        //UnityEngine.Debug.Log("case 2");
                        gun.dmgMOnBounce += 0.1f;
                        gun.reflects += 1;
                        break;
                    // ...
                    case 3:
                        //UnityEngine.Debug.Log("case 3");
                        data.maxHealth *= 1.5f;
                        break;
                    case 4:
                        //UnityEngine.Debug.Log("case 4");
                        gun.objectsToSpawn = new List<ObjectsToSpawn>(gun.objectsToSpawn)
                        {
                            
                           new ObjectsToSpawn ()
                           {
                                AddToProjectile = ChaosPoppycarsCards.Bundle.LoadAsset<GameObject>("REVERSETHRUSTER"),
                                stickToAllTargets = true,
                                spawnOn = ObjectsToSpawn.SpawnOn.all,
                                direction = ObjectsToSpawn.Direction.forward,
                                spawnAsChild = false,
                                effect = ChaosPoppycarsCards.Bundle.LoadAsset<GameObject>("REVERSETHRUSTER"),
                                removeScriptsFromProjectileObject = true,
                                scaleStacks = true,
                                scaleStackM = 0.7f,
                                scaleFromDamage = 0.7f
                           },
                        }.ToArray();

                        break;
                    case 5:
                        gun.damage *= 0.5f;
                        break;
                    case 6:
                        data.maxHealth = 1;
                        statModifiers.sizeMultiplier = 2.4f;
                        break;
                    case 7:
                        block.additionalBlocks += 4;
                        break;
                    case 8:
                        gun.spread += 0.5f;
                        break;
                    case 9:
                        gun.speedMOnBounce *= 0.3f;
                        //UnityEngine.Debug.Log("case 0");
                        //example case
                        break;
                    case 10:
                        ChaosPoppycarsCards.Instance.ExecuteAfterFrames(20, () => {
                            var legend = ModdingUtils.Utils.Cards.instance.GetRandomCardWithCondition(player, gun, gunAmmo, data, health, gravity, block, characterStats, LegendCondition);
                            var legend2 = ModdingUtils.Utils.Cards.instance.GetRandomCardWithCondition(player, gun, gunAmmo, data, health, gravity, block, characterStats, LegendCondition);
                            ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, legend, false, "", 2f, 2f, true);
                            ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, legend, 3f);
                            ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, legend2, false, "", 2f, 2f, true);
                            ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, legend2, 3f);
                        });
                        //UnityEngine.Debug.Log("case 0");
                        //example case
                        break;
                    case 11:
                        characterStats.AjustNulls(30);
                        //UnityEngine.Debug.Log("case 0");
                        //example case
                        break;
                    case 12:
                        block.forceToAdd += 25;
                        //UnityEngine.Debug.Log("case 0");
                        //example case
                        break;
                    case 13:
                        gravity.gravityForce = 0.001f;
                        //UnityEngine.Debug.Log("case 0");
                        //example case
                        break;
                    case 14:
                        gun.bodyRecoil += 30;
                        gun.recoilMuiltiplier += 2f;
                        //UnityEngine.Debug.Log("case 0");
                        //example case
                        break;
                    case 15:
                        block.objectsToSpawn.Add(ChaosPoppycarsCards.Bundle.LoadAsset<GameObject>("A_Explosion 1"));

                        //UnityEngine.Debug.Log("case 0");
                        //example case
                        break;
                    case 16:
                        gun.objectsToSpawn = new List<ObjectsToSpawn>(gun.objectsToSpawn)
                        {

                           new ObjectsToSpawn ()
                           {
                                AddToProjectile = ChaosPoppycarsCards.Bundle.LoadAsset<GameObject>("A_BombEffect 3 (2)"),
                                stickToAllTargets = true,
                                spawnOn = ObjectsToSpawn.SpawnOn.all,
                                direction = ObjectsToSpawn.Direction.forward,
                                spawnAsChild = false,
                                numberOfSpawns = 1,
                                normalOffset = 0.05f,
                                effect = ChaosPoppycarsCards.Bundle.LoadAsset<GameObject>("A_Bomb_Timed_Detonation 3 (2)"),
                                removeScriptsFromProjectileObject = false,
                                scaleStacks = true,
                                scaleStackM = 0.7f,
                                scaleFromDamage = 0.7f
                           },
                        }.ToArray();
                        //UnityEngine.Debug.Log("case 0");
                        //example case
                        break;
                    case 17:
                        ChaosPoppycarsCards.Instance.ExecuteAfterFrames(20, () => {
                            CurseManager.instance.CursePlayer(player, (curse) => {
                                ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, curse, 3f);
                            });
                            CurseManager.instance.CursePlayer(player, (curse) => {
                                ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, curse, 3f);
                            });
                        });
                        //UnityEngine.Debug.Log("case 0");
                        //example case
                        break;
                    case 18:
                        gun.knockback *= 10;
                        //UnityEngine.Debug.Log("case 0");
                        //example case
                        break;
                    case 19:
                        gun.bodyRecoil += 30;
                        gun.recoilMuiltiplier += 2f;
                        gun.recoilMuiltiplier *= -1f;
                        //UnityEngine.Debug.Log("case 0");
                        //example case
                        break;
                    case 20:
                        ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, CraftingTable.Card, addToCardBar: true);
                        ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, CraftingTable.Card);
                        ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, WoodenAxe.Card, addToCardBar: true);
                        ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, WoodenAxe.Card);
                        ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, WoodenSword.Card, addToCardBar: true);
                        ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, WoodenSword.Card);
                        ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, WoodenHoe.Card, addToCardBar: true);
                        ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, WoodenHoe.Card);
                        ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, LetherArmor.Card, addToCardBar: true); 
                        ModdingUtils.Utils.CardBarUtils.instance.ShowAtEndOfPhase(player, LetherArmor.Card);
                        //UnityEngine.Debug.Log("case 0");
                        //example case
                        break;
                    case 21:
                        characterStats.respawns += 1;
                        //UnityEngine.Debug.Log("case 0");
                        //example case
                        break;
                    case 22:
                        characterStats.regen += 10;
                        //UnityEngine.Debug.Log("case 0");
                        //example case
                        break;
                    case 23:
                        var addedObj = Instantiate((ChaosPoppycarsCards.Bundle.LoadAsset<GameObject>("A_DemonicPact 1")), player.transform.position, player.transform.rotation, player.transform);
                        characterStats.objectsAddedToPlayer.Add(addedObj);
                        //UnityEngine.Debug.Log("case 0");
                        //example case
                        break;
                    case 24:
                        //var addedObj2 = Instantiate((ChaosPoppycarsCards.Bundle.LoadAsset<GameObject>("A_DemonicPact 2")), player.transform.position, player.transform.rotation, player.transform);
                        //characterStats.objectsAddedToPlayer.Add(addedObj2);
                        gun.objectsToSpawn = new List<ObjectsToSpawn>(gun.objectsToSpawn)
                        {

                           new ObjectsToSpawn ()
                           {
                                AddToProjectile = ChaosPoppycarsCards.Bundle.LoadAsset<GameObject>("HealthBounceObject 1")

                           },
                        }.ToArray();
                        gun.reflects += 3;
                        //UnityEngine.Debug.Log("case 0");
                        //example case
                        break;
                    case 25:
                        characterStats.movementSpeed *= 5;
                        //UnityEngine.Debug.Log("case 25");
                        //example case
                        break;
                    case 26:
                        gun.dontAllowAutoFire = true;
                        //UnityEngine.Debug.Log("case 0");
                        //example case
                        break;
                    case 27:
                        data.maxHealth /= 2;
                        //UnityEngine.Debug.Log("case 0");
                        //example case
                        break;
                    case 28:
                        characterStats.lifeSteal *= 1.5f;
                        //UnityEngine.Debug.Log("case 0");
                        //example case
                        break;
                    case 29:
                        gravity.gravityForce *= -1;
                        characterStats.numberOfJumps += 10;
                        //UnityEngine.Debug.Log("case 0");
                        //example case
                        break;
                    case 30:
                        gun.reflects += 100;
                        //UnityEngine.Debug.Log("case 0");
                        //example case
                        break;
                    default:
                        UnityEngine.Debug.LogError("Unexpected random number value: " + randomNumber);
                        break;
                }
            }

            GenerateAndPerformEffect();
        }

        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
           
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
            //Run when the card is removed from the player
        }


        protected override string GetTitle()
        {
            return "TRT Poppys Chaos";
        }
        protected override string GetDescription()
        {
            return "The C H A O S, its now true (gives a effect from a list of effects)";
        }
        protected override GameObject GetCardArt()
        {
            return ChaosPoppycarsCards.Bundle.LoadAsset<GameObject>("C_PoppysChaos");
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Uncommon;
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    stat = "",
                    amount = "<#FFFF00>+???</color>",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                new CardInfoStat()
                {

                    stat = "",
                    amount = "<#FFFF00>-???</color>",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.ColdBlue;
        }
        public override string GetModName()
        {
            return "CPC";
        }
    }
}
