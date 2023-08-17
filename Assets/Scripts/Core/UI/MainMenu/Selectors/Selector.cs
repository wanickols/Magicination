using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class Selector : MonoBehaviour
    {
        /// Events
        public event Action SelectionChanged;

        public SelectorType type;

        //Grid
        public int columnCount = 1;

        //Scroll
        public bool scrollable;
        public ScrollRect scrollRect;
        public int scrollMovementTrigger;

        /// Private Parameters
        [SerializeField] private Vector3 mountingPosition;
        [SerializeField] private GameObject imageHolder;


        //Input
        protected PauseMenu pauseMenu;

        //Components
        private Animator animator;
        private RectTransform rectTransform;
        private List<RectTransform> selectableOptions = new List<RectTransform>();

        //Variables
        private int _selectedIndex = 0;
        /// Public Paremeters
        //Acceessors
        public Transform selectedTransform => getChild(SelectedIndex);

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                SelectionChanged?.Invoke();
            }
        }
        public IReadOnlyList<RectTransform> SelectableOptions => selectableOptions;
        public Transform getChild(int i) => transform.parent.GetChild(i);


        /// Unity Functions

        protected virtual void Awake()
        {
            pauseMenu = GetComponentInParent<PauseMenu>();
            rectTransform = GetComponent<RectTransform>();
            animator = GetComponent<Animator>();

            for (int i = 0; i < transform.parent.childCount; i++)
                if (getChild(i).CompareTag("Selectable"))
                    selectableOptions.Add(getChild(i).GetComponent<RectTransform>());


            if (selectableOptions.Count > 0)
                rectTransform.sizeDelta = new Vector2(selectableOptions[0].sizeDelta.x, rectTransform.sizeDelta.y + mountingPosition.y);

            imageHolder.transform.position += mountingPosition;
        }


        void Update()
        {
            if (rectTransform.anchoredPosition != selectableOptions[SelectedIndex].anchoredPosition)
                MoveToSelectedOption();
        }

        /// Private Functions
        //Movement
        private void MoveToSelectedOption()
        {
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, selectableOptions[(int)SelectedIndex].anchoredPosition, 8f);


        }
        public void setAnimation(bool animate) => animator.enabled = animate;

    }
}