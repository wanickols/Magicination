using System;
using UnityEngine;

namespace MGCNTN
{
    [Serializable]
    public class MenuInputHandler
    {
        /// Private Parameters
        //Input
        private float pressThreshold = .005f; // The minimum time between key presses private float
        private float lastPressTime = 0f; // The time of the last key press

        //Components
        private SelectionManager selectorManager;
        private Selector currSelector => selectorManager.CurrentSelector;

        /// Public Parameters
        //Audio
        public AudioSource menuChangeSound;

        /// Public Functions
        //Accessors
        public void resetLastPressedTime() => lastPressTime = Time.time;

        //Constructor
        public void Init(SelectionManager manager)
        {
            selectorManager = manager;
        }

        //Input
        public void HandleInput()
        {
            // Calculate the time difference between the current and last press
            float timeDifference = Time.time - lastPressTime;

            // Check if the time difference is greater than or equal to the threshold
            if (timeDifference < pressThreshold)
                return;

            //Reset Last Pressed
            resetLastPressedTime();

            //Moving Input
            bool hasInput = currSelector.type switch
            {
                SelectorType.Vertical => verticalInput(),
                SelectorType.Horizontal => horizontalInput(),
                SelectorType.Grid => gridInput(),
                SelectorType.ScrollerVertical => scrollerInput(true),
                _ => false,
            };

            //Selcted Input (Enter Or Back)
            if (!hasInput)
                checkSelectedInput();


        }

        ///Private Functions
        private bool verticalInput(int increment = 1)
        {
            // Check which key is pressed and handle it accordingly
            if (Input.GetKeyDown(KeyCode.UpArrow) && currSelector.SelectedIndex > 0)
                move(-increment);
            else if (Input.GetKeyDown(KeyCode.DownArrow) && currSelector.SelectedIndex != currSelector.SelectableOptions.Count - 1)
                move(increment);
            else
                return false;

            return true;
        }
        private bool horizontalInput(int increment = 1)
        {
            // Check which key is pressed and handle it accordingly
            if (Input.GetKeyDown(KeyCode.LeftArrow) && currSelector.SelectedIndex > 0)
                move(-increment);
            else if (Input.GetKeyDown(KeyCode.RightArrow) && currSelector.SelectedIndex != currSelector.SelectableOptions.Count - 1)
                move(increment);
            else
                return false;

            return true;
        }
        private bool gridInput()
        {
            if (horizontalInput() || verticalInput(currSelector.columnCount))
                return true;

            return false;
        }
        private bool scrollerInput(bool vertical, int increment = 1)
        {
            if (vertical)
            {
                if (verticalInput(increment))
                    return true;
            }
            else if (horizontalInput(increment))
                return true;



            return false;
        }
        private void checkSelectedInput()
        {
            if (Input.GetKeyDown(KeyCode.Return))
                selectorManager.Accept();

            else if (Input.GetKeyDown(KeyCode.Escape))
                selectorManager.Cancel();
        }
        private void move(int increment)
        {
            menuChangeSound.Play();
            currSelector.SelectedIndex += increment;
            selectorManager.checkHover();
        }
    }
}