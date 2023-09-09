using System;
using System.Collections.Generic;
using UnityEngine;

namespace MGCNTN.Core
{
    public class SkillManager : ScriptableObject
    {

        private Dictionary<Tuple<Skill, Skill>, Skill> combinationDictionary;

        private void Awake()
        {
            combinationDictionary = new Dictionary<Tuple<Skill, Skill>, Skill>();


        }

        public Skill FindCombination(Skill skill1, Skill skill2)
        {
            Tuple<Skill, Skill> key = new Tuple<Skill, Skill>(skill1, skill2);

            if (combinationDictionary.TryGetValue(key, out Skill result))
                return result;


            // Combination not found
            return null;
        }

        private void AddCombination(Skill skill1, Skill skill2, Skill resultSkill)
        {
            Tuple<Skill, Skill> key = new Tuple<Skill, Skill>(skill1, skill2);
            combinationDictionary[key] = resultSkill;
        }

    }
}