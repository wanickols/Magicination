using UnityEngine;
namespace Core
{

    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(Animator))]
    //For any character in the game
    public abstract class Character : MonoBehaviour
    {

        private Map map;

        public CharacterMover mover { get; private set; }
        public CharacterAnimator Animator { get; private set; }
        public bool isMoving => mover.isMoving;

        public CharacterTurner turn { get; private set; }
        public Vector2Int facing => turn.Facing;

        public Vector2Int currCell => mover.currCell;




        protected virtual void Awake()
        {

            turn = new CharacterTurner();
            Animator = new CharacterAnimator(this);
        }

        // Start is called before the first frame update
        protected virtual void Start()
        {
            map = Game.manager.Map;
            mover = new CharacterMover(this, map);
            //Puts characters in center of tile at spawn
            Vector2Int currentCell = map.GetCell2D(gameObject);
            transform.position = map.GetCellCenter2D(currentCell);
            map.addCell(currentCell, this);
        }

        // Update is called once per frame
        protected virtual void Update()
        {

            //Animator
            Animator.ChooseLayer();
            Animator.UpdateParameters();
        }
    }
}
