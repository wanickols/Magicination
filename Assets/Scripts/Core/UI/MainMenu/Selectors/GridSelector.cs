using Core;
using UnityEngine;
using UnityEngine.UI;

public class GridSelector : Selector
{
    /// Private Parameter
    private GridLayoutGroup grid;

    /// Unity Functions
    protected override void Awake()
    {
        base.Awake();
        grid = transform.parent.GetComponent<GridLayoutGroup>();
    }

    /// Public Functions
    public override void HandleInput()
    {
        Selector CurrentSelector = pauseMenu.CurrentSelector;
        int columnCount = grid.constraintCount;

        // Get the current time
        float currentTime = Time.time;

        // Calculate the time difference between the current and last press
        float timeDifference = currentTime - pauseMenu.lastPressTime;

        // Check if the time difference is greater than or equal to the threshold
        if (timeDifference >= pauseMenu.pressThreshold)
        {
            // Update the last press time
            pauseMenu.lastPressTime = currentTime;


            int maxCount = CurrentSelector.SelectableOptions.Count - 1;
            // Check which key is pressed and handle it accordingly
            if (Input.GetKeyDown(KeyCode.LeftArrow) && CurrentSelector.SelectedIndex > 0)
            {
                pauseMenu.menuChangeSound.Play();
                CurrentSelector.SelectedIndex--;
                pauseMenu.checkHover();
            }

            else if (Input.GetKeyDown(KeyCode.RightArrow) && CurrentSelector.SelectedIndex != maxCount)
            {
                pauseMenu.menuChangeSound.Play();
                CurrentSelector.SelectedIndex++;
                pauseMenu.checkHover();
            }

            else if (Input.GetKeyDown(KeyCode.UpArrow) && CurrentSelector.SelectedIndex > 0)
            {
                pauseMenu.menuChangeSound.Play();
                CurrentSelector.SelectedIndex -= columnCount;

                if (CurrentSelector.SelectedIndex < 0)
                    CurrentSelector.SelectedIndex = 0;

                pauseMenu.checkHover();
            }

            else if (Input.GetKeyDown(KeyCode.DownArrow) && CurrentSelector.SelectedIndex != maxCount)
            {
                pauseMenu.menuChangeSound.Play();
                CurrentSelector.SelectedIndex += columnCount;

                if (CurrentSelector.SelectedIndex > maxCount)
                    CurrentSelector.SelectedIndex = maxCount;

                pauseMenu.checkHover();
            }

            else if (Input.GetKeyDown(KeyCode.Return))
                pauseMenu.Accept();

            else if (Input.GetKeyDown(KeyCode.Escape))
                pauseMenu.Cancel();
        }
    }
}
