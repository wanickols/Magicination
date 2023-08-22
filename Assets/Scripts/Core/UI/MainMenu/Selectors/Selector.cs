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

        /// Private Parameters
        [SerializeField] private Vector3 mountingOffset;
        [SerializeField] private GameObject imageHolder;

        [SerializeField] private float SelectorSpeed = 8f;


        //Scrollable Stuff
        [Header("Scrollable")]
        public bool scrollable;
        public ScrollRect scrollRect;
        public int scrollMovementTrigger;
        private float itemsPerView = 0;
        int half = 0;
        float scrollableCount;
        private float itemHeight;

        /// Private
        private float selectorSpeed = 8f;

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
                rectTransform.sizeDelta = new Vector2(selectableOptions[0].sizeDelta.x, rectTransform.sizeDelta.y + mountingOffset.y);

            imageHolder.transform.position += mountingOffset;

            itemHeight = selectableOptions[0].rect.size.y;

            if (scrollable)
            {
                itemsPerView = scrollRect.viewport.rect.height / itemHeight;
                half = (int)itemsPerView / 2;
                scrollableCount = selectableOptions.Count - itemsPerView;
            }
        }


        void Update()
        {
            if (rectTransform.anchoredPosition != selectableOptions[SelectedIndex].anchoredPosition)
                MoveToSelectedOption();

            if (scrollable)
                UpdateScrollPosition();
        }


        /// Private Functions
        //Movement
        void UpdateScrollPosition()
        {

            selectorSpeed = SelectorSpeed;
            if (SelectedIndex >= half && SelectedIndex < (selectableOptions.Count - half))
            {
                // Calculate the normalized position of the selector relative to the content size
                float normalizedVerticalPosition = (SelectedIndex - half) / scrollableCount;

                // Set the normalized position of the scroll view
                scrollRect.verticalNormalizedPosition = 1 - normalizedVerticalPosition;
                selectorSpeed = SelectorSpeed * 8;
            }
        }

        private void MoveToSelectedOption()
        {
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, selectableOptions[(int)SelectedIndex].anchoredPosition, selectorSpeed);
        }
        public void setAnimation(bool animate) => animator.enabled = animate;

    }
}