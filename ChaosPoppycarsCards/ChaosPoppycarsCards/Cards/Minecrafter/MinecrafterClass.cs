﻿using ClassesManagerReborn;
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
            ClassesRegistry.Register(MCShield.Card, CardType.Card, CraftingTable.Card);
            ClassesRegistry.Register(WoodenSword.Card, CardType.Gate, CraftingTable.Card);
                ClassesRegistry.Register(StoneSword.Card, CardType.Gate, WoodenSword.Card);
                    ClassesRegistry.Register(IronSword.Card, CardType.Gate, StoneSword.Card);
            ClassesRegistry.Register(GoldSword.Card, CardType.Card, CraftingTable.Card);
                        ClassesRegistry.Register(DiamondSword.Card, CardType.Gate, IronSword.Card);
                            ClassesRegistry.Register(NetheriteSword.Card, CardType.Card, DiamondSword.Card);
            ClassesRegistry.Register(WoodenAxe.Card, CardType.Gate, CraftingTable.Card);
                ClassesRegistry.Register(StoneAxe.Card, CardType.Gate, WoodenAxe.Card);
                    ClassesRegistry.Register(IronAxe.Card, CardType.Gate, StoneAxe.Card);
            ClassesRegistry.Register(GoldAxe.Card, CardType.Card, CraftingTable.Card);
                        ClassesRegistry.Register(DiamondAxe.Card, CardType.Gate, IronAxe.Card);
                            ClassesRegistry.Register(NetheriteAxe.Card, CardType.Card, DiamondAxe.Card);
            ClassesRegistry.Register(AttackSpeed.Card, CardType.Gate, CraftingTable.Card);
                ClassesRegistry.Register(StoneHoe.Card, CardType.Gate, AttackSpeed.Card);
                    ClassesRegistry.Register(IronHoe.Card, CardType.Gate, StoneHoe.Card);
            ClassesRegistry.Register(GoldHoe.Card, CardType.Card, CraftingTable.Card);
                        ClassesRegistry.Register(DiamondHoe.Card, CardType.Gate, IronHoe.Card);
                            ClassesRegistry.Register(NetheriteHoe.Card, CardType.Card, DiamondHoe.Card);
            ClassesRegistry.Register(Larmor.Card, CardType.Gate, CraftingTable.Card);
                ClassesRegistry.Register(Carmor.Card, CardType.Gate, Larmor.Card);
                    ClassesRegistry.Register(Iarmor.Card, CardType.Gate, Carmor.Card);
            ClassesRegistry.Register(Garmor.Card, CardType.Card, CraftingTable.Card);
                        ClassesRegistry.Register(Darmor.Card, CardType.Gate, Iarmor.Card);
                            ClassesRegistry.Register(Narmor.Card, CardType.Card, Darmor.Card);
            ClassesRegistry.Register(TotemOfUndying.Card, CardType.Card, CraftingTable.Card);
            ClassesRegistry.Register(BrewingStand.Card, CardType.SubClass, CraftingTable.Card);
                ClassesRegistry.Register(InvisPotion.Card, CardType.Card, BrewingStand.Card);
                ClassesRegistry.Register(JumpPotion.Card, CardType.Card, BrewingStand.Card);
                ClassesRegistry.Register(RegenPotion.Card, CardType.Card, BrewingStand.Card);
                ClassesRegistry.Register(SpeedPotion.Card, CardType.Card, BrewingStand.Card);
                ClassesRegistry.Register(StrengthPotion.Card, CardType.Card, BrewingStand.Card);
                ClassesRegistry.Register(UltimatePotion.Card, CardType.Card, BrewingStand.Card);
            ClassesRegistry.Register(MCBow.Card, CardType.Card, CraftingTable.Card);
                //posion tipped arrows (posion effect, +dmg, another projectile)
                //fire tipped arrows (CR flame effect, another projectile)
                //Instant damage arrows (essentialy chompy bullets)
                //Speed boost tipped arrows (when you hit a oppenent they become much faster, +dmg, +another projectile)
                //Crossbow (much faster bullets, slower attack cooldown)
                //Jump boost tipped arrows (when you hit a oppenent they gain a lot of jump height)
            //Modded subclass
                //MOST OF THESE WILL BE RARES OR HIGHER
                //
            //Redstoner subclass
                //
            //Builder subclass
                //Dirt, build a box that is easily broken
                //Stone, build a box that is slightly harder to break
                //Deepslate
                //Obsidian
                //(tier system)
            //Farmer
        }
    }
}