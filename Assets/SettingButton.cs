using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingButton : MonoBehaviour
{
    [SerializeField] GameObject settingPopup;
    [SerializeField] private GameObject controlUi;
    [SerializeField] private SettingManager settingManager;
    void Start()
    {
        
    }

    public void OnClickSetting()
    {
        controlUi.SetActive(false);
        settingPopup.SetActive(true);
        LevelManager.Instance.Players[0].LinkedInputManager.InputDetectionActive = false;
        LevelManager.Instance.Players[0].GetComponent<CharacterMovement>().enabled = false;
        LevelManager.Instance.Players[0].GetComponent<TopDownController3D>().enabled = false;
        settingManager.LoadSetting();
    }

    public void OnClickBackSetting()
    {
        settingPopup.SetActive(false);
        controlUi.SetActive(true);
        LevelManager.Instance.Players[0].LinkedInputManager.InputDetectionActive = true;
        LevelManager.Instance.Players[0].GetComponent<CharacterMovement>().enabled = true;
        LevelManager.Instance.Players[0].GetComponent<TopDownController3D>().enabled = true;
        settingManager.OnClickHome();
    }
}
