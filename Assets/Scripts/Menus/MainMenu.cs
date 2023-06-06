using System.Collections;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    private Animator animator;
    private string menuOpenParamter = "menuOpen";
    bool isMenuOpen => Game.State == GameState.Menu;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {

        animator.SetBool(menuOpenParamter, isMenuOpen);

        if (Game.State == GameState.Menu)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                StartCoroutine(Co_CloseMenu());
            }
        }
    }

    private IEnumerator Co_CloseMenu()
    {
        yield return null; //adds extra frame
        Game.CloseMenu();
    }
}
