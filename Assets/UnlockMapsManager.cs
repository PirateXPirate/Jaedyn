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
    [SerializeField] Button unlockButton;
    int keyQuantity = 0;

    private void Awake()
    {
        uiManager = GetComponent<UIManager>();
        buttonFeedbacks = GetComponent<ButtonFeedbacks>();
        popupManager = GetComponent<PopupManager>();
        keyQuantity = PlayerPrefs.GetInt("keyQuantity", keyQuantity);
        
        unlockButton.onClick.AddListener(OnClickUnlockButton);
    }

    void OnClickUnlockButton()
    {
        if (keyQuantity <= 0) 
        {
            print("player has no key");
            //TODO popup player has no key
            return; 
        }

        if (levelIndex > CurrentUnlockedMap())
        {
            return;
        }

        LevelData.UnlockLevelStateData(levelIndex,mode);
        keyQuantity -= 1;
        PlayerPrefs.SetInt("keyQuantity", keyQuantity);
        LevelData.LoadLevelStateData();
        buttonFeedbacks.ResetAllSize();
        uiManager.SetUpUi();
        popupManager.CloseAllPopup();
    }
    int CurrentUnlockedMap()
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
        return count;
    }

    private void OnDestroy()
    {
        unlockButton.onClick.RemoveAllListeners();
    }
}
