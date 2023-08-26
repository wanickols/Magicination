using UnityEngine;
using UnityEngine.UI;

namespace MGCNTN.Battle
{
    public class TurnPortrait : MonoBehaviour
    {
        [SerializeField] private Image turnPortrait;

        public void setSprite(Sprite sprite)
        {
            turnPortrait.sprite = sprite;

        }
    }
}
