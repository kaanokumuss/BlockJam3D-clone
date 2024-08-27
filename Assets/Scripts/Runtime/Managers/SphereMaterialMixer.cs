using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SphereMaterialMixer : MonoBehaviour
{
    [SerializeField] private LevelSelectionSO levelSelectionSO;

    public void AssignMaterial(GameObject sphere, string materialName)
    {
        Renderer renderer = sphere.GetComponent<Renderer>();
        if (renderer != null && levelSelectionSO.sphereMaterials.Length > 0)
        {
            Material material = System.Array.Find(levelSelectionSO.sphereMaterials, mat => mat.name == materialName);
            if (material != null)
            {
                renderer.material = material;
            }
            else
            {
                Debug.LogWarning($"Material '{materialName}' bulunamadı!");
            }
        }
    }
}