using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToBattle : MonoBehaviour
{
    public Animator controller;
    public TextMeshProUGUI loadingText;

    public void GoToBattle()
    {
        if(DataHandler.Instance.quotaInfo.GetBattlesRemaining() > 0)
        {
            DataHandler.Instance.quotaInfo.SetBattlesRemaining(DataHandler.Instance.quotaInfo.GetBattlesRemaining() - 1);
            controller.SetTrigger("Open");
            StartCoroutine(HandleLoadingText());
            StartCoroutine(StartLoadDelay());
        }
    }
    
    private IEnumerator HandleLoadingText()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.25f);
            string text = loadingText.text;

            if(text.Contains("..."))
            {
                text = "Loading.";
            }
            else if(text.Contains(".."))
            {
                text = "Loading...";
            }
            else
            {
                text = "Loading..";
            }

            loadingText.text = text;
        }
    }

    private IEnumerator StartLoadDelay()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadSceneAsync(1);
    }
}
