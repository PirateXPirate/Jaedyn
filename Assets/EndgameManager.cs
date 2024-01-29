using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndgameManager : MonoBehaviour
{
    public GameObject popup1;
    public GameObject popup2;
    public GameObject popup3;
    public AudioClip EndGameSound;
    void Start()
    {
        StartCoroutine(ShowPopupWhenTrigger());
        Utils.soundManager.PlayLoop(EndGameSound);
    }

    IEnumerator ShowPopupWhenTrigger()
    {
        yield return new WaitForSeconds(10);
        popup1.SetActive(false);
        popup2.SetActive(true);
        yield return new WaitForSeconds(10);
        if (popup3)
        {
            popup2.SetActive(false);
            popup3.SetActive(true);
            yield return new WaitForSeconds(10);
            SceneManager.LoadScene("MapScene");
        }
        else
        {
            SceneManager.LoadScene("MapScene");
        }
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
