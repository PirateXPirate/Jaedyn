using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    ButtonFeedbacks _ButtonFeedBacks;

    [Header("Button")]
    [SerializeField] private Button easyModeButton;
    [SerializeField] private Button hardModeButton;
    [SerializeField] private Button playButton;
    [SerializeField] private Button homeButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private Button unlockButton;

    [Header("UI")]
    [SerializeField] GameObject easyModeUi;
    [SerializeField] GameObject hardModeUi;

    [Header("Popup")]

    public bool isHardmodePlayable;
    public string sceneToPlay = "";

    void Awake()
    {
        LevelData.LoadLevelStateData();
        _ButtonFeedBacks = gameObject.GetComponent<ButtonFeedbacks>();
        SetButtonListener();
    }

    void Start()
    {
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
        easyModeButton.onClick.AddListener(EasyMode);
        hardModeButton.onClick.AddListener(HardMode);
        playButton.onClick.AddListener(PlayButton);
        homeButton.onClick.AddListener(HomeButton);
        shopButton.onClick.AddListener(ShopButton);
    }

    void PlayButton()
    {
        if (sceneToPlay == "") { return; }
        Debug.Log($"Go to {sceneToPlay} scene");
        SceneManager.LoadSceneAsync(sceneToPlay, LoadSceneMode.Single);
    }
    void HardMode()
    {
        if (IsHardModePlayable())
        {
            hardModeUi.SetActive(true);
            easyModeUi.SetActive(false);
            _ButtonFeedBacks.ResetAllSize();
            _ButtonFeedBacks.blockPlayButton.gameObject.SetActive(true);
            _ButtonFeedBacks.playButton.gameObject.SetActive(false);
            easyModeButton.onClick.AddListener(EasyMode);
            hardModeButton.onClick.RemoveListener(HardMode);
        }

        else
        {
            Debug.Log("Show Popup HardModeLock");
        }
    }

    bool IsHardModePlayable()
    {
        //TODO if Unlocked Hard mode
        return isHardmodePlayable;
    }

    void EasyMode()
    {
        Debug.Log("EasyMode Button is Press!!");
        hardModeUi.SetActive(false);
        easyModeUi.SetActive(true);
        _ButtonFeedBacks.ResetAllSize();
        _ButtonFeedBacks.blockPlayButton.gameObject.SetActive(true);
        _ButtonFeedBacks.playButton.gameObject.SetActive(false);
        easyModeButton.onClick.RemoveListener(EasyMode);
        hardModeButton.onClick.AddListener(HardMode);
    }

    void HomeButton()
    {
        Debug.Log("HomeButton is Press!!");
    }

    void ShopButton()
    {
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
    void LevelState0(Transform map)
    {
        map.transform.Find("BG_Brown").gameObject.SetActive(false);
        map.transform.Find("BG_Pass").gameObject.SetActive(false);
        map.transform.Find("BG_Lock").gameObject.SetActive(true);
        map.transform.Find("Lock").gameObject.SetActive(true);
        map.transform.Find("Star").gameObject.SetActive(false);
        map.transform.Find("StarYellow").gameObject.SetActive(false);
    }

    void LevelState1(Transform map)
    {
        map.transform.Find("BG_Brown").gameObject.SetActive(false);
        map.transform.Find("BG_Pass").gameObject.SetActive(true);
        map.transform.Find("BG_Lock").gameObject.SetActive(false);
        map.transform.Find("Lock").gameObject.SetActive(false);
        map.transform.Find("Star").gameObject.SetActive(true);
        map.transform.Find("StarYellow").gameObject.SetActive(false);
    }

    void LevelState2(Transform map)
    {
        map.transform.Find("BG_Brown").gameObject.SetActive(false);
        map.transform.Find("BG_Pass").gameObject.SetActive(true);
        map.transform.Find("BG_Lock").gameObject.SetActive(false);
        map.transform.Find("Lock").gameObject.SetActive(false);
        map.transform.Find("Star").gameObject.SetActive(false);
        map.transform.Find("StarYellow").gameObject.SetActive(true);
    }

   void SetBrownPanelEasy(int levelIndex)
    {
        if (!LevelData.isTutorialComplete) { return; }
        var map = easyModeUi.transform.Find($"easyLevel{levelIndex}");

        map.transform.Find("BG_Brown").gameObject.SetActive(true);
        map.transform.Find("BG_Pass").gameObject.SetActive(false);
        map.transform.Find("BG_Lock").gameObject.SetActive(false);
        map.transform.Find("Lock").gameObject.SetActive(false);
        map.transform.Find("Star").gameObject.SetActive(true);
        map.transform.Find("StarYellow").gameObject.SetActive(false);
    }
    void SetBrownPanelHard(int levelIndex) 
    { 
        if (!LevelData.isTutorialComplete) { return; }
        var map = hardModeUi.transform.Find($"hardLevel{levelIndex}");

        map.transform.Find("BG_Brown").gameObject.SetActive(true);
        map.transform.Find("BG_Pass").gameObject.SetActive(false);
        map.transform.Find("BG_Lock").gameObject.SetActive(false);
        map.transform.Find("Lock").gameObject.SetActive(false);
        map.transform.Find("Star").gameObject.SetActive(true);
        map.transform.Find("StarYellow").gameObject.SetActive(false);
    }

    void SetCompleteTutorial()
    {
        if (LevelData.isTutorialComplete || isHardmodePlayable)
        {
            var map = easyModeUi.transform.Find($"LevelTutorial");
            map.transform.Find("BG_Brown").gameObject.SetActive(false);
            map.transform.Find("BG_Pass").gameObject.SetActive(true);
        }
    }
    #endregion //

}
