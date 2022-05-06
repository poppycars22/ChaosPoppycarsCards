using BepInEx;
using UnboundLib;
using UnboundLib.Cards;
using ChaosPoppycarsCards.Cards;
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

namespace ChaosPoppycarsCards
{

    // These are the mods required for our mod to work
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.playerjumppatch", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.moddingutils", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch", BepInDependency.DependencyFlags.HardDependency)]
    // Declares our mod to Bepin
    [BepInPlugin(ModId, ModName, Version)]

    // The game our mod is associated with
    [BepInProcess("Rounds.exe")]
    public class ChaosPoppycarsCards : BaseUnityPlugin
    {
        private const string ModId = "com.Poppycars.CPC.Id";
        private const string ModName = "ChaosPoppycarsCards";
        public const string Version = "0.4.2"; // What version are we on (major.minor.patch)?
        public const string ModInitials = "CPC";
        public static ChaosPoppycarsCards Instance { get; private set; }
        public static AssetBundle Bundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("cpcart", typeof(ChaosPoppycarsCards).Assembly);
        
        public static GameObject ShieldArt = Bundle.LoadAsset<GameObject>("C_MinecraftSheild");
        void Awake()
        {
            // Use this to call any harmony patch files your mod may have
            var harmony = new Harmony(ModId);
            harmony.PatchAll();
        }
        void Start()

        {
            Instance = this;
            GameModeManager.AddHook(GameModeHooks.HookGameStart, this.GameStart);
            ChaosPoppycarsCards.ArtAssets = AssetUtils.LoadAssetBundleFromResources("cpccart", typeof(ChaosPoppycarsCards).Assembly);
            CustomCard.BuildCard<Sugared>();
            CustomCard.BuildCard<GETAWAY>();
            CustomCard.BuildCard<AttackSpeed>();
            CustomCard.BuildCard<WoodenSword>();
            CustomCard.BuildCard<DoubleDuplicator>();
            CustomCard.BuildCard<Duplicator>();
            CustomCard.BuildCard<StoneSword>();
            CustomCard.BuildCard<GoldSword>();
            CustomCard.BuildCard<IronSword>();
            CustomCard.BuildCard<DiamondSword>();
            CustomCard.BuildCard<NetheriteSword>();
            CustomCard.BuildCard<MCBow>();
            CustomCard.BuildCard<AmmoChest>();
            CustomCard.BuildCard<StrengthPotion>();
            CustomCard.BuildCard<SpeedPotion>();
            CustomCard.BuildCard<Larmor>();
            CustomCard.BuildCard<Carmor>();
            CustomCard.BuildCard<Iarmor>();
            CustomCard.BuildCard<Garmor>();
            CustomCard.BuildCard<Darmor>();
            CustomCard.BuildCard<Narmor>();
            CustomCard.BuildCard<JumpPotion>();
            CustomCard.BuildCard<RegenPotion>();
            CustomCard.BuildCard<ActivatedDuplicator>();
            CustomCard.BuildCard<UltimatePotion>();
            CustomCard.BuildCard<WoodenAxe>();
            CustomCard.BuildCard<BouncyGel>();
            CustomCard.BuildCard<StoneAxe>();
            CustomCard.BuildCard<IronAxe>();
            CustomCard.BuildCard<GoldAxe>();
            CustomCard.BuildCard<DiamondAxe>();
            CustomCard.BuildCard<NetheriteAxe>();
            CustomCard.BuildCard<TotemOfUndying>();
            CustomCard.BuildCard<InvisPotion>();
            CustomCard.BuildCard<TimesNegativeOne>();
            CustomCard.BuildCard<StoneHoe>();
            CustomCard.BuildCard<GoldHoe>();
            CustomCard.BuildCard<IronHoe>();
            CustomCard.BuildCard<DiamondHoe>();
            CustomCard.BuildCard<NetheriteHoe>();
            CustomCard.BuildCard<WormholeClip>();
            CustomCard.BuildCard<MCShield>();
            CustomCard.BuildCard<CocaCola>();
            CustomCard.BuildCard<Pepsi>();
            CustomCard.BuildCard<DrPepper>();
            CustomCard.BuildCard<SpriteSoda>();
            CustomCard.BuildCard<BouncyBombs>();
            //  CustomCard.BuildCard<SpeedBounce>();
            // CustomCard.BuildCard<WoodenPickaxe>();
            // CustomCard.BuildCard<StonePickaxe>();
            // CustomCard.BuildCard<GoldPickaxe>();
            //  CustomCard.BuildCard<PoppysChaos>();
        }
        IEnumerator GameStart(IGameModeHandler gm)
        {
            // Runs at start of match
            foreach (var player in PlayerManager.instance.players)
            {
                ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Add(CPCCardCategories.StoneSwordCategory);
                ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Add(CPCCardCategories.IronSwordCategory);
                ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Add(CPCCardCategories.DiamondSwordCategory);
                ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Add(CPCCardCategories.NetheriteSwordCategory);
                ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Add(CPCCardCategories.StoneAxeCategory);
                ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Add(CPCCardCategories.IronAxeCategory);
                ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Add(CPCCardCategories.DiamondAxeCategory);
                ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Add(CPCCardCategories.NetheriteAxeCategory);
                ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Add(CPCCardCategories.StoneHoeCategory);
                ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Add(CPCCardCategories.IronHoeCategory);
                ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Add(CPCCardCategories.DiamondHoeCategory);
                ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Add(CPCCardCategories.NetheriteHoeCategory);
                ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Add(CPCCardCategories.ChainArmorCategory);
                ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Add(CPCCardCategories.IronArmorCategory);
                ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Add(CPCCardCategories.DiamondArmorCategory);
                ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Add(CPCCardCategories.NetheriteArmorCategory);
                ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Remove(CPCCardCategories.PotionCategory);
            }
            yield break;

            }
        
