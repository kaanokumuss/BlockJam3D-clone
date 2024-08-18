using UnityEngine;
using DG.Tweening;

[System.Serializable]
public class Sphere : MonoBehaviour, ITouchable
{
    public int Index;
    public Material Material;
    public GameObject SphereObject;

    private float rotationDuration = 20f; // Dönüş süresi (saniye cinsinden)
    private Vector3 rotationAxis = Vector3.up; // Dönme ekseni (Y ekseni)

    private void Start()
    {
        RotateSphere(); // Dönme işlemini başlat
    }

    public Tween MoveTo(Vector3 targetPosition)
    {
        return transform.DOMove(targetPosition, 1f).SetEase(Ease.InOutQuad);
    }

    private void RotateSphere()
    {
        // Gezegenin kendi ekseni etrafında dönmesini sağlar
        transform.DORotate(rotationAxis * 360f, rotationDuration, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear) // Hızın sabit olması için Linear easing kullanılır
            .SetLoops(-1, LoopType.Restart); // Sonsuz döngü, sürekli dönmesini sağlar
    }
}