using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelData
{
    const int TOTAL_LEVEL = 9;

    public static bool isTutorialComplete;
    public static bool isTutorialCompleteWithPicture;
    public static int[] easyModeState = new int[TOTAL_LEVEL];
    public static int[] hardModeState = new int[TOTAL_LEVEL];

    public enum Mode { easy, hard }
    public static Mode mode;
    public enum LevelState { Locked = 0, ClearWithOutPicture = 1, PerfectCleared = 2}
    public static LevelState levelState;


    public static void TutorialComplete(bool isPictureTaken)
    {
        if (isPictureTaken == true)
        {
            PlayerPrefs.SetInt("isTutorialComplete", 1);
            PlayerPrefs.SetInt("isTutorialCompleteWithPicture", 1);
        }
        else
        {
            PlayerPrefs.SetInt("isTutorialComplete", 1);
        }
    }
    
    /// <summary>
    /// Use this method when player completed any level to save state of that level or Use this method when player use key to unlock any level
    /// </summary>
    public static void SaveLevelStateData(int levelIndex)
    {
        if (levelIndex <= 0 || levelIndex > TOTAL_LEVEL) { Debug.LogError($"levelIndex must be between 1 and {TOTAL_LEVEL}"); return; }
        if (levelState == LevelState.Locked) { Debug.LogError($"Wrong LevelState input"); return; }

        switch (mode)
        {
            case Mode.easy:
                PlayerPrefs.SetInt($"easyLevel{levelIndex}State", (int)levelState);
                break;
            case Mode.hard:
                PlayerPrefs.SetInt($"hardLevel{levelIndex}State", (int)levelState);
                break;
        }
    }


    public static void LoadLevelStateData()
    {
        if (PlayerPrefs.HasKey("isTutorialComplete"))
        {
            isTutorialComplete = PlayerPrefs.GetInt("isTutorialComplete") == 1 ? true : false;
            isTutorialCompleteWithPicture = PlayerPrefs.GetInt("isTutorialCompleteWithPicture") == 1 ? true : false;
        }
           

        for (int i = 0; i < easyModeState.Length; i++)
        {
            if (PlayerPrefs.HasKey($"easyLevel{i+1}State"))
            {
                easyModeState[i] = PlayerPrefs.GetInt($"easyLevel{i+1}State", 0);
            }
            else break;
        }

        for (int i = 0; i < hardModeState.Length; i++)
        {
            if (PlayerPrefs.HasKey($"hardLevel{i+1}State"))
            {
                hardModeState[i] = PlayerPrefs.GetInt($"hardLevel{i+1}State", 0);
            }
            else break;
        }

    }
}
