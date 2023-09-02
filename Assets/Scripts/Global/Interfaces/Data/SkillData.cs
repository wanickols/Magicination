using System;
using UnityEngine;

namespace MGCNTN
{
    [Serializable]
    public class SkillData : ObjectData
    {
        [Header("Skills")]
        public bool menuVisibility = false;
        public int cost;
        public static int nextID = 0;

        public SkillData()
        {
            nextID++;
            id = nextID;
        }
    }
}