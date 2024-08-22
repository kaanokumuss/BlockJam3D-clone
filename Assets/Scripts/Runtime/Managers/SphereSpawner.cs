using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereSpawner : MonoBehaviour
{
    [SerializeField] private Transform sphereParent;
    [SerializeField] private GameObject spherePrefab;
    [SerializeField] private LevelSelectionSO levelSelectionSO; // LevelSelectionSO referansı
    
    public SphereMaterialMixer materialAssigner;
    
    private void Start()
    {
        Debug.Log("SpawnSpheres metodu çalışıyor.");
        SpawnSpheres();
    }

    void SpawnSpheres()
    {
        if (spherePrefab == null || materialAssigner == null || levelSelectionSO == null || levelSelectionSO.levelData.planets == null)
        {
            Debug.LogWarning("Missing references or level data!");
            return;
        }

        SphereData[] planets = levelSelectionSO.levelData.planets;

        for (int i = 0; i < planets.Length; i++)
        {
            Vector3 position = planets[i].position;
            Debug.Log($"Spawning sphere at position: {position}");  // Pozisyonu loglayın
            GameObject sphere = Instantiate(spherePrefab, position, Quaternion.identity, sphereParent);

            materialAssigner.AssignMaterial(sphere, planets[i].material);
        
            Sphere sphereComponent = sphere.GetComponent<Sphere>();
            if (sphereComponent != null)
            {
                sphereComponent.Index = planets[i].id;
            }
        }
    }

}