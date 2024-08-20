using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionButton : MonoBehaviour
{
    [SerializeField] GameObject lockIcon, playText;
    [SerializeField] TextMeshProUGUI levelInfoText;
    [SerializeField] Button playButton;
    int _index;

    public void Prepare(LevelScoresData data)
    {
        playButton.interactable = data.isUnlocked;
        _index = data.index;
        Debug.Log("Prepared");
        UpdateSprite(data.isUnlocked);
        UpdateInfoText(data);
    }

    void UpdateSprite(bool isUnlocked)
    {
        
        lockIcon.SetActive(!isUnlocked);
        playText.SetActive(isUnlocked);
        Debug.Log($"Set Lock or Play");
    }

    void UpdateInfoText(LevelScoresData data)
    {
        levelInfoText.text = $"Level {data.index} - {data.title} {Environment.NewLine} HighScore: {data.highScore}";
    }
}