using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour
{
    ButtonFeedbacks ButtonFeedBacks;
    PopupManager PopupManager;
    FadeController FadeController;

    [Header("Button")]
    [SerializeField] private Button easyModeButton;
    [SerializeField] private Button hardModeButton;
    [SerializeField] private Button playButton;
    [SerializeField] private Button homeButton;
    [SerializeField] private Button shopButton;

    [Header("UI")]
    [SerializeField] private GameObject easyModeUi;
    [SerializeField] private GameObject hardModeUi;

    public bool isHardmodePlayable;
    public string sceneToPlay = "";

    void Awake()
    {
        LevelData.LoadLevelStateData();
        ButtonFeedBacks = gameObject.GetComponent<ButtonFeedbacks>();
        PopupManager = gameObject.GetComponent<PopupManager>();
        FadeController = gameObject.GetComponent<FadeController>();
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
        playButton.onClick.AddListener(() => StartCoroutine(PlayButton()));
        homeButton.onClick.AddListener(() => StartCoroutine(HomeButton()));
        shopButton.onClick.AddListener(ShopButton);
    }

    IEnumerator PlayButton()
    {
        float fadingTime = 1.5f;
        if (sceneToPlay == "") { yield break; }

        Debug.Log($"Going to {sceneToPlay} scene");
        FadeController.isGotoNextScenePressed = true;
        yield return new WaitForSeconds(fadingTime);
        SceneManager.LoadScene(sceneToPlay);
        yield return null;
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

    IEnumerator HomeButton()
    {
        float fadingTime = 1.5f;

        FadeController.isGotoNextScenePressed = true;
        yield return new WaitForSeconds(fadingTime);
        SceneManager.LoadScene("MainMenuScene");
        yield return null;
    }

    void ShopButton()
    {
        //TODO FadeIn
        //TODO LoadScene ShopScene
        Debug.Log("ShopButton is Press!!");
    }

    void SetUpUi()
    {
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
        map.transform.Find("BG_Brown").gameObject.SetActive(false);
        map.transform.Find("BG_Pass").gameObject.SetActive(false);
        map.transform.Find("BG_Lock").gameObject.SetActive(false);
        map.transform.Find("Lock").gameObject.SetActive(false);
        map.transform.Find("Star").gameObject.SetActive(false);
        map.transform.Find("StarYellow").gameObject.SetActive(false);
    }
    void LevelState0(Transform map)
    {
        CloseAllSymbol(map);
        map.transform.Find("BG_Lock").gameObject.SetActive(true);
        map.transform.Find("Lock").gameObject.SetActive(true);
    }

    void LevelState1(Transform map)
    {
        CloseAllSymbol(map);
        map.transform.Find("BG_Pass").gameObject.SetActive(true);
        map.transform.Find("Star").gameObject.SetActive(true);
    }

    void LevelState2(Transform map)
    {
        CloseAllSymbol(map);
        map.transform.Find("BG_Pass").gameObject.SetActive(true);
        map.transform.Find("StarYellow").gameObject.SetActive(true);
    }

   void SetBrownPanelEasy(int levelIndex)
    {
        if (!LevelData.isTutorialComplete) { return; }
        var map = easyModeUi.transform.Find($"easyLevel{levelIndex}");
        CloseAllSymbol(map);
        map.transform.Find("BG_Brown").gameObject.SetActive(true);
        map.transform.Find("Star").gameObject.SetActive(true);
    }
    void SetBrownPanelHard(int levelIndex) 
    { 
        if (!LevelData.isTutorialComplete) { return; }
        var map = hardModeUi.transform.Find($"hardLevel{levelIndex}");
        CloseAllSymbol(map);
        map.transform.Find("BG_Brown").gameObject.SetActive(true);
        map.transform.Find("Star").gameObject.SetActive(true);
    }

    void SetCompleteTutorial()
    {
        if (LevelData.isTutorialComplete)
        {
            var map = easyModeUi.transform.Find($"LevelTutorial");
            map.transform.Find("BG_Brown").gameObject.SetActive(false);
            map.transform.Find("BG_Pass").gameObject.SetActive(true);
        }
    }
    #endregion 

}
