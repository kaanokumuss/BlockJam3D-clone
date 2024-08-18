using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SphereSpawner : MonoBehaviour
{
    [SerializeField] private GameObject spherePrefab;
    public GridCreator gridCreator;
    public SphereColorMixer colorAssigner; 

    private void Start()
    {
        SpawnSphere();
    }

    void SpawnSphere()
    {
        if (gridCreator == null || gridCreator.tiles == null || spherePrefab == null || colorAssigner == null)
        {
            Debug.LogWarning("Missing references!");
            return;
        }

        for (int i = 0; i < gridCreator.tiles.Length; i++)
        {
            Vector3 newPosition = gridCreator.tiles[i].position;
            newPosition.y += 3f;
            GameObject sphere = Instantiate(spherePrefab, newPosition, Quaternion.identity);
            colorAssigner.RandomColor(sphere);
        }
    }
}
