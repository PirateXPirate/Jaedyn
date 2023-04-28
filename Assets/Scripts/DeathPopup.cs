using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathPopup : MonoBehaviour
{
    [SerializeField] private string currentSceneName;
    [SerializeField] private GameObject controlUi;
    [SerializeField] private AudioClip homeButClickSound;

    [SerializeField] private AudioClip startPopupSound;
    [SerializeField] private AudioClip restartSound;

    [SerializeField] private Button setupBut;

    bool isExiting = false;
    private void Start()
    {
        controlUi.SetActive(false);
        Utils.soundManager.PlayFX(startPopupSound);
        LevelManager.Instance.Players[0].LinkedInputManager.InputDetectionActive = false;
        LevelManager.Instance.Players[0].GetComponent<CharacterMovement>().enabled = false;
        LevelManager.Instance.Players[0].GetComponent<TopDownController3D>().enabled = false;
        setupBut.enabled = false;
    }
    public void OnClickRestart()
    {
        if (!isExiting)
        {
            isExiting = true;
            Utils.soundManager.PlayFX(restartSound);
            Invoke("Restart", homeButClickSound.length);
          
        }
           
    }
    void Restart()
    {
        SceneManager.LoadScene(currentSceneName);
    }
    public void OnClickExit()
    {
        if (!isExiting)
        {
            isExiting = true;

            Invoke("Delay", homeButClickSound.length);
            Utils.soundManager.PlayFX(homeButClickSound, true);

        }
    }
    void Delay()
    {

        SceneManager.LoadScene("MapScene");
    }
}
