﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ChaosPoppycarsCards.Utilities
{
    internal class CPCDebug
    {
        public static void Log(object message)
        {
           if(Config.isDebugBuild)
            {
                UnityEngine.Debug.Log(message);
            }

        }
    }
}