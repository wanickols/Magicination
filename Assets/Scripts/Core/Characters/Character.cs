using UnityEngine;
namespace MGCNTN.Core
{

    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    //For any character in the game
    public abstract class Character : MonoBehaviour
    {
        [SerializeField] protected MapLocation playerLocation;
        public CharacterMover mover { get; private set; }
        public CharacterAnimator animator { get; private set; }
        public bool isMoving => mover.isMoving;
        public CharacterTurner turner { get; private set; }
        public Vector2Int facing => turner.Facing;

        public Vector2Int currCell => mover.currCell;


        protected virtual void Awake()
        {
            animator = new CharacterAnimator(this);
        }

        // Start is called before the first frame update
        protected virtual void Start()
        {
            mover = new CharacterMover(this);
            turner = new CharacterTurner(mover, playerLocation);
            mover.setUpStartingCell();
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            ///TODO maybe make animator check here so we can have non animated characters not need to update.
            //Animator
            animator.ChooseLayer();
            animator.UpdateParameters();
        }

        public abstract void setCurrCell(); ///TODO why not have defualt defintion
    }
}
