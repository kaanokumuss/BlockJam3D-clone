using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject winPanel, failPanel , dangerPanel;
    [SerializeField] private GameObject Enviroments;
    public float seconds = 5f;

    private void Awake()
    {
        GameEvents.CantTouch += OpenDanger;
        GameEvents.WinPanel += OpenWinPanel;
        GameEvents.OnFail += OpenFailPanel;
    }

    private void OnDestroy()
    {
        GameEvents.WinPanel -= OpenWinPanel;
        GameEvents.OnFail -= OpenFailPanel;
    }

    private void OpenWinPanel()
    {
        winPanel.SetActive(true);
        Enviroments.SetActive(false);
        StartCoroutine(HideFailPanelAfterDelay()); // Paneli belirli bir süre sonra kapatacak Coroutine'i başlat
    }

    private void OpenFailPanel()
    {
        failPanel.SetActive(true);
        StartCoroutine(HideFailPanelAfterDelay()); // Paneli belirli bir süre sonra kapatacak Coroutine'i başlat
    }

    private void OpenDanger()
    {
        dangerPanel.SetActive(true);
        Enviroments.SetActive(false);
        StartCoroutine(HideFailPanelAfterDelay()); // Paneli belirli bir süre sonra kapatacak Coroutine'i başlat
    }

    private IEnumerator HideFailPanelAfterDelay()
    {
        // Belirtilen süre kadar bekleyin
        yield return new WaitForSeconds(seconds);
        dangerPanel.SetActive(false);
        winPanel.SetActive(false);
        failPanel.SetActive(false);
        Enviroments.SetActive(true);

    }
}