using UnityEngine;
using ModdingUtils.MonoBehaviours;
using UnboundLib;
using ChaosPoppycarsCards.Extensions;
using PlayerActionsHelper;
using PlayerActionsHelper.Extensions;
using PlayerTimeScale;

namespace ChaosPoppycarsCards.MonoBehaviours
{
    internal class TimeBombMono : MonoBehaviour
    {
        Player player;
        PlayerTimeScale.PlayerTimeScale timeScale;
        public void Start()
        {
            this.player = this.GetComponentInParent<Player>();
            timeScale = player.ApplyTimeScale(3);
        }
        public void OnDestroy()
        {
            Destroy(timeScale);
        }

    }
}
