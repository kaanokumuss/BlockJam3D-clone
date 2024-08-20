using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMaterialMixer : MonoBehaviour
{
    [SerializeField] public Material[] sphereMaterials; 

    public void AssignMaterial(GameObject sphere)
    {
        Renderer renderer = sphere.GetComponent<Renderer>();
        if (renderer != null && sphereMaterials.Length > 0)
        {
            int randomIndex = Random.Range(0, sphereMaterials.Length);
            renderer.material = sphereMaterials[randomIndex];
        }
    }

    
}