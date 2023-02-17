using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    public GameObject hardModeLockedPopUp;
    public GameObject levelLockedPopUp;
    public GameObject unLockedPopup;

    [SerializeField] Button closeHardModeLockedButton;
    [SerializeField] Button closeLevelLockedButton;
    [SerializeField] Button closeUnLockedButton;

    private void Start()
    {
        SetListenner();
    }

    void SetListenner()
    {
        closeHardModeLockedButton.onClick.AddListener(CloseAllPopup);
        closeLevelLockedButton.onClick.AddListener(CloseAllPopup);
        closeUnLockedButton.onClick.AddListener(CloseAllPopup);
    }

    public void CloseAllPopup()
    {
        hardModeLockedPopUp.SetActive(false);
        levelLockedPopUp.SetActive(false);
        unLockedPopup.SetActive(false);
    }

    public void OpenPopUp(GameObject popup)
    {
        CloseAllPopup();
        popup.SetActive(true);
    }

}
