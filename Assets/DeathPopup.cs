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
    private void Start()
    {
        controlUi.SetActive(false);
        LevelManager.Instance.Players[0].LinkedInputManager.InputDetectionActive = false;
        LevelManager.Instance.Players[0].GetComponent<CharacterMovement>().enabled = false;
        LevelManager.Instance.Players[0].GetComponent<TopDownController3D>().enabled = false;
    }
    public void OnClickRestart()
    {
        SceneManager.LoadScene(currentSceneName);
    }
    public void OnClickExit()
    {
        Utils.soundManager.PlayFX(homeButClickSound);
        
        SceneManager.LoadScene("MapScene");
    }
}
