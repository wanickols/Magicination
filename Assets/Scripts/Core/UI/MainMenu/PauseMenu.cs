using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{


    public class PauseMenu : MonoBehaviour
    {
        /// Events
        public event Action openMenu;
        public event Action closeMenu;

        /// Private Variables
        //Serialized Objects
        [SerializeField] private Selector mainSelector;
        [SerializeField] private Selector memberSelector;
        [SerializeField] private Selector equipmentSelector, equippableSelector;
        public AudioSource menuChangeSound;

        //States
        private Dictionary<MenuState, Selector> stateSelector = new Dictionary<MenuState, Selector>();
        private MenuState menuState;

        //Components
        private MainWindow mainWindow;

        //Animations
        private Animator animator;
        private string menuOpenAnimation = "MenuOpen";
        private string menuCloseAnimation = "MenuClose";

        /// Public Parameters
        //Input
        public float pressThreshold = .005f; // The minimum time between key presses private float
        public float lastPressTime = 0f; // The time of the last key press

        //Menu Opened
        public bool isOpen { get; private set; }

        //Accessors
        private bool IsAnimating => animator.IsAnimating();
        public Selector CurrentSelector => stateSelector.ContainsKey(menuState) ? stateSelector[menuState] : null;

        public void addItemSelector(Selector itemSelector) => stateSelector.Add(MenuState.ItemSelection, itemSelector);
        public void removeItemSelector() => stateSelector.Remove(MenuState.ItemSelection);

        /// Unity Functions
        private void Awake()
        {
            mainWindow = GetComponentInChildren<MainWindow>();
            animator = GetComponent<Animator>();

            //Connecting selectors to states
            stateSelector.Add(MenuState.Main, mainSelector);
            stateSelector.Add(MenuState.MemberSelection, memberSelector);
            stateSelector.Add(MenuState.EquipmentSelection, equipmentSelector);
            stateSelector.Add(MenuState.EquippableSelection, equippableSelector);

        }
        private void Update()
        {
            if (Game.manager.State != GameState.Menu || IsAnimating)
                return;

            // Call the input function
            CurrentSelector.HandleInput();
        }


        /// Public Functions
        public void checkHover()
        {
            switch (menuState)
            {
                case MenuState.EquipmentSelection:
                case MenuState.EquippableSelection:
                    mainWindow.onHover(menuState, CurrentSelector);
                    break;
                default:
                    break;
            }

        }
        public void Open()
        {
            lastPressTime = Time.time;
            mainSelector.SelectedIndex = 0;
            SetMenuState(MenuState.Main, true); //cancel lets it animate
            isOpen = true;
            animator.Play(menuOpenAnimation);
            openMenu?.Invoke();
        }
        public void Cancel()
        {
            switch (menuState)
            {
                case (MenuState.Main):
                    Close();
                    break;
                case (MenuState.MemberSelection):
                    returnToMain();
                    break;
                case (MenuState.ItemSelection):
                    returnToMain();
                    mainWindow.closeItemView(this);
                    mainWindow.ShowDefaultView();
                    break;
                case (MenuState.EquipmentSelection):
                    returnToMembers();
                    break;
                case (MenuState.EquippableSelection):
                    mainWindow.hideEquippableSelection(CurrentSelector);
                    SetMenuState(MenuState.EquipmentSelection, true);
                    break;

            }
        }
        public void Accept()
        {
            switch (menuState)
            {
                case (MenuState.Main):
                    ProcessMainSelection();
                    break;
                case (MenuState.MemberSelection):
                    ProcessMemberSelection();
                    break;
                case (MenuState.EquipmentSelection):
                    int selected = CurrentSelector.SelectedIndex;
                    SetMenuState(MenuState.EquippableSelection, false);
                    mainWindow.ShowArsenalView(CurrentSelector, selected);
                    break;
                case (MenuState.EquippableSelection):
                    mainWindow.swapEquippable(CurrentSelector);
                    Cancel();
                    break;
                case (MenuState.ItemSelection):
                    Debug.Log("Show Item Page");
                    break;

            }
        }

        /// Private Functions
        private void Close()
        {
            isOpen = false;
            animator.Play(menuCloseAnimation);
            closeMenu?.Invoke();
            mainSelector.setAnimation(false);
        }
        private void returnToMain()
        {
            SetMenuState(MenuState.Main, true);
            CurrentSelector.setAnimation(true);
        }
        private void returnToMembers()
        {
            mainWindow.ShowDefaultView();
            SetMenuState(MenuState.MemberSelection, true);
            CurrentSelector.setAnimation(true);
        }
        private void ProcessMainSelection()
        {
            switch ((mainSelections)mainSelector.SelectedIndex)
            {
                case mainSelections.Items:
                    mainWindow.ShowItemView(this);
                    SetMenuState(MenuState.ItemSelection, false);
                    break;
                case mainSelections.Skills:
                case mainSelections.Equip:
                case mainSelections.Status:
                case mainSelections.Order:
                    SetMenuState(MenuState.MemberSelection, false);
                    break;
                case mainSelections.Save:
                case mainSelections.Quit:
                default:
                    Debug.LogWarning("Not implemented!");
                    break;
            }
        }
        private void ProcessMemberSelection()
        {
            switch ((mainSelections)mainSelector.SelectedIndex)
            {
                case mainSelections.Skills:
                    Debug.LogWarning("Not implemented!");
                    break;
                case mainSelections.Equip:
                    mainWindow.ShowEquipmentView(CurrentSelector.SelectedIndex);
                    SetMenuState(MenuState.EquipmentSelection, false);
                    break;
                case mainSelections.Status:
                case mainSelections.Order:
                default:
                    Debug.LogWarning("Not implemented!");
                    break;
            }
        }
        private void SetMenuState(MenuState newState, bool cancel)
        {
            if (!cancel)
                CurrentSelector.setAnimation(false);


            CurrentSelector.gameObject.SetActive(!cancel);

            if (stateSelector.ContainsKey(newState))
            {
                menuState = newState;

                if (!cancel)
                    CurrentSelector.SelectedIndex = 0;

                CurrentSelector.gameObject.SetActive(true);

            }


        }


    }
}