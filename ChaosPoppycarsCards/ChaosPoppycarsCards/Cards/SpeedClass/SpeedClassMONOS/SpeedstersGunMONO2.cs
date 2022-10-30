using System;
using ChaosPoppycarsCards.MonoBehaviours;
using ModdingUtils.MonoBehaviours;
using UnityEngine;

namespace ChaosPoppycarsCards.MonoBehaviours
{
    public class SpeedstersGunMono : MonoBehaviour
    {

        public void Update()
        {
            if (Time.time >= this.startTime + this.updateDelay)
            {
                this.ResetTimer();
                if (GetComponent<Player>().GetComponent<SpeedstersGunBUFFMono>() != null)
                {
                    GetComponent<Player>().GetComponent<SpeedstersGunBUFFMono>().Destroy();
                }
                GetComponent<Player>().transform.gameObject.AddComponent<SpeedstersGunBUFFMono>();
            }
        }

        private void ResetTimer()
        {
            this.startTime = Time.time;
        }

        private readonly float updateDelay = 0.1f;

        public float startTime = Time.time;

        public SpeedstersGunBUFFMono prevBuff;

        public float movespeed;

        
    }
}