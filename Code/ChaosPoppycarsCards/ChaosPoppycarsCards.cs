using BepInEx;
using UnityEngine;
using UnboundLib;
using UnboundLib.Cards;
using ChaosPoppycarsCards.Cards;
using ChaosPoppycarsCards.Cards.Minecrafter;
using HarmonyLib;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using ModdingUtils;
//using ModdingUtils.Extensions;
using System.Collections;
using UnboundLib.GameModes;
using Jotunn.Utils;
using System.Linq;
using System.Collections.ObjectModel;
using System;
using WillsWackyManagers.Utils;
using RarityLib.Utils;
using System.Collections.Generic;
using static ChaosPoppycarsCards.Utilities.CardUtils;
using ChaosPoppycarsCards.MonoBehaviours;
using ChaosPoppycarsCards.Extensions;
using ChaosPoppycarsCards.Patches;
using CPCCardInfostuffs;
using CPCTabInfoSTATS;
using BepInEx.Configuration;
using System.Diagnostics;
using UnboundLib.Utils.UI;
using System.Reflection;
using UnboundLib.Utils;
using InControl;
using PlayerActionsHelper;
//using PlayerActionsHelper;

namespace ChaosPoppycarsCards
{

    // These are the mods required for our mod to work
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.playerjumppatch", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.moddingutils", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("root.classes.manager.reborn", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.willuwontu.rounds.managers", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("root.rarity.lib", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.root.projectile.size.patch", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("root.cardtheme.lib", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.Root.Null", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.CrazyCoders.Rounds.RarityBundle", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.willuwontu.rounds.attacklevelPatch", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.rounds.willuwontu.ActionHelper", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.Poppycars.PSA.Id", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.root.player.time", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("com.willuwontu.rounds.tabinfo", BepInDependency.DependencyFlags.SoftDependency)]
    // Declares our mod to Bepin
    [BepInPlugin(ModId, ModName, Version)]

    // The game our mod is associated with
    [BepInProcess("Rounds.exe")]
    public class ChaosPoppycarsCards : BaseUnityPlugin
    {
        private const string ModId = "com.Poppycars.CPC.Id";
        private const string ModName = "ChaosPoppycarsCards";
        public const string Version = "1.4.2"; // What version are we on (major.minor.patch)?
        public const string ModInitials = "CPC";
        internal static List<BaseUnityPlugin> plugins;
        public static ChaosPoppycarsCards Instance { get; private set; }

        public static ConfigEntry<bool> MC_Particles;
        public static object CPC_Assets { get; internal set; }

        public static AssetBundle Bundle = null;
        void Awake()
        {
            Bundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("cpcart", typeof(ChaosPoppycarsCards).Assembly);

            PlayerActionManager.RegisterPlayerAction(new ActionInfo("Dash", new MouseBindingSource(Mouse.MiddleButton),
                new DeviceBindingSource(InputControlType.RightBumper)));

            PlayerActionManager.RegisterPlayerAction(new ActionInfo("BlockMoveSwitch", new KeyBindingSource(Key.R),
                new DeviceBindingSource(InputControlType.DPadLeft)));

            // Use this to call any harmony patch files your mod may have
            CardThemeLib.CardThemeLib.instance.CreateOrGetType("Evergreen", new CardThemeColor() { bgColor = new UnityEngine.Color(0.09f, 0.23f, 0.11f), targetColor = new UnityEngine.Color(0.28f, 0.80f, 0.32f) });

            var harmony = new Harmony(ModId);

            harmony.PatchAll();

            var TESTIG = Bundle.LoadAsset<GameObject>("ModCards");

            var TESTIG2 = TESTIG.GetComponent<CardHolder>();

            TESTIG2.RegisterCards();

            Bundle.LoadAllAssets();

            // PlayerActionManager.RegisterPlayerAction(new ActionInfo("SwitchHoldable", new KeyBindingSource(Key.Q),
            // new DeviceBindingSource(InputControlType.DPadDown)));

            MC_Particles = base.Config.Bind<bool>(ModId, "Minecraft_Particles", true, "Enable Minecraft Particles");
        }
        //REGISTER CURSES
        private void RegisterCards() {

            var assests = Bundle.LoadAllAssets<GameObject>();
            List<Type> types = typeof(ChaosPoppycarsCards).Assembly.GetTypes().Where(type => type.IsClass && !type.IsAbstract && type.IsSubclassOf(typeof(CustomCard))).ToList();
            foreach (var type in types)
            {
                try
                {
                    var card = assests.Where(a => a is GameObject && a.GetComponent<CustomCard>() != null && a.GetComponent<CustomCard>().GetType() == type).First();
                    try
                    {
                        type.GetField("Card", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static).GetValue(null);
                        card.GetComponent<CustomCard>().BuildUnityCard(cardInfo => type.GetField("Card", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static).SetValue(null, cardInfo));
                    }
                    catch
                    {
                        card.GetComponent<CustomCard>().BuildUnityCard(null);
                    }
                }
                catch
                {
                }
            }

            this.ExecuteAfterFrames(10, () => {
                CurseManager.instance.RegisterCurse(Anarkey.Card);
                CurseManager.instance.RegisterCurse(BlockConfusion.Card);
                CurseManager.instance.RegisterCurse(BrittleBullets.Card);
                CurseManager.instance.RegisterCurse(FearfulCurse.Card);
                CurseManager.instance.RegisterCurse(NerfGun.Card);
                CurseManager.instance.RegisterCurse(SpeedCurse.Card);
                CurseManager.instance.RegisterCurse(RandomCurse.Card);
                //CurseManager.instance.RegisterCurse(CursedTablet.Card);
            });
        }

        private void Start()
        {
            plugins = (List<BaseUnityPlugin>)typeof(BepInEx.Bootstrap.Chainloader).GetField("_plugins", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null);
            Instance = this;
            GameModeManager.AddHook(GameModeHooks.HookGameStart, this.GameStart);

            if (plugins.Exists(plugin => plugin.Info.Metadata.GUID == "com.willuwontu.rounds.tabinfo"))
            {
                TabinfoInterface.Setup();
            }

            ChaosPoppycarsCards.ArtAssets=AssetUtils.LoadAssetBundleFromResources("cpccart", typeof(ChaosPoppycarsCards).Assembly);
            RegisterCards();
            Unbound.RegisterMenu(ModName, delegate () { }, new Action<GameObject>(this.NewGUI), null, false);
            /*
                        CustomCard.BuildCard<Sugared>();
                        CustomCard.BuildCard<Getaway>();
                        CustomCard.BuildCard<WoodenHoe>((card) => WoodenHoe.Card = card);
                        CustomCard.BuildCard<WoodenSword>((card) => WoodenSword.Card = card);
                        CustomCard.BuildCard<DoubleDuplicator>();
                        CustomCard.BuildCard<Duplicator>();
                        CustomCard.BuildCard<StoneSword>((card) => StoneSword.Card = card);
                        CustomCard.BuildCard<GoldSword>((card) => GoldSword.Card = card);
                        CustomCard.BuildCard<IronSword>((card) => IronSword.Card = card);
                        CustomCard.BuildCard<DiamondSword>((card) => DiamondSword.Card = card);
                        CustomCard.BuildCard<NetheriteSword>((card) => NetheriteSword.Card = card);
                        CustomCard.BuildCard<MCBow>((card) => MCBow.Card = card);
                        CustomCard.BuildCard<AmmoChest>();
                        CustomCard.BuildCard<StrengthPotion>((card) => StrengthPotion.Card = card);
                        CustomCard.BuildCard<SpeedPotion>((card) => SpeedPotion.Card = card);
                        CustomCard.BuildCard<LetherArmor>((card) => LetherArmor.Card = card);
                        CustomCard.BuildCard<ChainArmor>((card) => ChainArmor.Card = card);
                        CustomCard.BuildCard<IronArmor>((card) => IronArmor.Card = card);
                        CustomCard.BuildCard<GoldArmor>((card) => GoldArmor.Card = card);
                        CustomCard.BuildCard<DiamondArmor>((card) => DiamondArmor.Card = card);
                        CustomCard.BuildCard<NetheriteArmor>((card) => NetheriteArmor.Card = card);
                        CustomCard.BuildCard<JumpPotion>((card) => JumpPotion.Card = card);
                        CustomCard.BuildCard<RegenPotion>((card) => RegenPotion.Card = card);
                        CustomCard.BuildCard<ActivatedDuplicator>();
                        CustomCard.BuildCard<UltimatePotion>((card) => UltimatePotion.Card = card);
                        CustomCard.BuildCard<WoodenAxe>((card) => WoodenAxe.Card = card);
                        CustomCard.BuildCard<BouncyGel>();
                        CustomCard.BuildCard<StoneAxe>((card) => StoneAxe.Card = card);
                        CustomCard.BuildCard<IronAxe>((card) => IronAxe.Card = card);
                        CustomCard.BuildCard<GoldAxe>((card) => GoldAxe.Card = card);
                        CustomCard.BuildCard<DiamondAxe>((card) => DiamondAxe.Card = card);
                        CustomCard.BuildCard<NetheriteAxe>((card) => NetheriteAxe.Card = card);
                        CustomCard.BuildCard<TotemOfUndying>((card) => TotemOfUndying.Card = card);
                        CustomCard.BuildCard<InvisablityPotion>((card) => InvisablityPotion.Card = card);
                        CustomCard.BuildCard<Negafy>();
                        CustomCard.BuildCard<StoneHoe>((card) => StoneHoe.Card = card);
                        CustomCard.BuildCard<GoldHoe>((card) => GoldHoe.Card = card);
                        CustomCard.BuildCard<IronHoe>((card) => IronHoe.Card = card);
                        CustomCard.BuildCard<DiamondHoe>((card) => DiamondHoe.Card = card);
                        CustomCard.BuildCard<NetheriteHoe>((card) => NetheriteHoe.Card = card);
                        CustomCard.BuildCard<WormHoleClip>();
                        CustomCard.BuildCard<MCShield>((card) => MCShield.Card = card);
                        CustomCard.BuildCard<CocaCola>();
                        CustomCard.BuildCard<Pepsi>();
                        CustomCard.BuildCard<DrPepper>();
                        CustomCard.BuildCard<Cards.Sprite>();
                        CustomCard.BuildCard<BouncyBombs>();
                        CustomCard.BuildCard<MountainDew>();
                        CustomCard.BuildCard<LightSaber>();
                        CustomCard.BuildCard<CraftingTable>((card) => CraftingTable.Card = card);
                        CustomCard.BuildCard<BrewingStand>((card) => BrewingStand.Card = card);
                        CustomCard.BuildCard<FlamingArrows>((card) => FlamingArrows.Card = card);
                        CustomCard.BuildCard<PoisonArrows>((card) => PoisonArrows.Card = card);
                        CustomCard.BuildCard<ToxicArrows>((card) => ToxicArrows.Card = card);
                        CustomCard.BuildCard<ExplosiveArrows>((card) => ExplosiveArrows.Card = card);
                        CustomCard.BuildCard<BouncyArrows>((card) => BouncyArrows.Card = card);
                        CustomCard.BuildCard<Arrows>((card) => Arrows.Card = card);
                        CustomCard.BuildCard<PunchII>((card) => PunchII.Card = card);
                        CustomCard.BuildCard<SpeedCurse>(cardInfo => { CurseManager.instance.RegisterCurse(cardInfo); });
                        CustomCard.BuildCard<BlockConfusion>(cardInfo => { CurseManager.instance.RegisterCurse(cardInfo); });
                        CustomCard.BuildCard<NerfGun>(cardInfo => { CurseManager.instance.RegisterCurse(cardInfo); });
                        CustomCard.BuildCard<SpeedDemon>((card) => SpeedDemon.Card = card);
                        CustomCard.BuildCard<MomentumShots>((card) => MomentumShots.Card = card);
                        CustomCard.BuildCard<SpeedyHands>((card) => SpeedyHands.Card = card);
                        CustomCard.BuildCard<Tricky>((card) => Tricky.Card = card);
                        CustomCard.BuildCard<TriggerFinger>((card) => TriggerFinger.Card = card);
                        CustomCard.BuildCard<Swifter>((card) => Swifter.Card = card);
                        CustomCard.BuildCard<Stretches>((card) => Stretches.Card = card);
                        CustomCard.BuildCard<LegDay>((card) => LegDay.Card = card);
                        CustomCard.BuildCard<SpeedstersGun>((card) => SpeedstersGun.Card = card);
                        CustomCard.BuildCard<AirHops>((card) => AirHops.Card = card);
                        // CustomCard.BuildCard<WoodenPickaxe>();
                        // CustomCard.BuildCard<StonePickaxe>();
                        // CustomCard.BuildCard<GoldPickaxe>();
                        CustomCard.BuildCard<PoppysChaos>((card) => PoppysChaos.Card = card);
                        CustomCard.BuildCard<PercentageBullets>();
                        CustomCard.BuildCard<BalloonBullets>();
                        CustomCard.BuildCard<IcySprings>();
                        CustomCard.BuildCard<GeeseSwarm>();
                        CustomCard.BuildCard<Goose>((card) => Goose.Card = card);
                        CustomCard.BuildCard<Whynack>();
                        CustomCard.BuildCard<Anarkey>(cardInfo => { CurseManager.instance.RegisterCurse(cardInfo); });
                        CustomCard.BuildCard<FWPhantom>();
                        CustomCard.BuildCard<Peptide>();
                        CustomCard.BuildCard<ScareJackpot>();
                        CustomCard.BuildCard<HealingBlock>();
                        CustomCard.BuildCard<AbsorbingBullets>();
                        CustomCard.BuildCard<RoyalGifting>();
                        CustomCard.BuildCard<BrittleBullets>(cardInfo => { CurseManager.instance.RegisterCurse(cardInfo); });
                        CustomCard.BuildCard<FearfulCurse>(cardInfo => { CurseManager.instance.RegisterCurse(cardInfo); });
                        CustomCard.BuildCard<LegendaryJackpot>();
                        CustomCard.BuildCard<SuperSprings>();
                        CustomCard.BuildCard<JumpPower>();
                        CustomCard.BuildCard<JumpBursts>();
                        CustomCard.BuildCard<JumpSpeed>();
                        CustomCard.BuildCard<JumpShrink>();
                        CustomCard.BuildCard<Nullgendary>();
                        CustomCard.BuildCard<FriendNulls>();
                        CustomCard.BuildCard<KnifeGoose>((card) => KnifeGoose.Card = card);
                        CustomCard.BuildCard<GoldGoose>((card) => GoldGoose.Card = card);
                        CustomCard.BuildCard<HealthBounces>();
                        //CustomCard.BuildCard<WoodenShovel>((card) => WoodenShovel.Card = card);*/
            GameModeManager.AddHook(GameModeHooks.HookRoundEnd, UpgradeAction);
            GameModeManager.AddHook(GameModeHooks.HookPointEnd, PointEnd);
            GameModeManager.AddHook(GameModeHooks.HookPlayerPickEnd, (gm) => PoppysChaos.ExtraPicks());
            //  GameModeManager.AddHook(GameModeHooks.HookBattleStart, LightSaberRangeReset);
            // make cards mutually exclusive
            this.ExecuteAfterFrames(10, () =>
            {
                /*if (GetCardInfo("Boomerang") != null)
                {
                    CardInfo otherCard = GetCardInfo("Boomerang");
                    MakeExclusive("Boomerang", "Light Saber");

                    List<CardCategory> newList = otherCard.categories.ToList();

                    otherCard.categories = newList.ToArray();
                }
                if (GetCardInfo("Shields Up") != null)
                {
                    CardInfo otherCard = GetCardInfo("Shields Up");
                    MakeExclusive("Shields Up", "Light Saber");

                    List<CardCategory> newList = otherCard.categories.ToList();

                    otherCard.categories = newList.ToArray();
                }
                if (GetCardInfo("Drive") != null)
                {
                    CardInfo otherCard = GetCardInfo("Drive");
                    MakeExclusive("Drive", "Light Saber");

                    List<CardCategory> newList = otherCard.categories.ToList();

                    otherCard.categories = newList.ToArray();
                }
                if (GetCardInfo("Flak Cannon") != null)
                {
                    CardInfo otherCard = GetCardInfo("Flak Cannon");
                    MakeExclusive("Flak Cannon", "Light Saber");

                    List<CardCategory> newList = otherCard.categories.ToList();

                    otherCard.categories = newList.ToArray();
                }
                if (GetCardInfo("Stasis") != null)
                {
                    CardInfo otherCard = GetCardInfo("Stasis");
                    MakeExclusive("Stasis", "Light Saber");

                    List<CardCategory> newList = otherCard.categories.ToList();

                    otherCard.categories = newList.ToArray();
                }
                if (GetCardInfo("Bullet Time") != null)
                {
                    CardInfo otherCard = GetCardInfo("Bullet Time");
                    MakeExclusive("Bullet Time", "Light Saber");

                    List<CardCategory> newList = otherCard.categories.ToList();

                    otherCard.categories = newList.ToArray();
                }
                if (GetCardInfo("Drone") != null)
                {
                    CardInfo otherCard = GetCardInfo("Drone");
                    MakeExclusive("Drone", "Light Saber");

                    List<CardCategory> newList = otherCard.categories.ToList();

                    otherCard.categories = newList.ToArray();
                }
                if (GetCardInfo("Focus") != null)
                {
                    CardInfo otherCard = GetCardInfo("Focus");
                    MakeExclusive("Focus", "Light Saber");

                    List<CardCategory> newList = otherCard.categories.ToList();

                    otherCard.categories = newList.ToArray();
                }
                if (GetCardInfo("Sun") != null)
                {
                    CardInfo otherCard = GetCardInfo("Sun");
                    MakeExclusive("Sun", "Light Saber");

                    List<CardCategory> newList = otherCard.categories.ToList();

                    otherCard.categories = newList.ToArray();
                }
                if (GetCardInfo("Flex Seal") != null)
                {
                    CardInfo otherCard = GetCardInfo("Flex Seal");
                    MakeExclusive("Flex Seal", "Light Saber");

                    List<CardCategory> newList = otherCard.categories.ToList();

                    otherCard.categories = newList.ToArray();
                }
                if (GetCardInfo("Rex") != null)
                {
                    CardInfo otherCard = GetCardInfo("Rex");
                    MakeExclusive("Rex", "Light Saber");

                    List<CardCategory> newList = otherCard.categories.ToList();

                    otherCard.categories = newList.ToArray();
                }
                if (GetCardInfo("Anti Material Rifle​") != null)
                {
                    CardInfo otherCard = GetCardInfo("Anti Material Rifle​");
                    MakeExclusive("Anti Material Rifle​", "Light Saber");

                    List<CardCategory> newList = otherCard.categories.ToList();

                    otherCard.categories = newList.ToArray();
                }
                if (GetCardInfo("Hyper Sonic​") != null)
                {
                    CardInfo otherCard = GetCardInfo("Hyper Sonic​");
                    MakeExclusive("Hyper Sonic​", "Light Saber");

                    List<CardCategory> newList = otherCard.categories.ToList();

                    otherCard.categories = newList.ToArray();
                }
                if (GetCardInfo("Squid​") != null)
                {
                    CardInfo otherCard = GetCardInfo("Squid​");
                    MakeExclusive("Squid​", "Light Saber");

                    List<CardCategory> newList = otherCard.categories.ToList();

                    otherCard.categories = newList.ToArray();
                }*/
            });
            ExtensionMethods.ExecuteAfterFrames(this, 60, delegate ()
            {
                Enumerable.ToList<Card>(CardManager.cards.Values).ForEach(delegate (Card card)
                {
                    this.AddMod(card);
                });
            });
        }
        private void AddMod(Card card)
        {
            string text = "__Rarity-" + card.cardInfo.rarity;
            CardCategory cardCategory = CustomCardCategories.instance.CardCategory(text);
            CardCategory[] categories = CollectionExtensions.AddToArray<CardCategory>(card.cardInfo.categories, cardCategory);
            card.cardInfo.categories = categories;
        }
        private void NewGUI(GameObject menu)
        {
            MenuHandler.CreateText(ModName, menu, out _, 60, false, null, null, null, null);

            MenuHandler.CreateToggle(MC_Particles.Value, "Enable Minecraft Particles (only effects potions right now)", menu, value => MC_Particles.Value = value);
            
            MenuHandler.CreateText("", menu, out _);
        }


        private IEnumerator UpgradeAction(IGameModeHandler gm)
        {

            yield return WoodenSword.UpgradeSword(gm);
            yield return WoodenHoe.UpgradeHoe(gm);
            yield return WoodenAxe.UpgradeAxe(gm);
            yield return LetherArmor.UpgradeArmor(gm);
           

        }
        IEnumerator PointEnd(IGameModeHandler gm)
        {
            foreach(var player in PlayerManager.instance.players)
            {
                player.data.stats.GetAdditionalData().firstHit = true;
                player.data.stats.GetAdditionalData().firstDamage = true;
                //player.data.stats.GetAdditionalData().damageMult = player.data.stats.GetAdditionalData().damageMultMax;
            }
            yield break;
        }
      
        /* private IEnumerator LightSaberRangeReset(IGameModeHandler gm)
         {
             yield return LightSaber.RangeResetTruth(gm);
         } */
        IEnumerator GameStart(IGameModeHandler gm)
        {
            // Runs at start of match
            foreach (var player in PlayerManager.instance.players)
            {
                ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Remove(CPCCardCategories.PotionCategory);
                
                ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Add(CPCCardCategories.GeeseCategory);

                 
                

            }
            yield break;

            }
        
        internal static AssetBundle ArtAssets;
    }
    static class CPCCardCategories
    {
        public static CardCategory PotionCategory = CustomCardCategories.instance.CardCategory("UltimatePotion");
        public static CardCategory GeeseCategory = CustomCardCategories.instance.CardCategory("GeeseCategory");
    }
}
