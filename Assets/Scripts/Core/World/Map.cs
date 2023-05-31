using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public static Grid grid { get; private set; }

    public static Dictionary<Vector2Int, MonoBehaviour> occupiedCells { get; private set; } = new Dictionary<Vector2Int, MonoBehaviour>();
    private void Awake()
    {
        grid = FindObjectOfType<Grid>();
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
