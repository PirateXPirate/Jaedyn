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

    public GameObject ActivateObject;
    [SerializeField] private TouchInputManager controllerUi;
    void Start()
    {
       
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (fxClick)
                Utils.soundManager.PlayFX(fxClick);
            gameObject.SetActive(false);
            LevelManager.Instance.Players[0].LinkedInputManager.InputDetectionActive = true;
            LevelManager.Instance.Players[0].GetComponent<CharacterMovement>().enabled = true;
            LevelManager.Instance.Players[0].GetComponent<TopDownController3D>().enabled = true;
            controllerUi.canInteract = true;
            if (ActivateObject)
            {
                ActivateObject.SetActive(true);
                ActivateObject = null;
            }
              
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
