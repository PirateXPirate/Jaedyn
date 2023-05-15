using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    ButtonFeedbacks ButtonFeedBacks;
    PopupManager PopupManager;

    [Header("Button")]
    [SerializeField] private Button easyModeButton;
    [SerializeField] private Button hardModeButton;
    [SerializeField] private Button playButton;
    [SerializeField] private Button homeButton;
    [SerializeField] private Button shopButton;

    [Header("UI")]
    [SerializeField] private GameObject easyModeUi;
    [SerializeField] private GameObject hardModeUi;

    [Header("Dialog")]
    [SerializeField] private GameObject dialogPanel;
    [SerializeField] private CanvasGroup dialogGroup;
    [SerializeField] private Button overLayButton;

    public bool isHardmodePlayable;
    public string sceneToPlay = "";

    void Awake()
    {
        LevelData.LoadLevelStateData();
        ButtonFeedBacks = gameObject.GetComponent<ButtonFeedbacks>();
        PopupManager = gameObject.GetComponent<PopupManager>();
        SetButtonListener();
    }

    void Start()
    {
        easyModeButton.interactable = false;
        SetUpUi();
        SetCompleteTutorial();
    }

    public void DefineSceneToPlay(string sceneName)
    {
        //defining scene to play while clicking on any map button
        sceneToPlay = sceneName;
    }

    void SetButtonListener()
    {
        hardModeButton.onClick.AddListener(HardMode);
        easyModeButton.onClick.AddListener(EasyMode);
        playButton.onClick.AddListener(PlayButton);
        homeButton.onClick.AddListener(HomeButton);
        shopButton.onClick.AddListener(ShopButton);
    }

    void PlayButton()
    {
        if (sceneToPlay == "") { return; }
        
        if (sceneToPlay == "EasyModeLevel1Scene") //scene1 name
        {
            StartCoroutine(StartDialog());
        }
        else
        {
            SceneManager.LoadScene(sceneToPlay);
        }
        

        IEnumerator StartDialog()
        {
            float dialogTime = 2f;
            float blockingTime = 2f;

            dialogPanel.SetActive(true);
            dialogGroup.DOFade(2, dialogTime);
            yield return new WaitForSeconds(blockingTime);
            overLayButton.gameObject.SetActive(true);
        }
    }
    void HardMode()
    {
        if (IsHardModePlayable())
        {
            hardModeUi.SetActive(true);
            easyModeUi.SetActive(false);
            ButtonFeedBacks.ResetAllSize();
            ButtonFeedBacks.blockPlayButton.gameObject.SetActive(true);
            ButtonFeedBacks.playButton.gameObject.SetActive(false);
            hardModeButton.interactable = false;
            easyModeButton.interactable = true;
        }

        else
        {
            Debug.Log("Show Popup HardModeLock");
            PopupManager.OpenPopUp(PopupManager.hardModeLockedPopUp);
        }
    }

    bool IsHardModePlayable()
    {
        //TODO Check if Hard mode is Unlocked
        return isHardmodePlayable;
    }

    void EasyMode()
    {
        hardModeUi.SetActive(false);
        easyModeUi.SetActive(true);
        ButtonFeedBacks.ResetAllSize();
        ButtonFeedBacks.blockPlayButton.gameObject.SetActive(true);
        ButtonFeedBacks.playButton.gameObject.SetActive(false);
        hardModeButton.interactable = true;
        easyModeButton.interactable = false;
    }

    void HomeButton()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    void ShopButton()
    {
        //TODO FadeIn
        //TODO LoadScene ShopScene
        Debug.Log("ShopButton is Press!!");
    }

    public void GoToScene1() => SceneManager.LoadScene("EasyModeLevel1Scene");
    void SetUpUi()
    {
        LevelData.easyModeState[0] = 1;
        LevelData.hardModeState[0] = 1;
        LevelData.easyModeState[1] = 1;
        LevelData.hardModeState[1] = 1;
        LevelData.easyModeState[2] = 1;
        LevelData.hardModeState[2] = 1;

        //setup EasymodeUI according to data
        for (int i = 0; i < LevelData.easyModeState.Length; i++)
        {
            if (LevelData.easyModeState[i] != 0)
            {
                SetActiveSymbolEasy(LevelData.easyModeState[i], i + 1);
            }            
            else
            {
                SetBrownPanelEasy(i+1);
                break;
            }
                
        }

        //setup HardmodeUI according to data
        for (int i = 0; i < LevelData.hardModeState.Length; i++)
        {
            if (LevelData.hardModeState[i] != 0)
            {
                SetActiveSymbolHard(LevelData.hardModeState[i], i + 1);
            }
            else
            {
               SetBrownPanelHard(i+1);
               break;
            }

            
        }
    }
    void SetActiveSymbolEasy(int levelState, int levelIndex)
    {
        var map = easyModeUi.transform.Find($"easyLevel{levelIndex}");

        switch (levelState)
        {
            case 0:
                LevelState0(map);
                break;
            case 1:
                LevelState1(map);
                break;
            case 2:
                LevelState2(map);
                break;
        }
    }
    void SetActiveSymbolHard(int levelState, int levelIndex)
    {
        var map = hardModeUi.transform.Find($"hardLevel{levelIndex}");

        switch (levelState)
        {
            case 0:
                LevelState0(map);
                break;
            case 1:
                LevelState1(map);
                break;
            case 2:
                LevelState2(map);
                break;
        }
    }

    #region -SetActive UI-
    void CloseAllSymbol(Transform map)
    {
        map.transform.Find("BG_Brown")?.gameObject.SetActive(false);
        map.transform.Find("BG_Pass")?.gameObject.SetActive(false);
        map.transform.Find("BG_Lock")?.gameObject.SetActive(false);
        map.transform.Find("Lock")?.gameObject.SetActive(false);
        map.transform.Find("Star")?.gameObject.SetActive(false);
        map.transform.Find("StarYellow")?.gameObject.SetActive(false);
    }
    void LevelState0(Transform map)
    {
        CloseAllSymbol(map);
        map.transform.Find("BG_Lock")?.gameObject.SetActive(true);
        map.transform.Find("Lock")?.gameObject.SetActive(true);
    }

    void LevelState1(Transform map)
    {
        CloseAllSymbol(map);
        map.transform.Find("BG_Pass")?.gameObject.SetActive(true);
        map.transform.Find("Star")?.gameObject.SetActive(true);
    }

    void LevelState2(Transform map)
    {
        CloseAllSymbol(map);
        map.transform.Find("BG_Pass")?.gameObject.SetActive(true);
        map.transform.Find("StarYellow")?.gameObject.SetActive(true);
    }

   void SetBrownPanelEasy(int levelIndex)
    {
        if (!LevelData.isTutorialComplete) { return; }
        var map = easyModeUi.transform.Find($"easyLevel{levelIndex}");
        CloseAllSymbol(map);
        map.transform.Find("BG_Brown")?.gameObject.SetActive(true);
        map.transform.Find("Star")?.gameObject.SetActive(true);
    }
    void SetBrownPanelHard(int levelIndex) 
    { 
        if (!LevelData.isTutorialComplete) { return; }
        var map = hardModeUi.transform.Find($"hardLevel{levelIndex}");
        CloseAllSymbol(map);
        map.transform.Find("BG_Brown")?.gameObject.SetActive(true);
        map.transform.Find("Star")?.gameObject.SetActive(true);
    }

    void SetCompleteTutorial()
    {
        var map = easyModeUi.transform.Find($"LevelTutorial");

        if (LevelData.isTutorialComplete)
        {
            map.transform.Find("BG_Brown")?.gameObject.SetActive(false);
            map.transform.Find("BG_Pass")?.gameObject.SetActive(true);
        }

        if (LevelData.isTutorialCompleteWithPicture)
        {
            map.transform.Find("Star")?.gameObject.SetActive(false);
            map.transform.Find("StarYellow")?.gameObject.SetActive(true);
        }
    }
    #endregion 

}
