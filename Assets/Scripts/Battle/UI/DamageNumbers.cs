using TMPro;
using UnityEngine;

namespace MGCNTN.Battle
{
    public class DamageNumbers : MonoBehaviour
    {
        ///Public variables
        public Vector3 targetPosition = Vector3.zero;

        ///Private Variables
        [SerializeField] private float animationSpeed = 1.0f;

        ///Public Functions
        public void setText(int damage, Color color)
        {
            TextMeshProUGUI text = GetComponentInChildren<TextMeshProUGUI>();
            text.text = "" + damage;
            text.color = color;
        }
        ///Unity Functions
        private void Awake()
        {
            targetPosition += transform.position;
        }

        // Update is called once per frame
        private void Update()
        {
            if (transform.position != targetPosition)
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * animationSpeed);
        }
    }
}