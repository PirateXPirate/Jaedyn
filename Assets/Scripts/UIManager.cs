using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using DG.Tweening;
using TMPro;
using System.Collections.Generic;

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

    [SerializeField] private TextMeshProUGUI keyQuantityText;
    public int keyQuantity = 0;

    public bool isHardmodePlayable;
    public string sceneToPlay = "";

    List<string> UnlockHardmodeCodeList = new List<string>();

    [SerializeField] private TextMeshProUGUI inputUnlockCodeTextField;
    [SerializeField] private Button unlcokCodeButton;

    void Awake()
    {
        LevelData.LoadLevelStateData();
        ButtonFeedBacks = gameObject.GetComponent<ButtonFeedbacks>();
        PopupManager = gameObject.GetComponent<PopupManager>();
        SetButtonListener();

        isHardmodePlayable = PlayerPrefsIntToBool("isHardmodePlayable");

        List<Dictionary<string, object>> dataUnlockHardmode = CSVReader.Read("Unlock");
        for (int i = 0; i < dataUnlockHardmode.Count; i++)
        {

            UnlockHardmodeCodeList.Add(dataUnlockHardmode[i]["code"].ToString());
         //   Debug.Log(dataUnlockHardmode[i]["code"]);
        }
        unlcokCodeButton.onClick.AddListener(OnClickUnlockHard);
    }

    private void OnClickUnlockHard()
    {
        string codeWithoutLastLetter = inputUnlockCodeTextField.text.Substring(0, inputUnlockCodeTextField.text.Length - 1);
        if (UnlockHardmodeCodeList.Contains(codeWithoutLastLetter))
        {
            isHardmodePlayable = true;
            PlayerPrefs.SetInt("isHardmodePlayable",1);
            unlcokCodeButton.transform.parent.transform.parent.gameObject.SetActive(false);

        }
      
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
        SceneManager.LoadScene("ShopScene");
    }

    public void GoToScene1() => SceneManager.LoadScene("EasyModeLevel1Scene");

   
    public void SetUpUi()
    {
        LevelData.LoadLevelStateData();
        //setup UI according to data
        for (int i = 0; i < LevelData.easyModeState.Length; i++)
        {
            SetActiveSymbolEasy(LevelData.easyModeState[i], i + 1);
        }

        for (int i = 0; i < LevelData.hardModeState.Length; i++)
        {
            SetActiveSymbolHard(LevelData.hardModeState[i], i + 1);
        }

        LoadingKey();
    }
    public void SetActiveSymbolEasy(int levelState, int levelIndex)
    {
        var map = easyModeUi.transform.Find($"easyLevel{levelIndex}");

        switch (levelState)
        {
            case 0:
                LevelState0Locked(map);
                break;
            case 1:
                LevelState1ClearedWithoutPicture(map);
                break;
            case 2:
                LevelState2PerfectCleared(map);
                break;
            case 3:
                LevelState3Unlocked(map);
                print("MAP IS  "+map);
                break;
        }
    }
    public void SetActiveSymbolHard(int levelState, int levelIndex)
    {
        var map = hardModeUi.transform.Find($"hardLevel{levelIndex}");

        switch (levelState)
        {
            case 0:
                LevelState0Locked(map);
                break;
            case 1:
                LevelState1ClearedWithoutPicture(map);
                break;
            case 2:
                LevelState2PerfectCleared(map);
                break;            
            case 3:
                LevelState3Unlocked(map);
                break;
        }
    }
    void LoadingKey()
    {
        keyQuantity = PlayerPrefs.GetInt("keyQuantity", 0);
        keyQuantityText.text = $"{keyQuantity} Key";
    }

    #region -SetActive UI-
    public void CloseAllSymbol(Transform map)
    {
        map.transform.Find("BG_Brown")?.gameObject.SetActive(false);
        map.transform.Find("BG_Pass")?.gameObject.SetActive(false);
        map.transform.Find("BG_Lock")?.gameObject.SetActive(false);
        map.transform.Find("Lock")?.gameObject.SetActive(false);
        map.transform.Find("Star")?.gameObject.SetActive(false);
        map.transform.Find("StarYellow")?.gameObject.SetActive(false);
    }
    void LevelState0Locked(Transform map)
    {
        CloseAllSymbol(map);
        map.transform.Find("BG_Lock")?.gameObject.SetActive(true);
        map.transform.Find("Lock")?.gameObject.SetActive(true);
    }

    void LevelState1ClearedWithoutPicture(Transform map)
    {
        CloseAllSymbol(map);
        map.transform.Find("BG_Pass")?.gameObject.SetActive(true);
        map.transform.Find("Star")?.gameObject.SetActive(true);
    }

    void LevelState2PerfectCleared(Transform map)
    {
        CloseAllSymbol(map);
        map.transform.Find("BG_Pass")?.gameObject.SetActive(true);
        map.transform.Find("StarYellow")?.gameObject.SetActive(true);
    }

   void LevelState3Unlocked(Transform map)
    {
        CloseAllSymbol(map);
        map.transform.Find("BG_Brown")?.gameObject.SetActive(true);
        map.transform.Find("Star")?.gameObject.SetActive(true);
    }

    public void SetCompleteTutorial()
    {
        var map = easyModeUi.transform.Find($"LevelTutorial");
        CloseAllSymbol(map);
        map.transform.Find("BG_Brown")?.gameObject.SetActive(true);
        map.transform.Find("Star")?.gameObject.SetActive(true);

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

    public bool PlayerPrefsIntToBool(string key)
    {
        int intValue = PlayerPrefs.GetInt(key);
        return intValue != 0;
    }

}
