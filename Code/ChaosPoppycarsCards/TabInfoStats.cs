using TabInfo.Utils;
using ChaosPoppycarsCards.Extensions;
using CPC.Extensions;

namespace CPCTabInfoSTATS
{
    public class TabinfoInterface
    {
        public static void Setup()
        {
            var CritStats = TabInfoManager.RegisterCategory("Critical Hit Stats", 8);
            TabInfoManager.RegisterStat(CritStats, "Crit Damage", (p) => p.data.weaponHandler.gun.GetAdditionalData().criticalHitDamage1 > 1f, (p) => string.Format("{0:F0}%", (p.data.weaponHandler.gun.GetAdditionalData().criticalHitDamage1 -1f) * 100));
            TabInfoManager.RegisterStat(CritStats, "Crit Chance", (p) => p.data.weaponHandler.gun.GetAdditionalData().criticalHitChance1 > 0, (p) => string.Format("{0:F0}%", p.data.weaponHandler.gun.GetAdditionalData().criticalHitChance1 * 100));
            TabInfoManager.RegisterStat(CritStats, "Crit Slow", (p) => p.data.weaponHandler.gun.GetAdditionalData().CritSlow > 0, (p) => string.Format("{0:F0}%", p.data.weaponHandler.gun.GetAdditionalData().CritSlow * 100));
            TabInfoManager.RegisterStat(CritStats, "Crit Bounces", (p) => p.data.weaponHandler.gun.GetAdditionalData().criticalHitBounces != 0, (p) => string.Format("{0:F0}", p.data.weaponHandler.gun.GetAdditionalData().criticalHitBounces));
            TabInfoManager.RegisterStat(CritStats, "Damage on Bounce when you Crit", (p) => p.data.weaponHandler.gun.GetAdditionalData().criticalHitDamageOnBounce > 0, (p) => string.Format("{0:F0}%", p.data.weaponHandler.gun.GetAdditionalData().criticalHitDamageOnBounce * 100));
            TabInfoManager.RegisterStat(CritStats, "Guranteed Crits", (p) => p.data.weaponHandler.gun.GetAdditionalData().guranteedCrits == true, (p) => "True");
            TabInfoManager.RegisterStat(CritStats, "Crit Bullet Speed", (p) => p.data.weaponHandler.gun.GetAdditionalData().criticalBulletSpeed > 1f, (p) => string.Format("{0:F0}%", (p.data.weaponHandler.gun.GetAdditionalData().criticalBulletSpeed - 1f) * 100));
            TabInfoManager.RegisterStat(CritStats, "Crit Simulation Speed", (p) => p.data.weaponHandler.gun.GetAdditionalData().criticalSimulationSpeed > 1f, (p) => string.Format("{0:F0}%", (p.data.weaponHandler.gun.GetAdditionalData().criticalSimulationSpeed -1f) * 100));
        }
    }
}