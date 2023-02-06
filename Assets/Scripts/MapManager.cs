using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public Text txtLevelPass;
    public Text txtKey;
    public GameObject popupLock;
    public GameObject popupEndlessLock;

    public AudioClip loop;
    public AudioClip fxClick;
    public AudioClip fxMode;

    public GameObject[] mapList;

    void Start()
    {
        Utils.soundManager.PlayLoop(loop);
        Utils.soundManager.PlayFX(fxMode);
        if (Utils.levelPass == 0) {
            txtLevelPass.text = "Tutorial";
        } else {
            txtLevelPass.text = "Map " + Utils.levelPass + "/9";
        }
    }

    //Button
    public void OnClickHome() {
        Utils.soundManager.PlayFX(fxClick);
        SceneManager.LoadScene("MainMenuScene");
    }

    public void OnClickClosePopup() {
        Utils.soundManager.PlayFX(fxClick);
        popupLock.SetActive(false);
        popupEndlessLock.SetActive(false);
    }

    public void OnClickUnlock() {
        Utils.soundManager.PlayFX(fxClick);
        popupEndlessLock.SetActive(true);
    }

    public void OnClickEndless() {
        Utils.soundManager.PlayFX(fxClick);
        popupEndlessLock.SetActive(true);
    }

    public void OnClickLevel(int lv) {
        Utils.soundManager.PlayFX(fxClick);

        if (Utils.levelPass>=lv) {
            Utils.currentLevel = lv;
            for(int i = 0; i < 9; i++) {
                if (lv == i) {
                    mapList[i].GetComponent<MapLevelThumb>().SetShowSelection();
                } else {
                    mapList[i].GetComponent<MapLevelThumb>().SetHideSelection();
                }
            }
        } else {
            popupLock.SetActive(true);
        }
    }

    public void OnClickPlay() {
        Utils.soundManager.PlayFX(fxClick);

        if (Utils.currentLevel == 0) {
            SceneManager.LoadScene("StoryScene");
            //SceneManager.LoadScene("Level_Tutorial");
            return;
        }
        if (Utils.currentLevel <= 4) {
            if (Utils.levelPass >= Utils.currentLevel) {
                //Go To Game Scene
            }
        } else if (Utils.currentLevel > 4) {
            if (Utils.isUnlock) {
                if (Utils.levelPass >= Utils.currentLevel) {
                    //Go To Game Scene
                }
            }
        }
    }

    public void OnClickGoShop() {
        SceneManager.LoadScene("ShopScene");
    }
}
