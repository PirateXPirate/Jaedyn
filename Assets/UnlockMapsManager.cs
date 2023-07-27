using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnlockMapsManager : MonoBehaviour
{
    UIManager uiManager;
    ButtonFeedbacks buttonFeedbacks;
    PopupManager popupManager;

    public int levelIndex;
    public string mode;
    int keyQuantity = 0;

    private void Awake()
    {
        uiManager = GetComponent<UIManager>();
        buttonFeedbacks = GetComponent<ButtonFeedbacks>();
        popupManager = GetComponent<PopupManager>();
        keyQuantity = PlayerPrefs.GetInt("keyQuantity", keyQuantity);
    }

    public void OnClickLevelLockedButton()
    {
        if (levelIndex == getCurrentUnlockedMap())
        {
            if (keyQuantity <= 0)
            {
                popupManager.OpenPopUp(popupManager.levelLockedPopUp);
                uiManager.SetUpUi();
                return;
            }

            LevelData.UnlockLevelStateData(levelIndex, mode);
            keyQuantity -= 1;
            PlayerPrefs.SetInt("keyQuantity", keyQuantity);
            popupManager.OpenPopUp(popupManager.levelUnlockedPopup);
            buttonFeedbacks.ResetAllSize();
            uiManager.SetUpUi();
        }
    }
    int getCurrentUnlockedMap()
    {
        var count = 1;

        if(mode == "easy")
        {
            for (int i = 0; i < LevelData.easyModeState.Length; i++)
            {
                if (LevelData.easyModeState[i] != 0)
                {
                    count++;
                }

            }
        }

        if (mode == "hard")
        {
            for (int i = 0; i < LevelData.easyModeState.Length; i++)
            {
                if (LevelData.hardModeState[i] != 0)
                {
                    count++;
                }
            }
        }
        Debug.Log("Count == " + count);
        return count;
    }
}
