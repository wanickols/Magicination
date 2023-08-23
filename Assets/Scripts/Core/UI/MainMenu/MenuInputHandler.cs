using System;
using System.Collections;
using UnityEngine;

namespace Core
{
    [Serializable]
    public class MenuInputHandler
    {

        /// Public Parameters
        //Audio
        public AudioSource menuChangeSound;

        /// Private Parameters
        //Input
        private float pressThreshold = .005f; // The minimum time between key presses private float
        private float lastPressTime = 0f; // The time of the last key press
        private SelectorManager selectorManager;
        private Selector currSelector => selectorManager.CurrentSelector;

        private int scrollIndex = 0;
        private float scrollSpeed = 2f;
        private bool isScrolling = false;

        private int height
        {
            get
            {
                if (currSelector.type != SelectorType.Grid)
                    return (currSelector.SelectableOptions.Count - (currSelector.scrollMovementTrigger * 2)) / 2;
                else
                    return (currSelector.SelectableOptions.Count) / currSelector.columnCount;
            }
        }

        /// Public Functions
        //Accessors
        public void resetLastPressedTime() => lastPressTime = Time.time;
        public void Init(SelectorManager manager)
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

        private void tryScroll(int increment, bool vertical = true)
        {

            Vector2 dir = Vector2.zero;
            if (vertical)
                dir.y = increment;
            else
                dir.x = increment;


            Debug.Log("Trying to Scroll");
            int trigger = currSelector.scrollMovementTrigger;

            if (scrollIndex < currSelector.SelectableOptions.Count - trigger && scrollIndex > trigger)
            {
                Vector2 targetPosition = clamp((Vector2)currSelector.transform.position + (-dir / height), 0, 1);
                Game.manager.StartCoroutine(CO_Scroll(targetPosition));
            }
        }

        private Vector2 clamp(Vector2 vector, float min, float max)
        {
            vector.x = Math.Clamp(vector.x, min, max);
            vector.y = Math.Clamp(vector.y, min, max);

            return vector;
        }

        private IEnumerator CO_Scroll(Vector2 targetPosition)
        {

            isScrolling = true;
            Debug.Log("Scrollin to " + targetPosition);

            while (isScrolling)
            {
                // Lerp between current position and target position
                currSelector.scrollRect.normalizedPosition = Vector2.MoveTowards(currSelector.scrollRect.normalizedPosition, targetPosition, scrollSpeed * Time.deltaTime);


                float val = Mathf.Abs((currSelector.scrollRect.normalizedPosition - targetPosition).magnitude);
                // Check if current position is close enough to target position
                if (val < 0.01f)
                {
                    currSelector.scrollRect.normalizedPosition = targetPosition;
                    isScrolling = false;

                }

                yield return null;
            }

            Debug.Log("Scrolled");
        }

    }
}