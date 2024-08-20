using UnityEngine;
using TMPro;
public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText; // TMP_Text türünde TextMeshPro referansı
    private int score = 0;

    private void OnEnable()
    {
        ScoreEvents.OnDestroyedSphere += GetPoint;
        ScoreEvents.OnTappedUndoButton += LosePoint;
    }

    private void OnDisable()
    {
        ScoreEvents.OnTappedUndoButton -= LosePoint;
        ScoreEvents.OnDestroyedSphere -= GetPoint;
    }

    private void GetPoint()
    {
        score += 10;
        UpdateScoreText();
    }

    void LosePoint()
    {
        score -= 10;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score : " + score;
    }
}