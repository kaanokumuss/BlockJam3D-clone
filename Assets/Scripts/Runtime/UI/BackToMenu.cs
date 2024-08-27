using System;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    [SerializeField] private GameObject Background;
    [SerializeField] private GameObject MenuPanel;

    public void OnMenuPressed()
    {
        MenuPanel.SetActive(enabled);
        Background.SetActive(false);
    }

    public void OnMainMenuPressed()
    {
        SceneManager.LoadScene("BootScene");
    }

    public void OnBackPressed()
    {
        MenuPanel.SetActive(false);
        Background.SetActive(enabled);
    }
}