        internal static AssetBundle ArtAssets;
    }
    static class CPCCardCategories
    {
        public static CardCategory StoneSwordCategory = CustomCardCategories.instance.CardCategory("StoneSword");
        public static CardCategory IronSwordCategory = CustomCardCategories.instance.CardCategory("IronSword");
        public static CardCategory DiamondSwordCategory = CustomCardCategories.instance.CardCategory("DiamondSword");
        public static CardCategory NetheriteSwordCategory = CustomCardCategories.instance.CardCategory("NetheriteSword");
        public static CardCategory StoneAxeCategory = CustomCardCategories.instance.CardCategory("StoneAxe");
        public static CardCategory IronAxeCategory = CustomCardCategories.instance.CardCategory("IronAxe");
        public static CardCategory DiamondAxeCategory = CustomCardCategories.instance.CardCategory("DiamondAxe");
        public static CardCategory NetheriteAxeCategory = CustomCardCategories.instance.CardCategory("NetheriteAxe");
        public static CardCategory StoneHoeCategory = CustomCardCategories.instance.CardCategory("StoneHoe");
        public static CardCategory IronHoeCategory = CustomCardCategories.instance.CardCategory("IronHoe");
        public static CardCategory DiamondHoeCategory = CustomCardCategories.instance.CardCategory("DiamondHoe");
        public static CardCategory NetheriteHoeCategory = CustomCardCategories.instance.CardCategory("NetheriteHoe");
        public static CardCategory ChainArmorCategory = CustomCardCategories.instance.CardCategory("ChainArmor");
        public static CardCategory IronArmorCategory = CustomCardCategories.instance.CardCategory("IronArmor");
        public static CardCategory DiamondArmorCategory = CustomCardCategories.instance.CardCategory("DiamondArmor");
        public static CardCategory NetheriteArmorCategory = CustomCardCategories.instance.CardCategory("NetheriteArmor");
        public static CardCategory PotionCategory = CustomCardCategories.instance.CardCategory("UltimatePotion");
    }
}
