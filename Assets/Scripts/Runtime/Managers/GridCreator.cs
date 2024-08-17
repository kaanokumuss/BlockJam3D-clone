using UnityEngine;
using UnityEngine.Serialization;

public class GridCreator : MonoBehaviour
{
    [SerializeField] private Transform tilesParent;
    [SerializeField] private GameObject prefabTile;
    [SerializeField] private int rows = 4;
    [SerializeField] private int columns = 3;
    [SerializeField] private float spacing = 2f;
    public Transform[] tiles;

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
                GameObject instance = Instantiate(prefabTile, tilesParent);
                float xPosition = column * spacing;
                float zPosition = row * spacing;
                instance.transform.position = new Vector3(xPosition, 0.01f, zPosition);
                tiles[index] = instance.transform;
                index++;
            }
        }
    }
}


