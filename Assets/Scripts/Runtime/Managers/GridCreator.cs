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
                // Yeni bir tile oluştur
                instance = Instantiate(prefabTile, tilesParent);

                // Pozisyon hesapla ve uygula
                float xPosition = column * spacing;
                float zPosition = row * spacing;
                float yPosition = 0; // veya istediğiniz başka bir değer

                // Tile'ın pozisyonunu ayarla
                instance.transform.localPosition = new Vector3(xPosition, yPosition, zPosition);

                // Transform referansını sakla
                tiles[index] = instance.transform;

               // Debug.Log("Tile at index " + index + " position: " + instance.transform.position); // Pozisyonları logla
                index++;
            }
        }
    }
}