using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashManager : MonoBehaviour
{
    public GameObject txtLoad;

    void Start()
    {
        Utils.udid = SystemInfo.deviceUniqueIdentifier;

        //Check is unlock Level
        //PlayerPrefs.DeleteAll();//Test
        if (PlayerPrefs.HasKey("unlock_level")) {
            string val = PlayerPrefs.GetString("unlock_level");
            Utils.gotUnlockLevel = val.Split(","[0]);
        } else {
            PlayerPrefs.SetString("unlock_level", "false,false,false,false,false,false,false,false,false");
            string val = PlayerPrefs.GetString("unlock_level");
            Utils.gotUnlockLevel = val.Split(","[0]);
        }

        //Check Level Pass
        for (int i = 0; i < 9; i++) {
            if (Utils.gotUnlockLevel[i] == "true") {
                Utils.levelPass = i+1;
            }
        }

        //Check is unlock Gallery
        if (PlayerPrefs.HasKey("unlock_gallery")) {
            string val = PlayerPrefs.GetString("unlock_gallery") ;
            Utils.gotUnlockImage = val.Split(","[0]);
        } else {
            PlayerPrefs.SetString("unlock_gallery", "true,true,false,false,false,false,false,false,false");
            string val = PlayerPrefs.GetString("unlock_gallery");
            Utils.gotUnlockImage = val.Split(","[0]);
        }

        //Check is unlock
        if (PlayerPrefs.HasKey("unlock")) {
            if (PlayerPrefs.GetInt("unlock") == 1) {
                Utils.isUnlock = true;
            } else {
                Utils.isUnlock = false;
            }
        } else {
            Utils.isUnlock = false;
            PlayerPrefs.SetInt("unlock", 1);
        }

        //Check is unlock
        if (PlayerPrefs.HasKey("endless_mode_unlock")) {
            if (PlayerPrefs.GetInt("endless_mode_unlock") == 1) {
                Utils.isEndlessUnlock = true;
            } else {
                Utils.isEndlessUnlock = false;
            }
        } else {
            Utils.isEndlessUnlock = false;
            PlayerPrefs.SetInt("isEndlessUnlock", 0);
        }

        //Check is skin unlock
        if (PlayerPrefs.HasKey("skin_unlock")) {
            if (PlayerPrefs.GetInt("skin_unlock") == 1) {
                Utils.isSkinUnlock = true;
            } else {
                Utils.isSkinUnlock = false;
            }
        } else {
            Utils.isSkinUnlock = false;
            PlayerPrefs.SetInt("skin_unlock", 0);
        }

        //Check Key,Potionn,Potion Barier 
        if (PlayerPrefs.HasKey("key")) {
            Utils.key = PlayerPrefs.GetInt("key");
        } else {
            Utils.key = 0;
            PlayerPrefs.SetInt("key", 0);
        }

        if (PlayerPrefs.HasKey("potion")) {
            Utils.potion = PlayerPrefs.GetInt("potion");
        } else {
            Utils.potion = 0;
            PlayerPrefs.SetInt("potion", 0);
        }

        if (PlayerPrefs.HasKey("potionBarier")) {
            Utils.potion_barier = PlayerPrefs.GetInt("potionBarier");
        } else {
            Utils.potion_barier = 0;
            PlayerPrefs.SetInt("potionBarier", 0);
        }

        //Check Loop
        if (PlayerPrefs.HasKey("loop")) {
            Utils.volumeLoop = PlayerPrefs.GetFloat("loop");
        } else {
            Utils.volumeLoop = 100f;
            PlayerPrefs.SetFloat("loop", 100f);
        }

        //Check FX
        if (PlayerPrefs.HasKey("fx")) {
            Utils.volumeFX = PlayerPrefs.GetFloat("fx");
        } else {
            Utils.volumeFX = 100f;
            PlayerPrefs.SetFloat("fx", 100f);
        }

        //Bonus Use
        if (PlayerPrefs.HasKey("BonusUse1")) {
            
        } else {
            Utils.coin = 0;
            PlayerPrefs.SetInt("BonusUse1", 0);
            PlayerPrefs.SetInt("BonusUse2", 0); 
            PlayerPrefs.SetInt("BonusUse3", 0);
            PlayerPrefs.SetInt("BonusUse4", 0);
            PlayerPrefs.SetString("BonusUse", ",");
        }

        //Coin
        if (PlayerPrefs.HasKey("coin")) {
            Utils.coin = PlayerPrefs.GetInt("coin");
        } else {
            Utils.coin = 0;
            PlayerPrefs.SetInt("coin", 0);
        }

        InvokeRepeating("BlinkText", 0.5f, 0.5f);
        Invoke("GoMainMenu", 5f);
    }

    void BlinkText() {
        if (txtLoad.activeSelf) {
            txtLoad.SetActive(false);
        } else{
            txtLoad.SetActive(true);
        }
    }

    void GoMainMenu() {
        CancelInvoke("BlinkText");
        SceneManager.LoadScene("MainMenuScene");
    }
}
