using UnityEngine;

public class GridCreator : MonoBehaviour
{
    [SerializeField] private Transform tilesParent;
    [SerializeField] private GameObject prefabTile;
    [SerializeField] private int rows = 4;
    [SerializeField] private int columns = 3;
    [SerializeField] private float spacing = 2f;
    public Transform[] tiles;
    private GameObject instance;

    void Awake()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        tiles = new Transform[rows * columns];
        int index = 0;

        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                instance = Instantiate(prefabTile, tilesParent);

                float xPosition = column * spacing;
                float zPosition = row * spacing;
                float yPosition = 0; 
               
                instance.transform.localPosition = new Vector3(xPosition, yPosition, zPosition);

              
                tiles[index] = instance.transform;

              
                index++;
            }
        }
    }
}