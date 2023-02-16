using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelData
{
    const int TOTAL_LEVEL = 9;

    public static bool isTutorialComplete;
    public static int[] easyModeState = new int[TOTAL_LEVEL];
    public static int[] hardModeState = new int[TOTAL_LEVEL];

    public static void TutorialComplete()
    {
        PlayerPrefs.SetInt("isTutorialComplete", 1);
    }

    /// <param name="mode">easy or hard</param>
    /// <param name="levelState">0 = locked, 1 = cleared without picture, 2 = cleared with picture(Perfect clear)</param>
    public static void SaveLevelStateData(string mode, int levelIndex, int levelState)
    {
        if (levelIndex <= 0) { Debug.LogError("levelIndex must be more than 0"); return; }

        if (mode == "easy" || mode == "hard")
        {
            PlayerPrefs.SetInt($"{mode}Level{levelIndex}State", levelState);
        }
        else
        {
            Debug.LogError("mode parameter must be \"easy\" or \"hard\"");
            return;
        }
    }

    public static void LoadLevelStateData()
    {
        if (PlayerPrefs.HasKey("isTutorialComplete"))
            isTutorialComplete = PlayerPrefs.GetInt("isTutorialComplete") == 1 ? true : false;

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
