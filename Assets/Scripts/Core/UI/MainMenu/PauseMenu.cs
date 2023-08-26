using System;
using UnityEngine;

namespace MGCNTN.Core
{


    public class PauseMenu : MonoBehaviour
    {
        /// Events
        public event Action openMenu;
        public event Action closeMenu;

        ///Public Parameters
        public Selector mainSelector;
        /// Private Variables
        //Serialized Objects
        [Header("Prefabs")]
        [SerializeField] private GameObject MainWindowPrefab;

        [Header("Managers")]
        [SerializeField] private MenuInputHandler inputHandler = new MenuInputHandler();

        //Components
        private MainWindow mainWindow;
        private PauseMenuSelection selectorManager;

        //Animations
        private Animator animator;
        private string menuOpenAnimation = "MenuOpen";
        private string menuCloseAnimation = "MenuClose";

        //Accessor
        private bool IsAnimating => animator.IsAnimating();

        /// Unity Functions
        private void Awake()
        {
            //Systems
            mainWindow = Instantiate(MainWindowPrefab, transform).GetComponentInChildren<MainWindow>();
            selectorManager = new PauseMenuSelection(mainWindow, this);
            inputHandler.Init(selectorManager);

            //Components
            animator = GetComponent<Animator>();
        }
        private void Update()
        {
            if (Game.manager.State != GameState.Menu || IsAnimating)
                return;

            // Call the input function
            inputHandler.HandleInput();
        }

        ///Public Functions
        public void Open()
        {
            inputHandler.resetLastPressedTime();
            mainSelector.SelectedIndex = 0;
            animator.Play(menuOpenAnimation);
            openMenu?.Invoke();
        }
        public void Close()
        {
            animator.Play(menuCloseAnimation);
            closeMenu?.Invoke();
        }
    }
}