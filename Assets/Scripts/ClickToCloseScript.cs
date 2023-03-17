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
    float currentVolume;
    public bool IgnoreSound;
    AudioSource audi;
    private void Start()
    {
        
        if (Utils.soundManager)
        {
            Utils.soundManager.loop.volume = PlayerPrefs.GetFloat("loop", .5f);
        }

    }
    private void OnEnable()
    {
       
        audi = GetComponent<AudioSource>();
        if (audi)
            audi.volume = Utils.soundManager.fx.volume;
        if (Utils.soundManager)
        {
            currentVolume = Utils.soundManager.loop.volume;
            if (IgnoreSound) return;
            var audi = GetComponent<AudioSource>();
            if (audi)
                audi.volume = currentVolume;
            Utils.soundManager.loop.volume *= 0.25f;
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (fxClick)
                Utils.soundManager.PlayFX(fxClick, true);
            gameObject.SetActive(false);
            if (!IgnoreSound)
                Utils.soundManager.loop.volume = currentVolume;
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
    public void SetupStory(string msg)
    {
        //  txtStory.text = msg;
        gameObject.SetActive(true);
    }

    public void OnClickClose()
    {
        Utils.soundManager.PlayFX(fxClick, true);

        gameObject.SetActive(false);
    }

    public void OnClickGoTitle()
    {
        Utils.soundManager.PlayFX(fxClick, true);
        SceneManager.LoadScene("MapScene");
    }
}
