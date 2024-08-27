using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpaceRotater : MonoBehaviour
{
    public float duration = 2f;
    public float rotationAmount = 360f;

    void Start()
    {
        RotateContinuously();
    }

    void RotateContinuously()
    {
        transform.DORotate(new Vector3(rotationAmount, rotationAmount, rotationAmount), duration,
                RotateMode.LocalAxisAdd)
            .SetLoops(-1, LoopType.Incremental);
    }
}