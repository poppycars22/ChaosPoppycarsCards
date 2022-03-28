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
        public const string Version = "0.3.5"; // What version are we on (major.minor.patch)?
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
        public static GameObject ChaosArt = Bundle.LoadAsset<GameObject>("C_PoppysChaos");
        public static GameObject UPotionArt = Bundle.LoadAsset<GameObject>("C_UltimatePotion");
        public static GameObject WoodenAxeArt = Bundle.LoadAsset<GameObject>("C_WoodenAxe");
        public static GameObject StoneAxeArt = Bundle.LoadAsset<GameObject>("C_StoneAxe");
        public static GameObject IronAxeArt = Bundle.LoadAsset<GameObject>("C_IronAxe");
        public static GameObject GoldAxeArt = Bundle.LoadAsset<GameObject>("C_GoldAxe");
        public static GameObject DiamondAxeArt = Bundle.LoadAsset<GameObject>("C_DiamondAxe");
        public static GameObject NetheriteAxeArt = Bundle.LoadAsset<GameObject>("C_NetheriteAxe");
        public static GameObject GetAwayArt = Bundle.LoadAsset<GameObject>("C_GETAWAY");
        public static GameObject WoodenHoeArt = Bundle.LoadAsset<GameObject>("C_WoodenHoe");
        public static GameObject StoneHoeArt = Bundle.LoadAsset<GameObject>("C_StoneHoe");
        public static GameObject GoldHoeArt = Bundle.LoadAsset<GameObject>("C_GoldHoe");
        public static GameObject IronHoeArt = Bundle.LoadAsset<GameObject>("C_IronHoe");
        public static GameObject DiamondHoeArt = Bundle.LoadAsset<GameObject>("C_DiamondHoe");
        public static GameObject NetheriteHoeArt = Bundle.LoadAsset<GameObject>("C_NetheriteHoe");
        public static GameObject TotemArt = Bundle.LoadAsset<GameObject>("C_TotemOfUndying");
        public static GameObject AmmoChestArt = Bundle.LoadAsset<GameObject>("C_AmmoChest");
        public static GameObject AduplicatorArt = Bundle.LoadAsset<GameObject>("C_ActivatedDuplicator");
        public static GameObject duplicatorArt = Bundle.LoadAsset<GameObject>("C_Duplicator");
        public static GameObject DduplicatorArt = Bundle.LoadAsset<GameObject>("C_DoubleDuplicator");
        public static GameObject BouncyGelArt = Bundle.LoadAsset<GameObject>("C_BouncyGel");
        public static GameObject NegativeOneArt = Bundle.LoadAsset<GameObject>("C_Negafy");
        public static GameObject SugaredArt = Bundle.LoadAsset<GameObject>("C_Sugared");
        void Awake()
        {
            // Use this to call any harmony patch files your mod may have
            var harmony = new Harmony(ModId);
            harmony.PatchAll();
        }
        void Start()

        {
            Instance = this;
            //   GameModeManager.AddHook(GameModeHooks.HookGameStart, this.GameStart);
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
          //  CustomCard.BuildCard<PoppysChaos>();
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
            //  CustomCard.BuildCard<SpeedBounce>();

        }
        //  IEnumerator GameStart(IGameModeHandler gm)
        //   {
        // Runs at start of match
        //        foreach (var player in PlayerManager.instance.players)
        //        {
        //            ModdingUtils.Extensions.CharacterStatModifiersExtension.GetAdditionalData(player.data.stats).blacklistedCategories.Add(CPCCardCategories.StoneSwordCategory);
        //            yield break;
        //        }
        //     }
        //  }
        // static class CPCCardCategories
        // {
        //    public static CardCategory StoneSwordCategory = CustomCardCategories.instance.CardCategory("Bounce Absorption");
        //     public static CardCategory RepentanceCategory = CustomCardCategories.instance.CardCategory("Repentance");
        //  }
    }
}
