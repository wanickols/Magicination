using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MGCNTN
{
    public class Selector : MonoBehaviour
    {
        /// Events
        public event Action SelectionChanged;


        [Header("Basic")]
        public SelectorType type;

        /// Private Parameters
        [SerializeField] private bool allowEmpty = false;
        [SerializeField] private float SelectorSpeed = 8f;
        [SerializeField] private Vector3 mountingOffset;
        [SerializeField] private GameObject imageHolder;


        [Header("Grid")]
        public int columnCount = 1;

        [Header("Scrollable")]
        public bool scrollable;
        public ScrollRect scrollRect;

        //Scrollable
        private int half = 0;
        private float itemsPerView = 0;
        private float scrollableCount;
        private float itemHeight;

        //Local
        protected float selectorSpeed = 8f;
        private int _selectedIndex = 0;

        //Components
        protected Animator animator;
        protected RectTransform rectTransform;
        private List<RectTransform> selectableOptions = new List<RectTransform>();

        /// Public Functions
        //Accessors
        public Transform selectedTransform => getChild(SelectedIndex);
        public Transform getChild(int i) => transform.parent.GetChild(i);
        public List<RectTransform> getSelectables() => selectableOptions;

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                if (allowEmpty || selectableOptions[value].gameObject.activeInHierarchy)
                {
                    _selectedIndex = value;
                    SelectionChanged?.Invoke();
                }
            }
        }
        public IReadOnlyList<RectTransform> SelectableOptions => selectableOptions;
        public void setAnimation(bool animate) => animator.enabled = animate;

        /// Unity Functions
        protected virtual void Awake()
        {

            rectTransform = GetComponent<RectTransform>();
            animator = GetComponent<Animator>();



            for (int i = 0; i < transform.parent.childCount; i++)
            {
                Transform child = getChild(i);
                if (child.CompareTag("Selectable"))
                    selectableOptions.Add(child.GetComponent<RectTransform>());
            }

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




        protected virtual void Update()
        {
            if (rectTransform.anchoredPosition != selectableOptions[SelectedIndex].anchoredPosition)
                MoveToSelectedOption();

            if (scrollable)
                UpdateScrollPosition();
        }


        /// Private Functions
        //Movement
        protected virtual void UpdateScrollPosition()
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

        protected virtual void MoveToSelectedOption()
        {
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, selectableOptions[(int)SelectedIndex].anchoredPosition, selectorSpeed);
        }
    }
}