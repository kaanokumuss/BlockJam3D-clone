using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class Sphere : MonoBehaviour, ITouchable
{
    public int Index;
    public Color Color;
    public GameObject SphereObject;
    public Tween MoveTo(Vector3 targetPosition)
    {
        return transform.DOMove(targetPosition, 1f).SetEase(Ease.InOutQuad);
    }
}
