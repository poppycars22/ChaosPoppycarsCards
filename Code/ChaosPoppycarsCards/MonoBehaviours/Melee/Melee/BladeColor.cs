using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib.Extensions;
using UnityEngine;
using UnboundLib.Utils;
using System.ComponentModel.Design.Serialization;
using UnboundLib.Networking;
using UnboundLib;

namespace ChaosPoppycarsCards.Lightsaber
{
    internal class BladeColor : MonoBehaviour
    {
        public static Player player;

        public int playerID;

        static Color color;

        static PlayerSkin skin;

        static SpriteRenderer render;

        void Start()
        {
            player = PlayerManager.instance.players.FirstOrDefault(p => p.playerID == playerID);
            skin = ExtraPlayerSkins.GetPlayerSkinColors(player.colorID());
            color = skin.color;
            render = this.gameObject.GetComponent<SpriteRenderer>();
            Unbound.Instance.ExecuteAfterFrames(10, UpdateBladeColor);
        }
        [UnboundRPC]
        internal static void RPCA_UpdateBladeColor(Color color)
        {
            render.color = color;
        }
        public static void UpdateBladeColor()
        {
            RPCA_UpdateBladeColor(color);
        }
    }
}
