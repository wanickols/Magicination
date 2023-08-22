using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class Augmentation
    {
        public string displayName;
        public string description;
        public bool costMP = false;
        public int cost; //for mp
        public float duration;
        public PartyMember target;
        public Dictionary<AugType, int> values;


        public Augmentation(string displayName, string description, int cost, float duration, PartyMember target, Dictionary<AugType, int> values)
        {
            this.displayName = displayName;
            this.description = description;
            this.cost = cost;
            this.duration = duration;
            this.target = target;
            this.values = values;
        }

        public void ApplyEffect()
        {
            if (costMP)
            {
                if (target.Stats.MP < cost)
                    return;
            }

            foreach (KeyValuePair<AugType, int> pair in values)
            {
                augment(pair.Key, pair.Value);
            }

            if (duration > 0)
            {
                Game.manager.StartCoroutine(CO_RemoveAfterDuration());
            }
        }


        public void RemoveEffect()
        {
            foreach (KeyValuePair<AugType, int> pair in values)
            {
                augment(pair.Key, -pair.Value);
            }
        }

        private IEnumerator CO_RemoveAfterDuration()
        {
            yield return new WaitForSeconds(duration);

            RemoveEffect();
        }

        private void augment(AugType type, int value)
        {
            switch (type)
            {
                case AugType.LVL:
                    augmentLVL(value);
                    break;
                case AugType.HP:
                    augmentHP(value);
                    break;
                case AugType.EXP:
                    augmentEXP(value);
                    break;
                case AugType.MP:
                    augmentMP(value);
                    break;
                case AugType.ATK:
                    augmentATK(value);
                    break;
                case AugType.DEF:
                    augmentDEF(value);
                    break;
                case AugType.MATK:
                    augmentMATK(value);
                    break;
                case AugType.MDEF:
                    augmentMDEF(value);
                    break;
                case AugType.SPD:
                    augmentSPD(value);
                    break;
                case AugType.EVS:
                    augmentEVS(value);
                    break;
                case AugType.ACC:
                    augmentACC(value);
                    break;

            }
        }


        private void augmentLVL(int val) => target.Stats.LV += val;
        private void augmentHP(int val) => target.Stats.HP += val;
        private void augmentEXP(int val) => target.Stats.EXP += val;
        private void augmentMP(int val) => target.Stats.MP += val;
        private void augmentATK(int val) => target.Stats.ATK += val;
        private void augmentDEF(int val) => target.Stats.DEF += val;
        private void augmentMATK(int val) => target.Stats.MATK += val;
        private void augmentMDEF(int val) => target.Stats.MDEF += val;
        private void augmentSPD(int val) => target.Stats.SPD += val;
        private void augmentEVS(int val) => target.Stats.EVS += val;
        private void augmentACC(int val) => Debug.Log("Accuracy Not implemented)");


    }
}
