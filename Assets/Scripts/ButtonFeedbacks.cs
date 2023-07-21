using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonFeedbacks : MonoBehaviour
{
    PopupManager PopupManager;
    UnlockMapsManager UnlockMapsManager;

    [SerializeField] Button tutorial;

    [Header("EasyMode Level")]
    [SerializeField] Button[] easyLevelButtons;

    [Header("HardMode Level")]
    [SerializeField] Button[] hardLevelButtons;

    [Header("Play Button")]
    public Button playButton;
    public Button blockPlayButton;

    public GameObject unLockObject;

    void Awake()
    {
        SetListenner();
        PopupManager = gameObject.GetComponent<PopupManager>();
        UnlockMapsManager = gameObject.GetComponent<UnlockMapsManager>();

    }

    void SetListenner()
    {

        for(int i = 0; i < easyLevelButtons.Length; i++)
        {
            var index = 0;
            index += i;
            easyLevelButtons[index].onClick.AddListener(() =>
            {
                ResetAllSize();
                SizeExpand(easyLevelButtons[index]);
                StateChecker(easyLevelButtons[index]);
                UnlockMapsManager.levelIndex = index+1;
                UnlockMapsManager.mode = "easy";
            });

            hardLevelButtons[index].onClick.AddListener(() =>
            {
                ResetAllSize();
                SizeExpand(hardLevelButtons[index]);
                StateChecker(hardLevelButtons[index]);
                UnlockMapsManager.levelIndex = index+1;
                UnlockMapsManager.mode = "hard";
            });
        }

        tutorial.onClick.AddListener(OnClickTutorialButton);
        blockPlayButton.onClick.AddListener(OnClickBlockPlayButton);
    }


    public void OnClickBlockPlayButton()
    {
        PopupManager.OpenPopUp(PopupManager.levelLockedPopUp);
    }
 
    public void OnClickLockedMapsButton()
    {
        PopupManager.OpenPopUp(PopupManager.unLockedPopup);
        ResetAllSize();
    }

    void OnClickTutorialButton()
    {
        ResetAllSize();
        SizeExpand(tutorial);
        StateChecker(tutorial);
    }
    void SizeExpand(Button button)
    {
        button.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }

    public void ResetAllSize()
    {
        var buttons = easyLevelButtons.Concat(hardLevelButtons).ToArray();
        SizeReset(tutorial);
        foreach (Button button in buttons) {
            SizeReset(button);
        }

        void SizeReset(Button button)
        {
            button.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        }
    }

    public void StateChecker(Button parent)
    {
        
        if (parent.transform.Find("BG_Lock").gameObject.activeSelf)
        {
            blockPlayButton.gameObject.SetActive(true);
            playButton.gameObject.SetActive(false);
            OnClickLockedMapsButton();
        }
        else
        {
            blockPlayButton.gameObject.SetActive(false);
            playButton.gameObject.SetActive(true);
        }
    }
    private void OnDisable()
    {
        var buttons = easyLevelButtons.Concat(hardLevelButtons).ToArray();
        foreach (Button button in buttons)
        {
            button.onClick.RemoveAllListeners();
        }
        
        tutorial.onClick.RemoveAllListeners();
        blockPlayButton.onClick.RemoveAllListeners();
    }
}
