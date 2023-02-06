using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GalleryManager : MonoBehaviour
{
    public GameObject pop;
    public GameObject[] imgs;
    public GameObject[] imgsLock;
    public AudioClip fxClick;
    public AudioClip fxMode;

    private int selectID;

    void Start()
    {
        Utils.soundManager.PlayFX(fxMode);
        for (int i = 0; i < imgsLock.Length; i++) {
            if (Utils.gotUnlockImage[i] == "true") {
                imgsLock[i].SetActive(false);
            }
        }
    }

    //Button
    public void OnClickHome() {
        Utils.soundManager.PlayFX(fxClick);
        SceneManager.LoadScene("MainMenuScene");
    }

    public void OnShowImage(int id) {
        Utils.soundManager.PlayFX(fxClick);

        if (Utils.gotUnlockImage[id]=="false") return;
        selectID = id;
        pop.SetActive(true);
        imgs[id].SetActive(true);
    }

    public void OnCloseImage() {
        Utils.soundManager.PlayFX(fxClick);
        pop.SetActive(false);
        imgs[selectID].SetActive(false);
    }
}
