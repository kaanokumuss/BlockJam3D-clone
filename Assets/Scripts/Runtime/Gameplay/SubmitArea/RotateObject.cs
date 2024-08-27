using DG.Tweening;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationDuration = 2f;
    public Vector3 rotationAmount = new Vector3(360, 0, 0);

    void Start()
    {
        transform.DORotate(rotationAmount, rotationDuration, RotateMode.LocalAxisAdd)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Incremental);
    }
}