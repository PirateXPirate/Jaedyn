using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public AudioClip fxClick;
    public Scrollbar sbLoop;
    public Scrollbar sbFX;

    public GameObject SettingPopup;

    float valLoop = 0f;
    float valFX = 0f;

     float tmpvalLoop;
     float tmpvalFX;
   

    public void OnChangeScrollBar(string type)
    {
        if (type == "loop")
        {
            valLoop = sbLoop.value;
            Utils.volumeLoop = valLoop * 100f;
            Utils.soundManager.loop.volume = valLoop;
        }
        else if (type == "fx")
        {
            valFX = sbFX.value;
            Utils.volumeFX = valFX * 100f;
            Utils.soundManager.fx.volume = valFX;
        }
    }

    public void LoadSetting()
    {
        valLoop = Utils.soundManager.loop.volume ;
        valFX = Utils.soundManager.fx.volume;

        tmpvalLoop = valLoop;
        tmpvalFX = valFX;

        sbLoop.value = valLoop;
        sbFX.value = valFX;
    }

    //Button
    public void OnClickHome()
    {
        if (SettingPopup)
            SettingPopup.SetActive(false);
        Utils.soundManager.PlayFX(fxClick);
        Utils.soundManager.fx.volume = tmpvalFX;
        Utils.soundManager.loop.volume = tmpvalLoop;
        //  SceneManager.LoadScene("MainMenuScene");
    }

    public void OnClickSave()
    {
        PlayerPrefs.SetFloat("loop", Utils.soundManager.loop.volume);
        PlayerPrefs.SetFloat("fx", Utils.soundManager.fx.volume);
        tmpvalFX = Utils.soundManager.fx.volume;
        tmpvalLoop = Utils.soundManager.loop.volume;
        Utils.soundManager.PlayFX(fxClick);
        // SceneManager.LoadScene("MainMenuScene");
    }
}
