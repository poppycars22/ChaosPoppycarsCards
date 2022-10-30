using System;
using ChaosPoppycarsCards.MonoBehaviours;
using ModdingUtils.MonoBehaviours;
using UnityEngine;

namespace ChaosPoppycarsCards.MonoBehaviours
{
    public class TriggerFingerMono : MonoBehaviour
    {

        public void Update()
        {
            if (Time.time >= this.startTime + this.updateDelay)
            {
                this.ResetTimer();
                if (GetComponent<Player>().GetComponent<TriggerFingerBUFFMono>() != null)
                {
                    GetComponent<Player>().GetComponent<TriggerFingerBUFFMono>().Destroy();
                }
                GetComponent<Player>().transform.gameObject.AddComponent<TriggerFingerBUFFMono>();
            }
        }

        private void ResetTimer()
        {
            this.startTime = Time.time;
        }

        private readonly float updateDelay = 0.1f;

        public float startTime = Time.time;

        public TriggerFingerBUFFMono prevBuff;

        public float movespeed;

        
    }
}