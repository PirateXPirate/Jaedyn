using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeButton : MonoBehaviour
{
    [SerializeField] private AudioClip homeButClickSound;
    bool isExiting = false;
    void Start()
    {
        
    }
    public void OnClickExit()
    {
        if (!isExiting)
        {
            LevelManager.Instance.Players[0].LinkedInputManager.InputDetectionActive = false;
            LevelManager.Instance.Players[0].GetComponent<CharacterMovement>().enabled = false;
            LevelManager.Instance.Players[0].GetComponent<TopDownController3D>().enabled = false;
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
