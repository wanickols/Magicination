using System;

namespace MGCNTN
{
    [Serializable]
    public class Status
    {
        public StatusType type;
        public int duration;
        public int severityLevel;
    }
}
