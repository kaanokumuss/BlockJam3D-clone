using UnityEngine;
using DG.Tweening;

public class SkyboxRotator : MonoBehaviour
{
    [SerializeField] private Material mat;
    [SerializeField] private float rotationDuration = 10f;
    [SerializeField] private float maxRotation = 360f;

    private void Start()
    {
        DOTween.To(() => mat.GetFloat("_Rotation"),
                value => mat.SetFloat("_Rotation", value),
                maxRotation,
                rotationDuration)
            .SetLoops(-1, LoopType.Incremental)
            .SetEase(Ease.Linear);
    }
}