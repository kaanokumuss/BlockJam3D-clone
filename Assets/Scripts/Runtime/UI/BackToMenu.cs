using System;
using DG.Tweening;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    [SerializeField] private GameObject Background ;
    [SerializeField] private GameObject MenuPanel;
    public void OnMenuPressed()
    {
        // MetaScene'i y�kle
        MenuPanel.SetActive(enabled);
        Background.SetActive(false);
        // SceneManager.LoadScene("BootScene");
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
    
    
        
    


    // void ClickMeAnimation()
    // {
    //     // DOTween.Sequence()
    //     //     .Append(transform.DOPunchScale(Vector3.one * .15f, .5f).SetEase(Ease.InOutExpo))
    //     //     .AppendInterval(.3f)
    //     //     .SetLoops(-1, LoopType.Restart)
    //     //     .OnKill(() =>
    //     //     {
    //     //         transform.localScale = Vector3.one;
    //     //     })
    //     //     .SetId(transform);
    // }
}
