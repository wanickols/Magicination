using UnityEngine;

public class PanelToggler : MonoBehaviour
{

    [SerializeField] private GameObject panel;
    // Start is called before the first frame update

    public void togglePanel()
    {
        if (panel != null)
        {
            panel.SetActive(!panel.activeSelf);
        }
    }
}
