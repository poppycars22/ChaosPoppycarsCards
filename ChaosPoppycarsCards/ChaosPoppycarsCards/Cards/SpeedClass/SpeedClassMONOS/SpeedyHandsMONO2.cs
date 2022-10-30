using System;
using ChaosPoppycarsCards.MonoBehaviours;
using ModdingUtils.MonoBehaviours;
using UnityEngine;

namespace ChaosPoppycarsCards.MonoBehaviours
{
    public class SpeedyHandsMono : MonoBehaviour
    {

        public void Update()
        {
            if (Time.time >= this.startTime + this.updateDelay)
            {
                this.ResetTimer();
                if (GetComponent<Player>().GetComponent<SpeedyHandsBUFFMono>() != null)
                {
                    GetComponent<Player>().GetComponent<SpeedyHandsBUFFMono>().Destroy();
                }
                GetComponent<Player>().transform.gameObject.AddComponent<SpeedyHandsBUFFMono>();
            }
        }

        private void ResetTimer()
        {
            this.startTime = Time.time;
        }

        private readonly float updateDelay = 0.1f;

        public float startTime = Time.time;

        public SpeedyHandsBUFFMono prevBuff;

        public float movespeed;

        
    }
}