using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    public GameObject popupBonus;
    public GameObject popupAlertBonusUsed;
    public GameObject popupAlertBonusNotFound;
    public GameObject popupAlertCoinNotEnought;
    public GameObject popupAlertBuyComplete;
    public InputField txtBonusCode;
    public Text txtCoin;

    public GameObject coinMode;
    public GameObject itemMode;

    public AudioClip fxClick;

    void Start()
    {
        txtCoin.text = Utils.coin.ToString();   
    }

    //Button Function
    public void OnClickHome() {
        Utils.soundManager.PlayFX(fxClick);
        SceneManager.LoadScene("MainMenuScene");
    }

    public void OnClickShop() {
        Utils.soundManager.PlayFX(fxClick);
        SceneManager.LoadScene("MapScene");
    }

    public void OnClickShowBonus() {
        Utils.soundManager.PlayFX(fxClick);
        popupBonus.SetActive(true);
    }

    public void OnClickCoinMode() {
        Utils.soundManager.PlayFX(fxClick);
        coinMode.SetActive(true);
        itemMode.SetActive(false);
    }

    public void OnClickItemMode() {
        Utils.soundManager.PlayFX(fxClick);
        coinMode.SetActive(false);
        itemMode.SetActive(true);
    }

    public void OnClickUseBonusCode() {
        Utils.soundManager.PlayFX(fxClick);
        if (txtBonusCode.text != "") {
            string used = PlayerPrefs.GetString("BonusUse");
            if (used.IndexOf(","+ txtBonusCode.text+",")>-1) {
                //Already Use
                popupAlertBonusUsed.SetActive(true);
            } else {
                //Not Use Check is Code Correct
                if (Utils.Bonus10.IndexOf(txtBonusCode.text) > -1) {
                    //Found 10
                    int val = Utils.GetCurrentEpoch()-PlayerPrefs.GetInt("BonusUse1");
                    if (val<1) {
                        popupAlertBonusNotFound.SetActive(true);
                        return;
                    }
                    PlayerPrefs.SetInt("BonusUse1", Utils.GetCurrentEpoch());

                    used += (txtBonusCode.text + ",");
                    PlayerPrefs.SetString("BonusUse",used);
                    Utils.coin += 10;
                    PlayerPrefs.SetInt("coin", Utils.coin);
                }else if (Utils.Bonus20.IndexOf(txtBonusCode.text) > -1) {
                    //Found 20
                    int val = Utils.GetCurrentEpoch() - PlayerPrefs.GetInt("BonusUse2");
                    if (val < 7) {
                        popupAlertBonusNotFound.SetActive(true);
                        return;
                    }
                    PlayerPrefs.SetInt("BonusUse2", Utils.GetCurrentEpoch());

                    used += txtBonusCode.text + ",";
                    PlayerPrefs.SetString("BonusUse", used);
                    Utils.coin += 20;
                    PlayerPrefs.SetInt("coin", Utils.coin);
                } else if (Utils.Bonus50.IndexOf(txtBonusCode.text) > -1) {
                    //Found 50
                    int val = Utils.GetCurrentEpoch() - PlayerPrefs.GetInt("BonusUse3");
                    if (val < 15) {
                        popupAlertBonusNotFound.SetActive(true);
                        return;
                    }
                    PlayerPrefs.SetInt("BonusUse3", Utils.GetCurrentEpoch());

                    used += txtBonusCode.text + ",";
                    PlayerPrefs.SetString("BonusUse", used);
                    Utils.coin += 50;
                    PlayerPrefs.SetInt("coin", Utils.coin);
                } else if (Utils.Bonus100.IndexOf(txtBonusCode.text) > -1) {
                    //Found 100
                    int val = Utils.GetCurrentEpoch() - PlayerPrefs.GetInt("BonusUse4");
                    if (val < 30) {
                        popupAlertBonusNotFound.SetActive(true);
                        return;
                    }
                    PlayerPrefs.SetInt("BonusUse4", Utils.GetCurrentEpoch());

                    used += txtBonusCode.text + ",";
                    PlayerPrefs.SetString("BonusUse", used);
                    Utils.coin += 100;
                    PlayerPrefs.SetInt("coin", Utils.coin);
                } else {
                    txtBonusCode.text = "";
                    popupAlertBonusNotFound.SetActive(true);
                    return;
                }
                txtBonusCode.text = "";
                txtCoin.text = Utils.coin.ToString();
                popupBonus.SetActive(false);

                Hashtable ht = new Hashtable();
                ht.Add("x", 1.2f);
                ht.Add("y", 1.2f);
                ht.Add("islocal", true);
                ht.Add("time", 0.5f);
                iTween.ScaleFrom(txtCoin.gameObject,ht);
            }
        }
    }

    public void OnClickBuyCoin(int id) {
        Utils.soundManager.PlayFX(fxClick);

    }

    public void OnClickBuyItem(int id) {
        Utils.soundManager.PlayFX(fxClick);
        print(Utils.coin+ "," +id);

        Hashtable ht = new Hashtable();
        ht.Add("x", 1.2f);
        ht.Add("y", 1.2f);
        ht.Add("islocal", true);
        ht.Add("time", 0.5f);
        switch (id) {
            case 1:
                if (Utils.coin >= 10) {
                    Utils.potion += 5;
                    Utils.coin -= 10;
                    PlayerPrefs.SetInt("potion", Utils.potion);
                    PlayerPrefs.SetInt("coin", Utils.coin);
                    PlayerPrefs.Save();
                    txtCoin.text = Utils.coin.ToString();
                    iTween.ScaleFrom(txtCoin.gameObject, ht);
                    popupAlertBuyComplete.SetActive(true);
                } else {
                    popupAlertCoinNotEnought.SetActive(true);
                }
                break;
            case 2:
                if (Utils.coin >= 30) {
                    Utils.potion += 20;
                    Utils.coin -= 30;
                    PlayerPrefs.SetInt("potion", Utils.potion);
                    PlayerPrefs.SetInt("coin", Utils.coin);
                    PlayerPrefs.Save();
                    txtCoin.text = Utils.coin.ToString();
                    iTween.ScaleFrom(txtCoin.gameObject, ht);
                    popupAlertBuyComplete.SetActive(true);
                } else {
                    popupAlertCoinNotEnought.SetActive(true);
                }
                break;
            case 3:
                if (Utils.coin >= 20) {
                    Utils.potion_barier += 10;
                    Utils.coin -= 20;
                    PlayerPrefs.SetInt("potionBarier", Utils.potion_barier);
                    PlayerPrefs.SetInt("coin", Utils.coin);
                    PlayerPrefs.Save();
                    txtCoin.text = Utils.coin.ToString();
                    iTween.ScaleFrom(txtCoin.gameObject, ht);
                    popupAlertBuyComplete.SetActive(true);
                } else {
                    popupAlertCoinNotEnought.SetActive(true);
                }
                break;
            case 4:
                if (Utils.coin >= 100) {
                    Utils.key += 5;
                    Utils.coin -= 100;
                    PlayerPrefs.SetInt("key", Utils.key);
                    PlayerPrefs.SetInt("coin", Utils.coin);
                    PlayerPrefs.Save();
                    txtCoin.text = Utils.coin.ToString();
                    iTween.ScaleFrom(txtCoin.gameObject, ht);
                    popupAlertBuyComplete.SetActive(true);
                } else {
                    popupAlertCoinNotEnought.SetActive(true);
                }
                break;
            case 5:
                if (Utils.coin >= 100) {
                    Utils.potion += 5;
                    Utils.key += 2;
                    Utils.coin -= 100;
                    Utils.isSkinUnlock = true;
                    PlayerPrefs.SetInt("key", Utils.key);
                    PlayerPrefs.SetInt("potion", Utils.potion);
                    PlayerPrefs.SetInt("coin", Utils.coin);
                    PlayerPrefs.SetInt("skin_unlock", 1);
                    PlayerPrefs.Save();
                    txtCoin.text = Utils.coin.ToString();
                    iTween.ScaleFrom(txtCoin.gameObject, ht);
                    popupAlertBuyComplete.SetActive(true);
                } else {
                    popupAlertCoinNotEnought.SetActive(true);
                }
                break;
            case 6:
                if (Utils.coin >= 300) {
                    Utils.potion += 50;
                    Utils.key += 10;
                    Utils.coin -= 300;
                    Utils.isSkinUnlock = true;
                    PlayerPrefs.SetInt("key", Utils.key);
                    PlayerPrefs.SetInt("potion", Utils.potion);
                    PlayerPrefs.SetInt("coin", Utils.coin);
                    PlayerPrefs.SetInt("skin_unlock", 1);
                    PlayerPrefs.Save();
                    txtCoin.text = Utils.coin.ToString();
                    iTween.ScaleFrom(txtCoin.gameObject, ht);
                    popupAlertBuyComplete.SetActive(true);
                } else {
                    popupAlertCoinNotEnought.SetActive(true);
                }
                break;
        }
    }

    public void OnClickHidePopup() {
        popupBonus.SetActive(false);
        popupAlertBonusUsed.SetActive(false);
        popupAlertBonusNotFound.SetActive(false);
        popupAlertCoinNotEnought.SetActive(false);
        popupAlertBuyComplete.SetActive(false);
    }
}
