using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
//For any character in the game
public abstract class Character : MonoBehaviour
{

    public CharacterMover mover { get; private set; }
    public CharacterAnimator Animator { get; private set; }
    public bool isMoving => mover.isMoving;

    public CharacterTurner turn { get; private set; }
    public Vector2Int facing => turn.Facing;

    public Vector2Int currCell => mover.currCell;




    protected virtual void Awake()
    {
        mover = new CharacterMover(this);
        turn = new CharacterTurner();
        Animator = new CharacterAnimator(this);
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //Puts characters in center of tile at spawn
        Vector2Int currentCell = Game.Map.grid.GetCell2D(this.gameObject);
        transform.position = Game.Map.grid.GetCellCenter2D(currentCell);
        Game.Map.occupiedCells.Add(currentCell, this);
    }

    // Update is called once per frame
    protected virtual void Update()
    {

        //Animator
        Animator.ChooseLayer();
        Animator.UpdateParameters();
    }
}
