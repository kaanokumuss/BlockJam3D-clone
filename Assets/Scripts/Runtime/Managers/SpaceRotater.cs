using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpaceRotater : MonoBehaviour
{
    public float duration = 2f; // Bir döngü süresi
    public float rotationAmount = 360f; // Her eksende dönecek derece miktarı

    void Start()
    {
        // Obje üzerinde sürekli döndürme animasyonunu başlat
        RotateContinuously();
    }

    void RotateContinuously()
    {
        // Döndürme animasyonunu tanımla ve başlat
        transform.DORotate(new Vector3(rotationAmount, rotationAmount, rotationAmount), duration, RotateMode.LocalAxisAdd)
            .SetLoops(-1, LoopType.Incremental);
    }
}
