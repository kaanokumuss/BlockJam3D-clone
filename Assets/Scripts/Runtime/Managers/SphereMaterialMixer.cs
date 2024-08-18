using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereMaterialMixer : MonoBehaviour
{
    [SerializeField] public Material[] sphereMaterials; // Farklı material'ları buraya ekle
    // Veya sprite kullanıyorsan:
    // [SerializeField] private Sprite[] sphereSprites;

    public void AssignMaterial(GameObject sphere)
    {
        Renderer renderer = sphere.GetComponent<Renderer>();
        if (renderer != null && sphereMaterials.Length > 0)
        {
            int randomIndex = Random.Range(0, sphereMaterials.Length);
            renderer.material = sphereMaterials[randomIndex];
        }
    }

    // Eğer SpriteRenderer kullanıyorsan:
    // public void AssignSprite(GameObject sphere)
    // {
    //     SpriteRenderer spriteRenderer = sphere.GetComponent<SpriteRenderer>();
    //     if (spriteRenderer != null && sphereSprites.Length > 0)
    //     {
    //         int randomIndex = Random.Range(0, sphereSprites.Length);
    //         spriteRenderer.sprite = sphereSprites[randomIndex];
    //     }
    // }
}