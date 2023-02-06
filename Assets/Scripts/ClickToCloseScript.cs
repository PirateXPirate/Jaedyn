using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ClickToCloseScript : MonoBehaviour
{
    //For Dim Story
    public Text txtStory;

    public AudioClip fxClick;

    void Start()
    {
        gameObject.SetActive(false);
    }

    public void SetupStory(string msg) {
        txtStory.text = msg;
        gameObject.SetActive(true);
    }

    public void OnClickClose() {
        Utils.soundManager.PlayFX(fxClick);
        gameObject.SetActive(false);
    }

    public void OnClickGoTitle() {
        Utils.soundManager.PlayFX(fxClick);
        SceneManager.LoadScene("MapScene");
    }
}
