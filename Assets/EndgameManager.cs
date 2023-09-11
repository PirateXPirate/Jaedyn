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
    void Start()
    {
        StartCoroutine(ShowPopupWhenTrigger());
    }

    IEnumerator ShowPopupWhenTrigger()
    {
        yield return new WaitForSeconds(5);
        popup1.SetActive(false);
        popup2.SetActive(true);
        yield return new WaitForSeconds(5);
        if (popup3)
        {
            popup2.SetActive(false);
            popup3.SetActive(true);
            yield return new WaitForSeconds(5);
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
