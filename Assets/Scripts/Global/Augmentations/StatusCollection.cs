using System.Collections.Generic;

namespace MGCNTN
{
    public class StatusCollection
    {
        ///Public Variables
        public List<Status> statusList { get; private set; } = new List<Status>();
        public Dictionary<StatusType, Status> statusDictionary { get; private set; } = new Dictionary<StatusType, Status>();


        public bool isPetrified => statusDictionary.ContainsKey(StatusType.Petrified);

        ///Public Functions


        //Decrements the list of statuses, and removes them if their duration has run out
        public void tickDuration()
        {
            for (int i = statusList.Count - 1; i >= 0; i--)
                if (--statusList[i].duration <= 0)
                {
                    statusDictionary.Remove(statusList[i].type);
                    statusList.RemoveAt(i);
                }

        }

        public void Remove(StatusType type)
        {
            statusDictionary.Remove(type);
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
            //Using out here will serve reference not copy
            if (statusDictionary.TryGetValue(incomingStatus.type, out Status existingStatus)) //Existing Status
            {

                if (existingStatus.severityLevel > incomingStatus.severityLevel) //If existing is higher level
                    return;

                else if (existingStatus.severityLevel == incomingStatus.severityLevel) //if they are the same level
                {
                    if (existingStatus.duration < incomingStatus.duration) //if existing duration is less than incoming
                    {
                        existingStatus.duration = incomingStatus.duration;
                    }

                }

                else //Incoming has higher level
                    existingStatus.severityLevel = incomingStatus.severityLevel; //replace duration outright

            }
            else //New Status
            {

                if (incomingStatus.type == StatusType.Petrified)
                    clear();

                statusList.Add(incomingStatus);
                statusDictionary.Add(incomingStatus.type, incomingStatus);
            }
        }

        public void clear()
        {
            statusList.Clear();
            statusDictionary.Clear();
        }
    }
}
