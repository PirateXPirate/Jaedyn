using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setvolume : MonoBehaviour
{
    float currentVolume;
    public SettingManager settingManager;
    private void OnEnable()
    {
        settingManager.OnSaveSoundSetting += OnSaveSetting;
        if (Utils.soundManager)
        {
            currentVolume = Utils.soundManager.loop.volume;
            GetComponent<AudioSource>().volume = currentVolume;
        }
    }
    private void OnDisable()
    {
        settingManager.OnSaveSoundSetting -= OnSaveSetting;
    }
    private void OnSaveSetting()
    {
        currentVolume = Utils.soundManager.loop.volume;
        GetComponent<AudioSource>().volume = Utils.soundManager.fx.volume;
    }
}
