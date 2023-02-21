using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathPopup : MonoBehaviour
{
    [SerializeField] private string currentSceneName;
    [SerializeField] private GameObject controlUi;
    [SerializeField] private AudioClip homeButClickSound;

    bool isExiting = false;
    private void Start()
    {
        controlUi.SetActive(false);
        LevelManager.Instance.Players[0].LinkedInputManager.InputDetectionActive = false;
        LevelManager.Instance.Players[0].GetComponent<CharacterMovement>().enabled = false;
        LevelManager.Instance.Players[0].GetComponent<TopDownController3D>().enabled = false;
    }
    public void OnClickRestart()
    {
        if (!isExiting)
        {
            SceneManager.LoadScene(currentSceneName);
        }
           
    }
    public void OnClickExit()
    {
        if (!isExiting)
        {
            isExiting = true;

            Invoke("Delay", homeButClickSound.length);
            Utils.soundManager.PlayFX(homeButClickSound);

        }
    }
    void Delay()
    {

        SceneManager.LoadScene("MapScene");
    }
}
