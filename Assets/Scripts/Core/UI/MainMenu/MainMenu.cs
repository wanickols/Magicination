using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{


    public class MainMenu : MonoBehaviour
    {


        //Events
        public event Action openMenu;
        public event Action closeMenu;


        [SerializeField] private Selector mainSelector;
        [SerializeField] private Selector memberSelector;
        //[SerializeField] private MenuSelector equipmentSelector;
        [SerializeField] private AudioSource menuChangeSound;

        private Dictionary<MenuState, Selector> stateSelector = new Dictionary<MenuState, Selector>();
        private MainWindow mainWindow;
        private Animator animator;
        private string menuOpenAnimation = "MenuOpen";
        private string menuCloseAnimation = "MenuClose";
        private MenuState menuState;

        private float pressThreshold = .005f; // The minimum time between key presses private float
        private float lastPressTime = 0f; // The time of the last key press

        private enum MenuState
        {
            Main,
            EquipMemberSelection,
            EquipmentSelection,
        }

        public bool isOpen { get; private set; }

        private bool IsAnimating => animator.IsAnimating();
        public Selector CurrentSelector => stateSelector.ContainsKey(menuState) ? stateSelector[menuState] : null;

        private void Awake()
        {
            mainWindow = GetComponentInChildren<MainWindow>();
            animator = GetComponent<Animator>();
            stateSelector.Add(MenuState.Main, mainSelector);
            stateSelector.Add(MenuState.EquipMemberSelection, memberSelector);
            //stateSelector.Add(MenuState.EquipmentSelection, equipmentSelector);

        }

        private void Update()
        {
            if (Game.manager.State != GameState.Menu || IsAnimating)
                return;

            // Call the input function
            HandleInput();
        }

        private void HandleInput()
        {
            // Get the current time
            float currentTime = Time.time;

            // Calculate the time difference between the current and last press
            float timeDifference = currentTime - lastPressTime;

            // Check if the time difference is greater than or equal to the threshold
            if (timeDifference >= pressThreshold)
            {
                // Update the last press time
                lastPressTime = currentTime;

                // Check which key is pressed and handle it accordingly
                if (Input.GetKeyDown(KeyCode.UpArrow) && CurrentSelector.SelectedIndex > 0)
                {
                    menuChangeSound.Play();
                    CurrentSelector.SelectedIndex--;
                }

                else if (Input.GetKeyDown(KeyCode.DownArrow) && CurrentSelector.SelectedIndex != CurrentSelector.SelectableOptions.Count - 1)
                {
                    menuChangeSound.Play();
                    CurrentSelector.SelectedIndex++;
                }

                else if (Input.GetKeyDown(KeyCode.Return))
                    Accept();

                else if (Input.GetKeyDown(KeyCode.Escape))
                    Cancel();
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

        public void Close()
        {
            isOpen = false;
            animator.Play(menuCloseAnimation);
            closeMenu?.Invoke();
            mainSelector.setAnimation(false);
        }

        public void Cancel()
        {
            switch (menuState)
            {
                case (MenuState.Main):
                    Close();
                    break;
                case (MenuState.EquipMemberSelection):
                    SetMenuState(MenuState.Main, true);
                    CurrentSelector.setAnimation(true);
                    break;
                case (MenuState.EquipmentSelection):
                    mainWindow.ShowDefaultView();
                    SetMenuState(MenuState.EquipMemberSelection, true);
                    CurrentSelector.setAnimation(true);
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
                case (MenuState.EquipMemberSelection):

                    mainWindow.ShowEquipmentView(CurrentSelector.SelectedIndex);
                    SetMenuState(MenuState.EquipmentSelection, false);
                    break;

            }
        }
        private void ProcessMainSelection()
        {
            switch ((mainSelections)mainSelector.SelectedIndex)
            {
                case mainSelections.Equip:
                    Equip();
                    break;
                default:
                    Debug.LogWarning("Not implemented!");
                    break;
            }
        }

        private void Equip()
        {
            SetMenuState(MenuState.EquipMemberSelection, false);
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