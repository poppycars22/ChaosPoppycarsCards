using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using ChaosPoppycarsCards.Lightsaber.Extensions;

namespace ChaosPoppycarsCards.Lightsaber
{
    class ObjectCollider : MonoBehaviour
    {
        /// <summary>
        /// TODO
        /// add blade activation build it with modules and network
        /// before you may proggress in this finsish coding assingment
        /// refine art
        /// match blade color with player
        /// 
        /// </summary>

        private ObjectSlash objectStab;
        internal Collider2D collider;
        void Start()
        {
            this.objectStab = this.GetComponentInParent<ObjectSlash>();
            this.collider = this.GetComponent<Collider2D>();
            this.collider.isTrigger = false;
            this.collider.attachedRigidbody.mass = 500;
            // must be on this layer to avoid prop-flying and shadow casting
            this.gameObject.layer = LayerMask.NameToLayer("PlayerObjectCollider");

            foreach (var collider in this.objectStab.player.GetComponentsInChildren<Collider2D>())
                Physics2D.IgnoreCollision(this.collider, collider);
            //Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("PlayerObjectCollider"), LayerMask.NameToLayer("Player"));
            //-removes the option to collide with cards
            //+removes players jumping on the object to fly
        }
        
        void OnTriggerEnter2D(Collider2D collider2D)
        {
            this.objectStab?.TrySlash(collider2D);
        }
        void OnTriggerStay2D(Collider2D collider2D)
        {
            this.objectStab?.TrySlash(collider2D);
        }
        void OnCollisionEnter2D(Collision2D collision)
        {
            this.objectStab?.TrySlash(collision);
        }
        void OnCollisionStay2D(Collision2D collision)
        {
            this.objectStab?.TrySlash(collision);
        }
    }
}
