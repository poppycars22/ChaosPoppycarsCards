using BepInEx;
using UnboundLib;
using UnboundLib.Cards;
using ChaosPoppycarsCards.Cards;
using ChaosPoppycarsCards.Cards.Minecrafter;
using HarmonyLib;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using UnityEngine;
using ModdingUtils;
using ModdingUtils.Extensions;
using System.Collections;
using UnboundLib.GameModes;
using Jotunn.Utils;
using System.Linq;
using System.Collections.ObjectModel;
using System;
using WillsWackyManagers.Utils;
using RarityLib.Utils;

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
    // Declares our mod to Bepin
    [BepInPlugin(ModId, ModName, Version)]

    // The game our mod is associated with
    [BepInProcess("Rounds.exe")]
    public class ChaosPoppycarsCards : BaseUnityPlugin
    {
        private const string ModId = "com.Poppycars.CPC.Id";
        private const string ModName = "ChaosPoppycarsCards";
        public const string Version = "0.7.3"; // What version are we on (major.minor.patch)?
        public const string ModInitials = "CPC";
        public static ChaosPoppycarsCards Instance { get; private set; }
        public static object CPC_Assets { get; internal set; }

        public static AssetBundle Bundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("cpcart", typeof(ChaosPoppycarsCards).Assembly);

        void Awake()
        {
            // Use this to call any harmony patch files your mod may have
            CardThemeLib.CardThemeLib.instance.CreateOrGetType("Evergreen", new CardThemeColor() { bgColor = new Color(0.09f, 0.23f, 0.11f), targetColor = new Color(0.28f, 0.80f, 0.32f) });
            RarityUtils.AddRarity("Goose", 6, new Color(0.722f, 0.840f, 0.775f), new Color(0.860f, 1.00f, 0.923f));
            var harmony = new Harmony(ModId);
            harmony.PatchAll();
        }
        private void Start()

        {
            Instance = this;
            GameModeManager.AddHook(GameModeHooks.HookGameStart, this.GameStart);

            ChaosPoppycarsCards.ArtAssets = AssetUtils.LoadAssetBundleFromResources("cpccart", typeof(ChaosPoppycarsCards).Assembly);

            CustomCard.BuildCard<Sugared>();
            CustomCard.BuildCard<GETAWAY>();
            CustomCard.BuildCard<AttackSpeed>((card) => AttackSpeed.Card = card);
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
            CustomCard.BuildCard<Larmor>((card) => Larmor.Card = card);
            CustomCard.BuildCard<Carmor>((card) => Carmor.Card = card);
            CustomCard.BuildCard<Iarmor>((card) => Iarmor.Card = card);
            CustomCard.BuildCard<Garmor>((card) => Garmor.Card = card);
            CustomCard.BuildCard<Darmor>((card) => Darmor.Card = card);
            CustomCard.BuildCard<Narmor>((card) => Narmor.Card = card);
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
            CustomCard.BuildCard<InvisPotion>((card) => InvisPotion.Card = card);
            CustomCard.BuildCard<TimesNegativeOne>();
            CustomCard.BuildCard<StoneHoe>((card) => StoneHoe.Card = card);
            CustomCard.BuildCard<GoldHoe>((card) => GoldHoe.Card = card);
            CustomCard.BuildCard<IronHoe>((card) => IronHoe.Card = card);
            CustomCard.BuildCard<DiamondHoe>((card) => DiamondHoe.Card = card);
            CustomCard.BuildCard<NetheriteHoe>((card) => NetheriteHoe.Card = card);
            CustomCard.BuildCard<WormholeClip>();
            CustomCard.BuildCard<MCShield>((card) => MCShield.Card = card);
            CustomCard.BuildCard<CocaCola>();
            CustomCard.BuildCard<Pepsi>();
            CustomCard.BuildCard<DrPepper>();
            CustomCard.BuildCard<SpriteSoda>();
            CustomCard.BuildCard<BouncyBombs>();
            CustomCard.BuildCard<MountainDewSoda>();
            CustomCard.BuildCard<LightSaber>();
            CustomCard.BuildCard<CraftingTable>((card) => CraftingTable.Card = card);
            CustomCard.BuildCard<BrewingStand>((card) => BrewingStand.Card = card);
            CustomCard.BuildCard<FlammingArrows>((card) => FlammingArrows.Card = card);
            CustomCard.BuildCard<PoisonArrows>((card) => PoisonArrows.Card = card);
            CustomCard.BuildCard<ToxicArrows>((card) => ToxicArrows.Card = card);
            CustomCard.BuildCard<ExplosiveArrows>((card) => ExplosiveArrows.Card = card);
            CustomCard.BuildCard<BouncyArrows>((card) => BouncyArrows.Card = card);
            CustomCard.BuildCard<Arrows>((card) => Arrows.Card = card);
            CustomCard.BuildCard<Punch2>((card) => Punch2.Card = card);
            CustomCard.BuildCard<SpeedCurse>(cardInfo => { CurseManager.instance.RegisterCurse(cardInfo); });
            CustomCard.BuildCard<BlockFlipCurse>(cardInfo => { CurseManager.instance.RegisterCurse(cardInfo); });
            CustomCard.BuildCard<NerfCurse>(cardInfo => { CurseManager.instance.RegisterCurse(cardInfo); });
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
            CustomCard.BuildCard<PoppysChaos>();
            CustomCard.BuildCard<PercentageBullets>();
            CustomCard.BuildCard<BalloonBullets>();
            CustomCard.BuildCard<IcySprings>();
            CustomCard.BuildCard<GeeseSwarm>();
            CustomCard.BuildCard<Goose>();
            CustomCard.BuildCard<Whynack>();
            CustomCard.BuildCard<Anarkey>(cardInfo => { CurseManager.instance.RegisterCurse(cardInfo); });
            GameModeManager.AddHook(GameModeHooks.HookRoundEnd, UpgradeAction);
            
            //  GameModeManager.AddHook(GameModeHooks.HookBattleStart, LightSaberRangeReset);
        }


        
        private IEnumerator UpgradeAction(IGameModeHandler gm)
        {

            yield return WoodenSword.UpgradeSword(gm);
            yield return AttackSpeed.UpgradeHoe(gm);
            yield return WoodenAxe.UpgradeAxe(gm);
            yield return Larmor.UpgradeArmor(gm);

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
