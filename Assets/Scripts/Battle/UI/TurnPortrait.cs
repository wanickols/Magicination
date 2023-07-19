using UnityEngine;
using UnityEngine.UI;

public class TurnPortrait : MonoBehaviour
{
    [SerializeField] private Image turnPortrait;

    public void setSprite(Sprite sprite)
    {
        turnPortrait.sprite = sprite;

    }
}
