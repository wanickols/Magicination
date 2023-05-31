using UnityEngine;

public class Map : MonoBehaviour
{
    public static Grid grid { get; private set; }

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
