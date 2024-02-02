using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ChaosPoppycarsCards.Lightsaber.Extensions;

namespace ChaosPoppycarsCards.Lightsaber
{
    /*class ObjectMirror : MonoBehaviour
    {
        Holdable holdable;
        static readonly float xPos = 0.1f;
        static readonly float yPos = 1.15f;
        static readonly float scale = 1.15f;
        private static readonly Vector3 LeftPos = new Vector3(-xPos, yPos, 0f);
        private static readonly Vector3 RightPos = new Vector3(xPos, yPos, 0f);
        private static readonly Vector3 LeftScale = new Vector3(-scale, scale, 1f);
        private static readonly Vector3 RightScale = new Vector3(-scale, -scale, 1f);
        private const float LeftRot = 300f;
        private const float RightRot = 235f;
        private bool Spinning = false;
        private float SpinTimer = 0f;
        void OnEnable()
        {
            this.holdable = base.transform.root.GetComponent<Holdable>();

            // do a quick 360 when first pulled out
            this.Spinning = true;
            this.SpinTimer = A_HoldableObject.SwitchDelay;
        }
        void Update()
        {
            if (this.holdable is null || this.holdable.holder is null) { return; }

            bool left = this.transform.root.position.x - 0.1f < this.holdable.holder.transform.position.x;
            this.transform.localScale = left ? LeftScale : RightScale;
            float rot = left ? LeftRot : RightRot;
            if (this.Spinning)
            {
                this.SpinTimer -= TimeHandler.deltaTime;
                rot = Mathf.Lerp(rot, rot + 360f, this.SpinTimer / A_HoldableObject.SwitchDelay);
            }
            
            this.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, rot));
            this.transform.localPosition = left ? LeftPos : RightPos;
        }
    }*/
    class ObjectMirror : MonoBehaviour
    {
        Holdable holdable;
        private static readonly Vector3 LeftPos = new Vector3(0.1f, 1.95f, 0f);
        private static readonly Vector3 RightPos = new Vector3(0.4f, 1.95f, 0f);
        private static readonly Vector3 LeftScale = new Vector3(-1f, 1f, 1f);
        private static readonly Vector3 RightScale = new Vector3(-1f, -1f, 1f);
        private const float LeftRot = 300f;
        private const float RightRot = 225f;
        internal float rotMod = 0f;
        internal Vector3 positionMod = Vector3.zero;
        void OnEnable()
        {
            this.holdable = base.transform.root.GetComponent<Holdable>();
        }
        void Update()
        {
            if (this.holdable is null || this.holdable?.holder is null) { return; }
            bool left = this.transform.root.position.x - 0.1f < this.holdable.holder.transform.position.x;
            this.transform.localScale = (left ? LeftScale : RightScale);
            float rot = (left ? LeftRot : RightRot) + (rotMod * (left ? -1f : 1f));
            this.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, rot));
            Vector3 pos = (left ? LeftPos : RightPos) + (new Vector3(positionMod.x * (left ? -1f : 1f), positionMod.y, positionMod.z));
            this.transform.localPosition = pos;
        }
    }
}
