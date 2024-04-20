using System;
using UnityEngine;

namespace MGCNTN
{
    [Serializable]
    public class Status
    {
        public StatusType type;
        [HideInInspector]
        public UInt16 duration;
        public UInt16 severityLevel;
    }
}
