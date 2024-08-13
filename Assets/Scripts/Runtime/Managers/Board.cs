using System;
using UnityEngine;

public class Board : MonoBehaviour
{
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private Transform tileParent;

    public Tile[] Tiles { get; private set;}

    void Awake()
    {
        TouchEvents.OnElementTapped += TileTapped;
    }

    private void OnDestroy()
    {
        TouchEvents.OnElementTapped -= TileTapped;
    }

    void PrepareTiles()
    {
        var tileCount = 5; 
        Tiles = new Tile[tileCount]; // todo: change with level tile amount 

        for (int i = 0; i < tileCount; i++)
        {
            Tiles[i] = Instantiate(tilePrefab, tileParent);
        }
    }

    void TileTapped(ITouchable touchable)
    {
        var tappedTile = touchable.gameObject.GetComponent<Tile>();
    }
} 
