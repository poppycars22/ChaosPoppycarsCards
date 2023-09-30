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
//using ModdingUtils.Extensions;
using ChaosPoppycarsCards.Cards.Minecrafter;
using ChaosPoppycarsCards.Extensions;
using System.Collections;
using UnboundLib.GameModes;
using System.Numerics;
using UnityEditor;

namespace ChaosPoppycarsCards.Cards
{
    class PoppysChaos : CustomCard
    {
        private System.Random randomNumberGenerator;
        private PhotonView photonView;
        private int previousRandomNumber;
        public bool HealthBouncesCASE = false;
        public bool BlockCase = false;
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
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been setup.");
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");
            //Edits values on player when card is selected
            
            void GenerateAndPerformEffect()
            {
                int randomNumber = randomNumberGenerator.Next(1, 45);

                /*while (randomNumber == previousRandomNumber)
                {
                    randomNumber = randomNumberGenerator.Next(41, 42);   //add 1 to max value (want 4 results? max num = 5)
                }*/

                previousRandomNumber = randomNumber;

                switch (randomNumber)
                { 
                    case 0:

                        //UnityEngine.Debug.Log("case 0");
                        //example case
                        break;
                    case 1:
                        
                        UnityEngine.Debug.Log("Poppys Chaos case 1");
                        gun.damage *= 1.5f;
                        break;
                    case 2:
                        UnityEngine.Debug.Log("Poppys Chaos case 2");
                        gun.dmgMOnBounce += 0.1f;
                        gun.reflects += 1;
                        break;
                    // ...
                    case 3:
                        UnityEngine.Debug.Log("Poppys Chaos case 3");
                        data.maxHealth *= 1.5f;
                        break;
                    case 4:
                        UnityEngine.Debug.Log("Poppys Chaos case 4");
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
                        //gun.reloadTime *= 0.25f;
                        gunAmmo.reloadTimeMultiplier *= 0.25f;
                        UnityEngine.Debug.Log("Poppys Chaos case 5");
                        break;
                    case 6:
                        data.maxHealth = 1;
                        //statModifiers.sizeMultiplier = 2.4f;
                        characterStats.sizeMultiplier *= 2.4f;
                        characterStats.respawns += 2;
                        characterStats.GetAdditionalData().useNewRespawnTime = true;
                        characterStats.GetAdditionalData().newRespawnTime = 0.5f; 
                        UnityEngine.Debug.Log("Poppys Chaos case 6");
                        break;
                    case 7:
                        block.additionalBlocks += 2;
                        UnityEngine.Debug.Log("Poppys Chaos case 7");
                        break;
                    case 8:
                        gun.spread += 0.5f;
                        gun.numberOfProjectiles += 1;
                        UnityEngine.Debug.Log("Poppys Chaos case 8");
                        break;
                    case 9:
                        gun.speedMOnBounce *= 0.3f;
                        gun.reflects += 1;

                        UnityEngine.Debug.Log("Poppys Chaos case 9");
                        //example case
                        break;
                    case 10:
                        
                        ChaosPoppycarsCards.Instance.ExecuteAfterFrames(10, () => {
                            var legend = ModdingUtils.Utils.Cards.instance.GetRandomCardWithCondition(player, gun, gunAmmo, data, health, gravity, block, characterStats, LegendCondition);
                            ModdingUtils.Utils.Cards.instance.AddCardToPlayer(player, legend, false, "", 2f, 2f, true);
                            ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, legend, 3f);
                           
                        });
                        UnityEngine.Debug.Log("Poppys Chaos case 10");
                        //example case
                        break;
                    case 11:
                        characterStats.AjustNulls(20);
                        UnityEngine.Debug.Log("Poppys Chaos case 11");
                        //example case
                        break;
                    case 12:
                        block.forceToAdd += 15;
                        UnityEngine.Debug.Log("Poppys Chaos case 12");
                        //example case
                        break;
                    case 13:
                        gravity.gravityForce *= 0.001f;
                        UnityEngine.Debug.Log("Poppys Chaos case 13");
                        //example case
                        break;
                    case 14:
                        gun.bodyRecoil += 30;
                        gun.recoilMuiltiplier += 2f;
                        UnityEngine.Debug.Log("Poppys Chaos case 14");
                        //example case
                        break;
                    case 15:
                        block.objectsToSpawn.Add(ChaosPoppycarsCards.Bundle.LoadAsset<GameObject>("A_Explosion 1"));

                        UnityEngine.Debug.Log("Poppys Chaos case 15");
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
                        UnityEngine.Debug.Log("Poppys Chaos case 16");
                        //example case
                        break;
                    case 17:
                        ChaosPoppycarsCards.Instance.ExecuteAfterFrames(10, () => {
                            CurseManager.instance.CursePlayer(player, (curse) => {
                                ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, curse, 3f);
                            });
                            CurseManager.instance.CursePlayer(player, (curse) => {
                                ModdingUtils.Utils.CardBarUtils.instance.ShowImmediate(player, curse, 3f);
                            });
                        });
                        UnityEngine.Debug.Log("Poppys Chaos case 17");
                        //example case
                        break;
                    case 18:
                        gun.knockback *= 3;
                        UnityEngine.Debug.Log("Poppys Chaos case 18");
                        //example case
                        break;
                    case 19:
                        gun.bodyRecoil += 30;
                        gun.recoilMuiltiplier += 2f;
                        gun.recoilMuiltiplier *= -1f;
                        UnityEngine.Debug.Log("Poppys Chaos case 19");
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
                        UnityEngine.Debug.Log("Poppys Chaos case 20");
                        //example case
                        break;
                    case 21:
                        characterStats.respawns += 1;
                        UnityEngine.Debug.Log("Poppys Chaos case 21");
                        //example case
                        break;
                    case 22:
                        characterStats.regen += 15;
                        UnityEngine.Debug.Log("Poppys Chaos case 22");
                        //example case
                        break;
                    case 23:
                        var addedObj = Instantiate((ChaosPoppycarsCards.Bundle.LoadAsset<GameObject>("A_DemonicPact 1")), player.transform.position, player.transform.rotation, player.transform);
                        characterStats.objectsAddedToPlayer.Add(addedObj);
                        gun.damage *= 2f;
                        gunAmmo.reloadTime *= 0.75f;
                        UnityEngine.Debug.Log("Poppys Chaos case 23");
                        //example case
                        break;
                    case 24:
                        //var addedObj2 = Instantiate((ChaosPoppycarsCards.Bundle.LoadAsset<GameObject>("A_DemonicPact 2")), player.transform.position, player.transform.rotation, player.transform);
                        //characterStats.objectsAddedToPlayer.Add(addedObj2);
                        gun.objectsToSpawn = new List<ObjectsToSpawn>(gun.objectsToSpawn)
                        {

                           new ObjectsToSpawn ()
                           {
                                AddToProjectile = ChaosPoppycarsCards.Bundle.LoadAsset<GameObject>("HealthBounceObject")

                           },
                        }.ToArray();
                        gun.reflects += 3;
                        var mono = player.gameObject.GetOrAddComponent<HealthBounceMono>();
                        HealthBouncesCASE = true;
                        UnityEngine.Debug.Log("Poppys Chaos case 24");
                        //example case
                        break;
                    case 25:
                        characterStats.movementSpeed *= 2.5f;
                        UnityEngine.Debug.Log("Poppys Chaos case 25");
                        //example case
                        break;
                    case 26:
                        gun.dontAllowAutoFire = true;
                        gun.damage *= 3;
                        UnityEngine.Debug.Log("Poppys Chaos case 26");
                        //example case
                        break;
                    case 27:
                        data.maxHealth /= 2;
                        characterStats.regen += 35;
                        UnityEngine.Debug.Log("Poppys Chaos case 27");
                        //example case
                        break;
                    case 28:
                        characterStats.lifeSteal *= 1.5f;
                        UnityEngine.Debug.Log("Poppys Chaos case 28");
                        //example case
                        break;
                    case 29:
                        gravity.gravityForce *= -0.05f;
                        characterStats.sizeMultiplier *= -1;
                        data.jumps += 9;
                        characterStats.jump *= -1f;
                        UnityEngine.Debug.Log("Poppys Chaos case 29");
                        //example case
                        break;
                    case 30:
                        gun.reflects += 25;
                        UnityEngine.Debug.Log("Poppys Chaos case 30");
                        //example case
                        break;
                    case 31:
                        characterStats.GetAdditionalData().shuffles += 2;
                        UnityEngine.Debug.Log("Poppys Chaos case 31");
                        //example case
                        break;
                    case 32:
                        player.data.movement.airControl /= 5;
                        UnityEngine.Debug.Log("Poppys Chaos case 32");
                        //example case
                        break;
                    case 33:
                        player.data.movement.airControl *= 5;
                        UnityEngine.Debug.Log("Poppys Chaos case 33");
                        //example case
                        break;
                    case 34:
                        //UnityEngine.Vector3 vectorThing = new UnityEngine.Vector3(1f, 2f, 1f);
                        //gun.transform.localScale += vectorThing;
                        gun.damage = 0.25f;
                        UnityEngine.Debug.Log("Poppys Chaos case 34");
                        //example case
                        break;
                    case 35:
                        gunAmmo.reloadTimeMultiplier *= 1.75f;
                        UnityEngine.Debug.Log("Poppys Chaos case 35");
                        //example case
                        break;
                    case 36:
                        gunAmmo.ammoReg -= 0.5f;
                        gunAmmo.maxAmmo = 1;
                        gunAmmo.reloadTimeAdd += 0.75f;
                        UnityEngine.Debug.Log("Poppys Chaos case 36");
                        //example case
                        break;
                    case 37:
                        characterStats.respawns -= 1;
                        characterStats.health *= 0.75f;
                        characterStats.sizeMultiplier = 1f;
                        UnityEngine.Debug.Log("Poppys Chaos case 37");
                        //example case
                        break;
                    case 38:
                        characterStats.numberOfJumps *= 0;
                        data.jumps *= 0;
                        UnityEngine.Debug.Log("Poppys Chaos case 38");
                        //example case
                        break;
                    case 39:
                        gun.damage = 50 / 28;
                        gun.damage *= 0.5f;
                        UnityEngine.Debug.Log("Poppys Chaos case 39");
                        //example case
                        break;
                    case 40:
                        gun.drag += 2.5f;
                        gun.gravity *= 0f;
                        gun.cos += 0.5f;
                        UnityEngine.Debug.Log("Poppys Chaos case 40");
                        //example case
                        break;
                    case 41:
                        gun.destroyBulletAfter += 0.5f;

