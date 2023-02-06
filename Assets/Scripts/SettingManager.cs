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

    float valLoop = 0f;
    float valFX = 0f;

    void Start()
    {
        valLoop = Utils.volumeLoop / 100f;
        valFX = Utils.volumeFX / 100f;

        sbLoop.value = valLoop;
        sbFX.value = valFX;
    }

    public void OnChangeScrollBar(string type) {
        if (type == "loop") {
            valLoop = sbLoop.value;
            Utils.volumeLoop = valLoop * 100f;
            Utils.soundManager.loop.volume = valLoop;
        } else if (type == "fx") {
            valFX = sbFX.value;
            Utils.volumeFX = valFX * 100f;
            Utils.soundManager.fx.volume = valFX;
        }
    }

    //Button
    public void OnClickHome() {
        Utils.soundManager.PlayFX(fxClick);
        SceneManager.LoadScene("MainMenuScene");
    }

    public void OnClickSave() {
        PlayerPrefs.SetFloat("loop", Utils.volumeLoop);
        PlayerPrefs.SetFloat("fx", Utils.volumeLoop);

        Utils.soundManager.PlayFX(fxClick);
        SceneManager.LoadScene("MainMenuScene");
    }
}
