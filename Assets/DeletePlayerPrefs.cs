using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeletePlayerPrefs : MonoBehaviour
{
    [SerializeField] Button deletePlayerPrefsButton;
    [SerializeField] Button unlockedMapsButton;

    [SerializeField] UiManager uiManager;

    [SerializeField] Button[] testUnlockMapEasy;
    private void Awake()
    {
        deletePlayerPrefsButton.onClick.RemoveAllListeners();
        deletePlayerPrefsButton.onClick.AddListener(OnClickDeletePlayerPrefs);

        unlockedMapsButton.onClick.RemoveAllListeners();
        unlockedMapsButton.onClick.AddListener(OnClickUnlockedAllMaps);

        LevelData.TutorialComplete(true);

        for (int i = 0; i < testUnlockMapEasy.Length; i++)
        {
            var index = 0;
            index += i;
            testUnlockMapEasy[i].onClick.AddListener(() => TestUnlockMap(index));
        }
    }

    void TestUnlockMap(int levelIndex)
    {
        LevelData.mode = LevelData.Mode.easy;
        LevelData.levelState = LevelData.LevelState.PerfectCleared;
        LevelData.SaveLevelStateData(levelIndex + 1);
        LevelData.LoadLevelStateData();
        RefreshUi();
    }

    void OnClickDeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
        LevelData.LoadLevelStateData();
        RefreshUi();
    }

    void OnClickUnlockedAllMaps()
    {
        for (int i = 0; i < LevelData.easyModeState.Length; i++)
        {
            LevelData.easyModeState[i] = 3;
            LevelData.hardModeState[i] = 3;
            LevelData.isTutorialComplete = true;
            LevelData.isTutorialCompleteWithPicture = true;
        }
        RefreshUi();
    }

    void RefreshUi()
    {
        uiManager.SetUpUi();
        uiManager.SetCompleteTutorial();
    }
}
