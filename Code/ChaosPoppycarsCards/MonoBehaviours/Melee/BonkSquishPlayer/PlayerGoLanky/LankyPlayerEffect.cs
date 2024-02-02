using ModdingUtils.RoundsEffects;
using UnityEngine;

namespace DoggoBonk.MonoBehaviors.Squish.Lanky
{
    
    public class LankyPlayerEffect : MonoBehaviour
    {   
        private float startTime = -1f;
        
        internal float ratio = 1f;

        public bool isMult;

        public float ratioOffset;
        
        public float yOffset;

        internal Vector3 restore_scale = Vector3.zero;
        private void Start()
        {
            if (isMult) ratio *= ratioOffset;
            else ratio += ratioOffset;


            this.restore_scale = base.gameObject.transform.localScale;
            foreach (object obj in base.gameObject.transform.parent.GetChild(4))
                ((Transform)obj).localPosition += new Vector3(0f, this.yOffset, 0f);
            this.ResetScale();
            this.Lanken();
            this.ResetTimer();
        }
        private void Update()
        {
            if (Time.time >= this.startTime + 0.5f)
            {
                this.ResetTimer();
                if (base.gameObject.transform.localScale.x == base.gameObject.transform.localScale.y)
                {
                    this.restore_scale = base.gameObject.transform.localScale;
                    this.Lanken();
                }
            }
        }

        internal void Lanken()
        {
            if (Mathf.Abs(base.gameObject.transform.localScale.x / base.gameObject.transform.localScale.y - this.ratio) >= 0.0001f)
            {
                base.gameObject.transform.localScale = 1.25f * new Vector3(base.gameObject.transform.localScale.x * this.ratio,
                    base.gameObject.transform.localScale.x, base.gameObject.transform.localScale.z);
                base.gameObject.transform.localPosition = new Vector3(0f, this.yOffset, 0f);
            }
        }

        internal void ResetScale()
        {
            if (this.restore_scale != Vector3.zero)
                base.gameObject.transform.localScale = this.restore_scale;
        }

        private void ResetTimer() => this.startTime = Time.time;

        private void OnDestroy()
        {
            this.ResetScale();
            base.gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
            foreach (object obj in base.gameObject.transform.parent.GetChild(4))
            {
                ((Transform)obj).localPosition -= new Vector3(0f, this.yOffset, 0f);
            }
        }
    }
}
