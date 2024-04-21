using System.Collections.Generic;

namespace MGCNTN
{
    public class StatusCollection
    {
        ///Public Variables
        public List<Status> statusList { get; private set; }

        public bool isPetrified = false;

        ///Public Functions
        public StatusCollection()
        {
            statusList = new List<Status>();
        }

        //Decrements the list of statuses, and removes them if their duration has run out
        public void tickDuration()
        {
            for (int i = statusList.Count - 1; i >= 0; i--)
                if (--statusList[i].duration <= 0)
                    statusList.RemoveAt(i);
        }

        public void Remove(StatusType type)
        {
            if (type == StatusType.Petrified)
                isPetrified = false;

            statusList.RemoveAll(status => status.type == type);
        }

        public void Add(List<Status> _statusList)
        {
            foreach (Status s in _statusList)
                add(s);
        }

        ///Private Functions
        private void add(Status incomingStatus)
        {
            if (incomingStatus.type == StatusType.Petrified)
            {
                isPetrified = true;
                statusList.Clear();
                statusList.Add(incomingStatus);
            }
            int index = statusList.FindIndex(s => s.type == incomingStatus.type);

            if (index == -1) //Status Not Found
            {
                statusList.Add(incomingStatus);
                return;
            }


            //Comparing Status
            Status existingStatus = statusList[index];


            if (existingStatus.severityLevel > incomingStatus.severityLevel) //If existing is higher level
                return;
            else if (existingStatus.severityLevel == incomingStatus.severityLevel) //if they are the same level
            {
                if (existingStatus.duration < incomingStatus.duration) //if existing duration is less than incoming
                    statusList[index].duration = incomingStatus.duration; //refresh duration 

            }
            else //Incoming has higher level
            {
                statusList[index] = incomingStatus; //replace duration outright
            }
        }

        ///Private Functions
    }
}
