using System;
using UnityEngine;

namespace Core
{


    public class PauseMenu : MonoBehaviour
    {
        /// Events
        public event Action openMenu;
        public event Action closeMenu;


        public Selector mainSelector;
        /// Private Variables
        //Serialized Objects
        [Header("Prefabs")]
        [SerializeField] private GameObject MainWindowPrefab;

        [Header("Managers")]
        [SerializeField] private MenuInputHandler inputHandler = new MenuInputHandler();


        //Components
        private MainWindow mainWindow;
        private SelectorManager selectorManager;

        //Animations
        private Animator animator;
        private string menuOpenAnimation = "MenuOpen";
        private string menuCloseAnimation = "MenuClose";

        /// Public Parameters



        //Menu Opened
        public bool isOpen { get; private set; }

        //Accessors
        private bool IsAnimating => animator.IsAnimating();
        public Selector CurrentSelector => selectorManager.CurrentSelector;



        /// Unity Functions
        private void Awake()
        {
            mainWindow = Instantiate(MainWindowPrefab, transform).GetComponentInChildren<MainWindow>();
            animator = GetComponent<Animator>();

            selectorManager = new SelectorManager(mainWindow, this);
            inputHandler.Init(selectorManager);


        }
        private void Update()
        {
            if (Game.manager.State != GameState.Menu || IsAnimating)
                return;

            // Call the input function
            inputHandler.HandleInput();
        }


        public void Open()
        {
            inputHandler.resetLastPressedTime();
            mainSelector.SelectedIndex = 0;
            selectorManager.SetMenuState(MenuState.Main, true); //cancel lets it animate
            isOpen = true;
            animator.Play(menuOpenAnimation);
            openMenu?.Invoke();
        }


        /// Private Functions
        public void Close()
        {
            isOpen = false;
            animator.Play(menuCloseAnimation);
            closeMenu?.Invoke();
            mainSelector.setAnimation(false);
        }




    }
}