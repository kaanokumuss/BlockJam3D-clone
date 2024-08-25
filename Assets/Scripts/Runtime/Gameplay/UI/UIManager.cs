using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject winPanel, failPanel;
    public float seconds =5f;
    private void Awake()
    {
        GameEvents.WinPanel += OpenWinPanel;
        // GameEvents.OnFail += OpenFailPanel;
    }

    private void OnDestroy()
    {
        GameEvents.WinPanel -= OpenWinPanel;
        // GameEvents.OnFail -= OpenFailPanel;
    }

    private void OpenWinPanel()
    {
        winPanel.SetActive(true);
        Debug.Log("ISETactive");
    }
    
    private void OpenFailPanel()
    {
        failPanel.SetActive(true);
    }
   
}
