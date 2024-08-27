using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText; // TMP_Text türünde TextMeshPro referansı
    public int score = 0;

    private void OnEnable()
    {
        ScoreEvents.OnDestroyedSphere += IncreaseScore;
        ScoreEvents.OnTappedUndoButton += DecreaseScore;
    }

    private void OnDisable()
    {
        ScoreEvents.OnTappedUndoButton -= DecreaseScore;
        ScoreEvents.OnDestroyedSphere -= IncreaseScore;
    }

    public void IncreaseScore()
    {
        score += 10;
        UpdateScoreText();
    }

    public void DecreaseScore()
    {
        score -= 10;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (scoreText == null)
        {
            Debug.LogError("scoreText is not assigned.");
            return;
        }

        scoreText.text = "Score : " + score;
    }
}