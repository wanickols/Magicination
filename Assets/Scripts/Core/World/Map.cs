using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{


    public Dictionary<Vector2Int, MonoBehaviour> occupiedCells { get; private set; } = new Dictionary<Vector2Int, MonoBehaviour>();

    public Grid grid { get; private set; }

    private void Awake()
    {
        grid = GetComponent<Grid>();
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
