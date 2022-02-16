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
        public const string Version = "0.1.3"; // What version are we on (major.minor.patch)?
        public const string ModInitials = "CPC";
        public static ChaosPoppycarsCards Instance { get; private set; }

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
        }
    }
}
