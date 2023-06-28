using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private Animator animator;
    private string menuOpenAnimation = "MenuOpen";
    private string menuCloseAnimation = "MenuClose";

    public bool isOpen { get; private set; }

    public bool IsAnimating => (animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1);

    private void Awake()
    {
        isOpen = false;
        animator = GetComponent<Animator>();
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
