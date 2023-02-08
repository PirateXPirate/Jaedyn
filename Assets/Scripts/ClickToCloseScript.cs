using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MoreMountains.TopDownEngine;

public class ClickToCloseScript : MonoBehaviour
{
    //For Dim Story
    public Text txtStory;

    public AudioClip fxClick;

    public GameManager gameManager;

    void Start()
    {
       
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameObject.SetActive(false);
            LevelManager.Instance.Players[0].LinkedInputManager.InputDetectionActive = true;
        }
    }
    public void SetupStory(string msg) {
      //  txtStory.text = msg;
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
