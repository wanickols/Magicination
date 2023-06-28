using UnityEngine;

public class MainMenu : MonoBehaviour
{
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
            Game.manager.returnState();
            Close();

        }
        else
        {
            Game.manager.changeState(GameState.Menu);
            Open();
        }
    }

    public void Open()
    {
        isOpen = true;
        animator.Play(menuOpenAnimation);
    }

    public void Close()
    {
        isOpen = false;
        animator.Play(menuCloseAnimation);
    }
}
