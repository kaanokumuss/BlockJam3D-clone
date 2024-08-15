using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SettingsView : MonoBehaviour
{
    [SerializeField] GameObject settingsPanel;
    [SerializeField] Button closeButton;
    [SerializeField] Transform settingsContainer;

    void Awake()
    {
        CloseFast();

        UIEvents.OpenSettingsPanel += Appear;
        closeButton.onClick.AddListener(Disappear);
    }

    void OnDestroy()
    {
        UIEvents.OpenSettingsPanel -= Appear;
        closeButton.onClick.RemoveListener(Disappear);
    }

    void Appear()
    {
        DOTween.Kill(settingsContainer);
        
        settingsPanel.SetActive(true);

        settingsContainer.DOScale(1, .28f)
            .OnStart(() => settingsContainer.localScale = Vector3.one * .5f)
            .OnComplete(()=> closeButton.interactable = true)
            .SetEase(Ease.OutBack);
    }
    
    void Disappear()
    {
        DOTween.Kill(settingsContainer);
        
        settingsContainer.DOScale(0, .28f)
            .OnStart(()=> closeButton.interactable = false)
            .OnComplete(()=> settingsPanel.SetActive(false))
            .SetEase(Ease.InBack);
    }

    void CloseFast()
    {
        settingsContainer.localScale = Vector3.zero;
        closeButton.interactable = false;
        settingsPanel.SetActive(false);
    }
}
