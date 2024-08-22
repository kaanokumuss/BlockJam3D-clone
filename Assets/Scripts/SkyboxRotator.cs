using UnityEngine;
using DG.Tweening; // DoTween için

public class SkyboxRotator : MonoBehaviour
{
    [SerializeField] private Material mat; // Skybox materyali
    [SerializeField] private float rotationDuration = 10f; // Tam tur süresi
    [SerializeField] private float maxRotation = 360f; // Maksimum döndürme açısı

    private void Start()
    {
        // `_Rotation` parametresini DoTween ile döndür
        DOTween.To(() => mat.GetFloat("_Rotation"), 
                value => mat.SetFloat("_Rotation", value), 
                maxRotation, 
                rotationDuration)
            .SetLoops(-1, LoopType.Incremental) // Sürekli döngü
            .SetEase(Ease.Linear); // Smooth, hızlanmayan döndürme
    }
}