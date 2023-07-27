using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    public GameObject hardModeLockedPopUp;
    public GameObject levelLockedPopUp;
    public GameObject levelUnlockedPopup;

    [SerializeField] Button closeHardModeLockedButton;
    [SerializeField] Button[] closeLevelLockedButtons;
    [SerializeField] Button[] closeLevelUnLockedButtons;

    private void Start()
    {
        SetListenner();
    }

    void SetListenner()
    {
        closeHardModeLockedButton.onClick.AddListener(CloseAllPopup);
        
        foreach (var button in closeLevelLockedButtons)
        {
            button.onClick.AddListener(CloseAllPopup);
        }

        foreach (var button in closeLevelUnLockedButtons)
        {
            button.onClick.AddListener(CloseAllPopup);
        }
    }

    public void CloseAllPopup()
    {
        hardModeLockedPopUp.SetActive(false);
        levelLockedPopUp.SetActive(false);
        levelUnlockedPopup.SetActive(false);
    }

    public void OpenPopUp(GameObject popup)
    {
        CloseAllPopup();
        popup.SetActive(true);
    }

    private void OnDestroy()
    {
        closeHardModeLockedButton.onClick.RemoveAllListeners();
        foreach (var button in closeLevelLockedButtons)
        {
            button.onClick.RemoveAllListeners();
        }

        foreach (var button in closeLevelUnLockedButtons)
        {
            button.onClick.RemoveAllListeners();
        }
    }

}
