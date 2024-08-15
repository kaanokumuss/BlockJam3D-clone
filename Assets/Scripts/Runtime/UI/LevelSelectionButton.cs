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
    
    void OnEnable()
    {
        playButton.onClick.AddListener(OnClick);
    }

    void OnDisable()
    {
        playButton.onClick.RemoveListener(OnClick);
    }

    public void Prepare(LevelScoresData data)
    {
        playButton.interactable = data.isUnlocked;
        _index = data.index;
        UpdateSprite(data.isUnlocked);
        UpdateInfoText(data);
    }

    void UpdateSprite(bool isUnlocked)
    {
        lockIcon.SetActive(!isUnlocked);
        playText.SetActive(isUnlocked);
    }

    void UpdateInfoText(LevelScoresData data)
    {
        levelInfoText.text = $"Level {data.index} - {data.title} {Environment.NewLine} HighScore: {data.highScore}";
    }

    void OnClick()
    {
        LevelEvents.OnLevelSelected?.Invoke(_index);
    }
}