using UnityEngine;
using DG.Tweening;
using NaughtyAttributes;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] private Button button;

    [Header("Punch Animation Settings")]
    [SerializeField, Range(0.01f, 1f)] private float punchScale = 0.1f;  // Punch büyüklüğü
    [SerializeField, Range(0.1f, 2f)] private float punchDuration = 0.5f; // Animasyon süresi

    void Start()
    {
        // Punch efekti için uygun easing ve parametrelerle ayarla
        button.transform.DOPunchScale(Vector3.one * punchScale, punchDuration)
            .SetEase(Ease.InOutQuad)  
            .SetLoops(-1, LoopType.Yoyo)  // Yoyo loop ile animasyon tekrarlanır
            .SetId(button.transform);    // Bu buton için bir ID ayarla (gerekirse kullanabilirsin)
    }

#if UNITY_EDITOR
    [Button]
    void FindButton()
    {
        button = GetComponent<Button>();
    }
#endif   
}