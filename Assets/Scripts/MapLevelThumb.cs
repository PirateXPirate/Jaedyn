using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapLevelThumb : MonoBehaviour
{
    public Text txtLevel;
    public Image Star;
    public GameObject Lock;

    public Sprite spStarPass;

    public int level;
    public GameObject selectFlag;

    void Start()
    {
        selectFlag.SetActive(false);
        txtLevel.text = level.ToString();

        //For Hide/Show Image - Star Manage
        if (level == 0) {
            Star.gameObject.SetActive(false);
            Lock.SetActive(false);
            selectFlag.SetActive(true);
        }else {
            if (Utils.gotUnlockImage[level-1] == "true") {
                Star.gameObject.SetActive(true);
                Star.sprite = spStarPass;
            } else {
                Star.gameObject.SetActive(true);
            }
        }

        //For Unlock Level - Lock Manage
        if (Utils.levelPass >= level) {
            Lock.SetActive(false);
        } else {
            Lock.SetActive(true);
        }
    }

    public void SetShowSelection() {
        selectFlag.SetActive(true);
    }

    public void SetHideSelection() {
        selectFlag.SetActive(false);
    }
}
