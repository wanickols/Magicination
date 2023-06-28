using UnityEngine;
namespace Core
{
    public class CharacterAnimator
    {
        private Character character;
        private Animator animator;

        private string horizontalParameter = "xDir";
        private string verticalParameter = "yDir";
        private string walkingParameter = "isWalking";

        public CharacterAnimator(Character character)
        {
            this.character = character;
            this.animator = character.GetComponent<Animator>();
        }

        public void ChooseLayer()
        {
            bool isWalking = character.isMoving;
            animator.SetBool(walkingParameter, isWalking);
        }

        public void UpdateParameters()
        {
            animator.SetFloat(horizontalParameter, character.facing.x);
            animator.SetFloat(verticalParameter, character.facing.y);
        }

    }
}
