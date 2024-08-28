using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private GameObject self;
    [SerializeField] private GameObject UI;
    [SerializeField] private float waitTimeEnd = 35f;

    private static bool hasPlayed = false;

    void Start()
    {
        if (!hasPlayed)
        {
            UI.SetActive(false);
            StartCoroutine(WaitAndShowUI(waitTimeEnd));
        }
        else
        {
            UI.SetActive(true);
            self.SetActive(false);
        }
    }

    IEnumerator WaitAndShowUI(float waitTimeEnd)
    {
        yield return new WaitForSeconds(waitTimeEnd);
        UI.SetActive(true);
        self.SetActive(false);
        
        hasPlayed = true;
    }
}