using System.Collections.Generic;
using UnityEngine;

namespace MGCNTN
{

    public class SkillMenu : MonoBehaviour
    {
        [SerializeField] private GameObject LevelsContainer;

        public Selector mainSelector;
        [SerializeField] private List<Selector> skillSelectors;

        public Selector getSkillSelector() => skillSelectors[mainSelector.SelectedIndex];


    }
}
