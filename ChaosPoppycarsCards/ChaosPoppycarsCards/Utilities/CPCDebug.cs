using System;
using System.Collections.Generic;
using System.Text;

namespace ChaosPoppycarsCards.Utilities
{
    public class CPCDebug
    {
        public static void Log(object message)
        {
           if(Config.isDebugBuild)
            {
                CPCDebug.Log(message);
            }

        }
    }
}