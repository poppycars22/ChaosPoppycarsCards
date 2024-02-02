using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace DoggoBonk.MonoBehaviors.Squish
{
    public abstract class OnFlipEffect : MonoBehaviour
    {
        public abstract void Selected();

        public abstract void Unselected();
    }
}