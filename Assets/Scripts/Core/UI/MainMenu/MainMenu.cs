using System;
using UnityEngine;

namespace Core
{
    public class MainMenu : MonoBehaviour
    {
        //Events
        public event Action openMenu;
        public event Action closeMenu;



        private Animator animator;
        private string menuOpenAnimation = "MenuOpen";
        private string menuCloseAnimation = "MenuClose";

        private bool isOpen = false;

        private bool IsAnimating => (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1);

        private void Awake()
        {
            isOpen = false;
            animator = GetComponent<Animator>();
        }

        public void toggle()
        {
            if (IsAnimating)
                return;

            if (isOpen)
            {
                Close();

            }
            else
            {
                Open();
            }
        }

        public void Open()
        {
            isOpen = true;
            animator.Play(menuOpenAnimation);
            openMenu?.Invoke();
        }

        public void Close()
        {
            isOpen = false;
            animator.Play(menuCloseAnimation);
            closeMenu?.Invoke();
        }
    }
}