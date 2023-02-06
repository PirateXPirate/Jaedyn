using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject Head;
    public GameObject buttomMenu;

    public AudioClip loop;
    public AudioClip fxClick;
    public AudioClip fxSpeak;

    public void Start() {
        Utils.soundManager.PlayLoop(loop);
        Utils.soundManager.PlayFX(fxSpeak);

        Hashtable ht = new Hashtable();
        ht.Add("y", 2000);
        ht.Add("time", 0.5f);
        ht.Add("delay", 0.5f);
        ht.Add("easetype", iTween.EaseType.easeOutBack);
        iTween.MoveFrom(Head, ht);

        ht.Clear();
        ht.Add("y", -200);
        ht.Add("time", 0.5f);
        ht.Add("delay", 1f);
        ht.Add("easetype", iTween.EaseType.easeOutBack);
        iTween.MoveFrom(buttomMenu, ht);
    }

    public void OnClickPlayTutorial() {
        //if (Utils.levelPass == -1) {
        //    SceneManager.LoadScene("Level_Tutorial");
        //} else {
        //    SceneManager.LoadScene("MapScene");
        //}
        Utils.soundManager.PlayFX(fxClick);
        SceneManager.LoadScene("MapScene");
    }

    public void OnClickGallery() {
        Utils.soundManager.PlayFX(fxClick);
        SceneManager.LoadScene("GalleryScene");
    }

    public void OnClickSetting() {
        Utils.soundManager.PlayFX(fxClick);
        SceneManager.LoadScene("SettingScene");
    }
}
