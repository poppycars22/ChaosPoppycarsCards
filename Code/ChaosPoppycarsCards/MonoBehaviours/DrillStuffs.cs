using UnityEngine;
using System.Linq;

namespace ChaosPoppycarsCards.MonoBehaviours
{
    internal class SetupDrill : MonoBehaviour
    {
        public float metersToDrill;
        public float speedModFlat2;
        public float speedMod2;
        public void Start()
        {
            if (transform.parent != null)
            {
                ObjectsToSpawn objectsToSpawn = ((GameObject)Resources.Load("0 cards/DrillAmmo")).GetComponent<Gun>().objectsToSpawn[0];
                
                objectsToSpawn.AddToProjectile.GetComponents<Component>().ToList().ForEach(obj =>
                {
                    gameObject.AddComponent(obj.GetType());
                });
                var Drilladjustments = gameObject.GetComponent<RayHitDrill>();
                Drilladjustments.metersOfDrilling = metersToDrill;
                Drilladjustments.speedModFlat = speedModFlat2;
                Drilladjustments.speedMod = speedMod2;
               
            }
        }
    }
}
