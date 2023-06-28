using UnityEngine;
namespace Core
{

    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    //For any character in the game
    public abstract class Character : MonoBehaviour
    {
        [SerializeField] protected MapLocation playerLocation;
        private Map map;
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
            map = Game.manager.Map;
            mover = new CharacterMover(this, map);
            turner = new CharacterTurner(mover, playerLocation);
            //Puts characters in center of tile at spawn

            transform.position = map.GetCellCenter2D(mover.currCell);
            map.addCell(mover.currCell, this);
        }

        // Update is called once per frame
        protected virtual void Update()
        {

            //Animator
            animator.ChooseLayer();
            animator.UpdateParameters();
        }

        public abstract void setCurrCell();



    }
}