                        UnityEngine.Debug.Log("Poppys Chaos case 41");
                        //example case
                        break;
                    case 42:
                        block.SetFieldValue("timeBetweenBlocks", ((float)block.GetFieldValue("timeBetweenBlocks")) + 0.4f);
                        BlockCase = true;
                        UnityEngine.Debug.Log("Poppys Chaos case 42");
                        //example case
                        break;
                    case 43:
                        gun.dontAllowAutoFire = true;
                        gun.percentageDamage -= 0.15f;
                        gun.damage *= 0.75f;
                        UnityEngine.Debug.Log("Poppys Chaos case 43");
                        //example case
                        break;
                    case 44:
                        gun.ignoreWalls = false;
                        gun.damageAfterDistanceMultiplier *= 0.5f;
                        UnityEngine.Debug.Log("Poppys Chaos case 44");
                        //example case
                        break;
                    /*case 45:
                        foreach (CardInfo.Rarity r in Enum.GetValues(typeof(CardInfo.Rarity)))
                        {
                            if (r == CardInfo.Rarity.Common || r == RarityUtils.GetRarity("Trinket"))
                            {
                                continue;
                            }
                            var commonCategory = CustomCardCategories.instance.CardCategory($"__rarity-{r}");
                            ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Add(commonCategory);
                        }
                            UnityEngine.Debug.Log("Poppys Chaos case 44");
                        //example case
                        break;*/
                    default:
                        UnityEngine.Debug.LogError("Unexpected random number value: " + randomNumber);
                        break;
                }
            }

            GenerateAndPerformEffect();
        }

        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            if (HealthBouncesCASE == true)
            {
                UnityEngine.GameObject.Destroy(player.GetComponent<HealthBounceMono>());
            }
            if (BlockCase == true)
            {
                block.SetFieldValue("timeBetweenBlocks", ((float)block.GetFieldValue("timeBetweenBlocks")) - 0.4f);
            }
            CPCDebug.Log($"[{ChaosPoppycarsCards.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
            //Run when the card is removed from the player
        }


        protected override string GetTitle()
        {
            return "Poppys Chaos";
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
        internal static IEnumerator ExtraPicks()
        {
            foreach (Player player in PlayerManager.instance.players.ToArray())
            {

                while (player.data.stats.GetAdditionalData().shuffles > 0)
                {
                    player.data.stats.GetAdditionalData().shuffles -= 1;
                    yield return GameModeManager.TriggerHook(GameModeHooks.HookPlayerPickStart);
                    CardChoiceVisuals.instance.Show(Enumerable.Range(0, PlayerManager.instance.players.Count).Where(i => PlayerManager.instance.players[i].playerID == player.playerID).First(), true);
                    yield return CardChoice.instance.DoPick(1, player.playerID, PickerType.Player);
                    yield return new WaitForSecondsRealtime(0.1f);
                    yield return GameModeManager.TriggerHook(GameModeHooks.HookPlayerPickEnd);
                    yield return new WaitForSecondsRealtime(0.1f);
                }
            }
            yield break;
        }
    }
}
