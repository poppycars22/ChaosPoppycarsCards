using ClassesManagerReborn;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using UnboundLib.Cards;
using UnboundLib.GameModes;

namespace ChaosPoppycarsCards.Cards.Minecrafter
{
    class MinecrafterClass : ClassHandler
    {
        internal static string name = "Minecrafter";

        public override IEnumerator Init()
        {
            UnityEngine.Debug.Log("Regestering: " + name);
            while (!(CraftingTable.Card && MCBow.Card && MCShield.Card && WoodenSword.Card && StoneSword.Card && IronSword.Card && GoldSword.Card && DiamondSword.Card && NetheriteSword.Card && WoodenAxe.Card && StoneAxe.Card && IronAxe.Card && GoldAxe.Card && DiamondAxe.Card && NetheriteAxe.Card && AttackSpeed.Card && StoneHoe.Card && IronHoe.Card && GoldHoe.Card && DiamondHoe.Card && NetheriteHoe.Card && Larmor.Card && Carmor.Card && Iarmor.Card && Garmor.Card && Darmor.Card && Narmor.Card && BrewingStand.Card && InvisPotion.Card && JumpPotion.Card && RegenPotion.Card && SpeedPotion.Card && StrengthPotion.Card && TotemOfUndying.Card && UltimatePotion.Card)) yield return null;
            ClassesRegistry.Register(CraftingTable.Card, CardType.Entry);
            ClassesRegistry.Register(MCBow.Card, CardType.Card, CraftingTable.Card);
            ClassesRegistry.Register(MCShield.Card, CardType.Card, CraftingTable.Card);
            ClassesRegistry.Register(WoodenSword.Card, CardType.Card, CraftingTable.Card);
            ClassesRegistry.Register(StoneSword.Card, CardType.Card, CraftingTable.Card);
            ClassesRegistry.Register(IronSword.Card, CardType.Card, CraftingTable.Card);
            ClassesRegistry.Register(GoldSword.Card, CardType.Card, CraftingTable.Card);
            ClassesRegistry.Register(DiamondSword.Card, CardType.Card, CraftingTable.Card);
            ClassesRegistry.Register(NetheriteSword.Card, CardType.Card, CraftingTable.Card);
            ClassesRegistry.Register(WoodenAxe.Card, CardType.Card, CraftingTable.Card);
            ClassesRegistry.Register(StoneAxe.Card, CardType.Card, CraftingTable.Card);
            ClassesRegistry.Register(IronAxe.Card, CardType.Card, CraftingTable.Card);
            ClassesRegistry.Register(GoldAxe.Card, CardType.Card, CraftingTable.Card);
            ClassesRegistry.Register(DiamondAxe.Card, CardType.Card, CraftingTable.Card);
            ClassesRegistry.Register(NetheriteAxe.Card, CardType.Card, CraftingTable.Card);
            ClassesRegistry.Register(AttackSpeed.Card, CardType.Card, CraftingTable.Card);
            ClassesRegistry.Register(StoneHoe.Card, CardType.Card, CraftingTable.Card);
            ClassesRegistry.Register(IronHoe.Card, CardType.Card, CraftingTable.Card);
            ClassesRegistry.Register(GoldHoe.Card, CardType.Card, CraftingTable.Card);
            ClassesRegistry.Register(DiamondHoe.Card, CardType.Card, CraftingTable.Card);
            ClassesRegistry.Register(NetheriteHoe.Card, CardType.Card, CraftingTable.Card);
            ClassesRegistry.Register(Larmor.Card, CardType.Card, CraftingTable.Card);
            ClassesRegistry.Register(Carmor.Card, CardType.Card, CraftingTable.Card);
            ClassesRegistry.Register(Iarmor.Card, CardType.Card, CraftingTable.Card);
            ClassesRegistry.Register(Garmor.Card, CardType.Card, CraftingTable.Card);
            ClassesRegistry.Register(Darmor.Card, CardType.Card, CraftingTable.Card);
            ClassesRegistry.Register(Narmor.Card, CardType.Card, CraftingTable.Card);
            ClassesRegistry.Register(TotemOfUndying.Card, CardType.Card, CraftingTable.Card);
            ClassesRegistry.Register(BrewingStand.Card, CardType.SubClass, CraftingTable.Card);
            ClassesRegistry.Register(InvisPotion.Card, CardType.Card, BrewingStand.Card);
            ClassesRegistry.Register(JumpPotion.Card, CardType.Card, BrewingStand.Card);
            ClassesRegistry.Register(RegenPotion.Card, CardType.Card, BrewingStand.Card);
            ClassesRegistry.Register(SpeedPotion.Card, CardType.Card, BrewingStand.Card);
            ClassesRegistry.Register(StrengthPotion.Card, CardType.Card, BrewingStand.Card);
            ClassesRegistry.Register(UltimatePotion.Card, CardType.Card, BrewingStand.Card);
        }
    }
}