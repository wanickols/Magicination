using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class Selector : MonoBehaviour
    {
        private PauseMenu mainMenu;
        private RectTransform rectTransform;
        private List<RectTransform> selectableOptions = new List<RectTransform>();
        [SerializeField] private Vector3 mountingPosition;
        [SerializeField] private GameObject imageHolder;
        private Animator animator;
        public Transform selectedTransform => getChild(SelectedIndex);

        public event Action SelectionChanged;

        private int _selectedIndex = 0;
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



        private void Awake()
        {
            mainMenu = GetComponentInParent<PauseMenu>();
            rectTransform = GetComponent<RectTransform>();
            animator = GetComponent<Animator>();

            for (int i = 0; i < rectTransform.parent.childCount; i++)
            {
                if (rectTransform.parent.GetChild(i).CompareTag("Selectable"))
                    selectableOptions.Add(rectTransform.parent.GetChild(i).GetComponent<RectTransform>());
            }

            if (selectableOptions.Count > 0)
                rectTransform.sizeDelta = new Vector2(selectableOptions[0].sizeDelta.x, rectTransform.sizeDelta.y + mountingPosition.y);

            imageHolder.transform.position += mountingPosition;
        }


        void Update()
        {
            if (mainMenu.CurrentSelector != this)
                return;

            if (rectTransform.anchoredPosition != selectableOptions[(int)SelectedIndex].anchoredPosition)
                MoveToSelectedOption();
        }

        private void MoveToSelectedOption()
        {
            rectTransform.anchoredPosition = Vector2.MoveTowards(rectTransform.anchoredPosition, selectableOptions[(int)SelectedIndex].anchoredPosition, 8f);


        }

        public void setAnimation(bool animate) => animator.enabled = animate;

    }
}