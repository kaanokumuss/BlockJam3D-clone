using UnityEngine;
using TMPro;
using DG.Tweening;

public class DuprinGames: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private float dropDuration = 1f;  // Her harfin düşme süresi
    [SerializeField] private float interval = 0.1f;    // Harfler arasında bekleme süresi
    [SerializeField] private float startOffset = 10f;   // Düşüşün başlangıçta offsetlenmesi için

    private void Start()
    {
        ApplyVerticalMatrixEffect();
    }

    private void ApplyVerticalMatrixEffect()
    {
        // Metni harflere ayır
        string text = textMeshPro.text;
        textMeshPro.text = "";  // TextMeshPro bileşenini temizle

        // Her harfi ayrı ayrı işleme al
        for (int i = 0; i < text.Length; i++)
        {
            char character = text[i];
            TextMeshProUGUI charText = CreateCharacterText(character, i);
            
            // Harfi düşür ve döngüye al
            AnimateCharacter(charText, i * interval);
        }
    }

    private TextMeshProUGUI CreateCharacterText(char character, int index)
    {
        GameObject charObject = new GameObject($"Char_{index}");
        charObject.transform.SetParent(textMeshPro.transform, false);
        
        TextMeshProUGUI charText = charObject.AddComponent<TextMeshProUGUI>();
        charText.text = character.ToString();
        charText.font = textMeshPro.font;
        charText.fontSize = textMeshPro.fontSize;
        charText.color = textMeshPro.color;
        charText.alignment = textMeshPro.alignment;
        
        charText.rectTransform.anchoredPosition = new Vector2(index * textMeshPro.fontSize, startOffset); // Başlangıç pozisyonu
        
        return charText;
    }

    private void AnimateCharacter(TextMeshProUGUI charText, float delay)
    {
        // Harfi düşür ve sürekli döngüye al
        charText.rectTransform.DOAnchorPosY(-textMeshPro.rectTransform.rect.height, dropDuration)
            .SetDelay(delay)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(Ease.InOutSine);
    }
}
