using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSpawner : MonoBehaviour
{
    [SerializeField] private Transform sphereParent;
    [SerializeField] private GameObject spherePrefab;
    public GridCreator gridCreator;
    public SphereMaterialMixer materialAssigner; // ismi değiştirilmiş
    
    private void Start()
    {
        SpawnSphere();
    }

    void SpawnSphere()
    {
        if (gridCreator == null || gridCreator.tiles == null || spherePrefab == null || materialAssigner == null)
        {
            Debug.LogWarning("Missing references!");
            return;
        }

        for (int i = 0; i < gridCreator.tiles.Length; i++)
        {
            Vector3 newPosition = gridCreator.tiles[i].position;
            newPosition.y += 0.1f;
            Debug.Log("Sphere will spawn at: " + newPosition); // Pozisyonu loglayın
            GameObject sphere = Instantiate(spherePrefab, newPosition, Quaternion.identity, sphereParent);
            materialAssigner.AssignMaterial(sphere); // Material atama
        }
    }

}