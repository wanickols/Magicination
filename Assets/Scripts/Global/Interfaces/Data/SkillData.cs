using System;
using UnityEngine;

namespace MGCNTN
{

    [Serializable]
    public class SkillData : ObjectData
    {
        [Header("Skills")]
        public int cost;
        public SkillStatus skillStatus = SkillStatus.hidden;
        public Vector2Int menuPos; //for skill tree
        public static int nextID = 0;


        public SkillData()
        {
            nextID++;
            id = nextID;
        }
    }
}
