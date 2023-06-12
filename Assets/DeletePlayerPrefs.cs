using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeletePlayerPrefs : MonoBehaviour
{
    [SerializeField] Button deletePlayerPrefsButton;
    [SerializeField] Button unlockedMapsButton;

    [SerializeField] UIManager uiManager;
    private void Awake()
    {
        deletePlayerPrefsButton.onClick.RemoveAllListeners();
        deletePlayerPrefsButton.onClick.AddListener(OnClickDeletePlayerPrefs);

        unlockedMapsButton.onClick.RemoveAllListeners();
        unlockedMapsButton.onClick.AddListener(OnClickUnlockedAllMaps);
    }

    void OnClickDeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        for (int i = 0; i < LevelData.easyModeState.Length; i++)
        {
            LevelData.easyModeState[i] = 0;
            LevelData.hardModeState[i] = 0;

            uiManager.SetActiveSymbolEasy(0, i + 1);
            uiManager.SetActiveSymbolHard(0, i + 1);
        }
        RefreshUi();
    }

    void OnClickUnlockedAllMaps()
    {
        for (int i = 0; i < LevelData.easyModeState.Length; i++)
        {
            LevelData.easyModeState[i] = 2;
            LevelData.hardModeState[i] = 2;
        }
        RefreshUi();
    }

    void RefreshUi()
    {
        uiManager.SetUpUi();
        uiManager.SetCompleteTutorial();
    }
}
