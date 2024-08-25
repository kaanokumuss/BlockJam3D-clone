using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject winPanel, failPanel;
    [SerializeField] private GameObject Enviroments;
    public float seconds = 5f;

    private void Awake()
    {
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
        Enviroments.SetActive(false);
        StartCoroutine(HideFailPanelAfterDelay()); // Paneli belirli bir süre sonra kapatacak Coroutine'i başlat
    }

    private IEnumerator HideFailPanelAfterDelay()
    {
        // Belirtilen süre kadar bekleyin
        yield return new WaitForSeconds(seconds);

        winPanel.SetActive(false);
        failPanel.SetActive(false);
        Enviroments.SetActive(true);

    }
}