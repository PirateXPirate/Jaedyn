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
        yield return new WaitForSeconds(30);
        popup1.SetActive(false);
        popup2.SetActive(true);
        yield return new WaitForSeconds(30);
        if (popup3)
        {
            popup2.SetActive(false);
            popup3.SetActive(true);
            yield return new WaitForSeconds(30);
            if (SceneManager.GetActiveScene().name == "EndScene3")
            {
                SceneManager.LoadScene("GalleryScene");
            }
            else
            {
                SceneManager.LoadScene("MapScene");
            }
           
        }
        else
        {
            if (SceneManager.GetActiveScene().name == "EndScene3")
            {
                SceneManager.LoadScene("GalleryScene");
            }
            else
            {
                SceneManager.LoadScene("MapScene");
            }
        }
      
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (SceneManager.GetActiveScene().name == "EndScene3")
            {
                SceneManager.LoadScene("GalleryScene");
            }
            else
            {
                SceneManager.LoadScene("MapScene");
            }
        }
    }
}
