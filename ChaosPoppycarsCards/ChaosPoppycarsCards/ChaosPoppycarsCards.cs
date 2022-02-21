using BepInEx;
using UnboundLib;
using UnboundLib.Cards;
using ChaosPoppycarsCards.Cards;
using HarmonyLib;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using UnityEngine;

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
        public const string Version = "0.2.0"; // What version are we on (major.minor.patch)?
        public const string ModInitials = "CPC";
        public static ChaosPoppycarsCards Instance { get; private set; }
        private static readonly AssetBundle Bundle = Jotunn.Utils.AssetUtils.LoadAssetBundleFromResources("cpcart", typeof(ChaosPoppycarsCards).Assembly);

        public static GameObject WoodenSwordArt = Bundle.LoadAsset<GameObject>("C_WoodenSword");
        public static GameObject StoneSwordArt = Bundle.LoadAsset<GameObject>("C_StoneSword");
        public static GameObject GoldSwordArt = Bundle.LoadAsset<GameObject>("C_GoldSword");
        public static GameObject IronSwordArt = Bundle.LoadAsset<GameObject>("C_IronSword");
        public static GameObject DiamondSwordArt = Bundle.LoadAsset<GameObject>("C_DiamondSword");
        public static GameObject NetheriteSwordArt = Bundle.LoadAsset<GameObject>("C_NetheriteSword");
        public static GameObject LarmorArt = Bundle.LoadAsset<GameObject>("C_LeatherArmor");
        public static GameObject CarmorArt = Bundle.LoadAsset<GameObject>("C_ChainmailArmor");
        public static GameObject GarmorArt = Bundle.LoadAsset<GameObject>("C_GoldArmor");
        public static GameObject IarmorArt = Bundle.LoadAsset<GameObject>("C_IronArmor");
        public static GameObject DarmorArt = Bundle.LoadAsset<GameObject>("C_DiamondArmor");
        public static GameObject NarmorArt = Bundle.LoadAsset<GameObject>("C_NetheriteArmor");
        public static GameObject RegenPotionArt = Bundle.LoadAsset<GameObject>("C_RegenrationPotion");
        public static GameObject SpeedPotionArt = Bundle.LoadAsset<GameObject>("C_SpeedPotion");
        public static GameObject JumpPotionArt = Bundle.LoadAsset<GameObject>("C_JumpPotion");
        public static GameObject MinecraftBowArt = Bundle.LoadAsset<GameObject>("C_MinecraftBow");
        public static GameObject StrengthPotionArt = Bundle.LoadAsset<GameObject>("C_StrengthPotion");
        void Awake()
        {
            // Use this to call any harmony patch files your mod may have
            var harmony = new Harmony(ModId);
            harmony.PatchAll();
        }
        void Start()

        {
          Instance = this;
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
            CustomCard.BuildCard<PoppysChaos>();
            CustomCard.BuildCard<ActivatedDuplicator>();
            CustomCard.BuildCard<UltimatePotion>();
        }
    }
}
