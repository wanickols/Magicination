using UnityEngine;

namespace MGCNTN
{
    [CreateAssetMenu(fileName = "Skill", menuName = ("Skill"))]
    public class Skill : ScriptableObject
    {
        [SerializeField] private SkillData data = new SkillData();
        public SkillData Data
        {
            get => data;
            set => data = value;
        }

        public void Activate(Stats target)
        {
            if (data.augData == null)
                return;

            data.augData.CreateAugmentation(target).ApplyEffect();
        }

    }
}