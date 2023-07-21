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
    public enum LevelState { Locked = 0, ClearWithOutPicture = 1, PerfectCleared = 2, UnLocked = 3}
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

        PlayerPrefs.SetInt($"easyLevel1State", 3);
        PlayerPrefs.SetInt($"hardLevel1State", 3);
    }
    
    /// <summary>
    /// Use this method when player completed any level to save state of that level
    /// </summary>
    public static void SaveLevelStateData(int levelIndex)
    {
        if (levelIndex <= 0 || levelIndex > TOTAL_LEVEL) { Debug.LogError($"levelIndex must be between 1 and {TOTAL_LEVEL}"); return; }
        //if (levelState == LevelState.Locked) { Debug.LogError($"Wrong LevelState input"); return; }

        switch (mode)
        {
            case Mode.easy:
                PlayerPrefs.SetInt($"easyLevel{levelIndex}State", (int)levelState);

                if (isNextLevelHasLocked(levelIndex,"easy"))
                {
                    PlayerPrefs.SetInt($"easyLevel{levelIndex + 1}State", 3);
                }
                break;
            case Mode.hard:
                PlayerPrefs.SetInt($"hardLevel{levelIndex}State", (int)levelState);

                if (isNextLevelHasLocked(levelIndex,"hard"))
                {
                    PlayerPrefs.SetInt($"hardLevel{levelIndex + 1}State", 3);
                }
                break;
        }

        bool isNextLevelHasLocked(int levelIndex, string mode)
        {
            if (levelIndex >= TOTAL_LEVEL)
            {
                return false;
            }

            if (PlayerPrefs.GetInt($"{mode}Level{levelIndex + 1}State") == 0)
            {
                return true;
            }

            return false;
        }
    }

    /// <summary>
    /// Use this method when player is unlocking by Key (from store)
    /// </summary>
    public static void UnlockLevelStateData(int levelIndex,string mode)
    {
        PlayerPrefs.SetInt($"{mode}Level{levelIndex}State", 3);
    }


    public static void LoadLevelStateData()
    {
        if (PlayerPrefs.HasKey("isTutorialComplete"))
        {
            isTutorialComplete = PlayerPrefs.GetInt("isTutorialComplete") == 1 ? true : false;
            isTutorialCompleteWithPicture = PlayerPrefs.GetInt("isTutorialCompleteWithPicture") == 1 ? true : false;
        }
           

        for (int i = 0; i < TOTAL_LEVEL; i++)
        {
            easyModeState[i] = PlayerPrefs.GetInt($"easyLevel{i+1}State", 0);
            hardModeState[i] = PlayerPrefs.GetInt($"hardLevel{i+1}State", 0);
        }
    }

}