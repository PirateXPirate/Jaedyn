using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[Serializable]
public class BonusButton
{
    public int index;
    public Button bonusButton;
    public int coinUsed;
}
public class NewShopManager : MonoBehaviour
{
    public Button HomeButton;
    public Button BackButton;
    public Button RedeemButton;

    public Button OkButton;
    public TMP_Text redeemCodeText;

    int currentCoin;
    public TextMeshProUGUI CoinText;
    public AudioClip fxClick;

    public BonusButton[] BonusList;

    public GameObject RedeemPopup;
    public GameObject AlreadyBuyPopup;
    public GameObject WrongCodePopup;
    public GameObject NotEnoughCoinPopup;
    public GameObject BuyCompletePopup;

    public Button[] CloseButtonList;


    bool bonus1Bought = false;
    bool bonus2Bought = false;
    bool bonus3Bought = false;
    bool bonus4Bought = false;
    bool bonus5Bought = false;
    bool bonus6Bought = false;

    int hpPotionQuantity;
    int resistancePotionQuantity;

    int keyQuantity;

    List<string> Code50List = new List<string>();
    List<string> Code100List = new List<string>();
    List<string> Code300List = new List<string>();

    List<string> UsedCodeList = new List<string>();
    void Start()
    {
        UsedCodeList =  LoadList("UsedCodeList");
      
        List <Dictionary<string, object>> data50 = CSVReader.Read("50Coin");
        List<Dictionary<string, object>> data100 = CSVReader.Read("100Coin");
        List<Dictionary<string, object>> data300 = CSVReader.Read("300Coin");

        for (int i = 0; i < data50.Count; i++)
        {

            Code50List.Add(data50[i]["Code"].ToString());
        }
        for (int i = 0; i < data100.Count; i++)
        {

            Code50List.Add(data100[i]["Code"].ToString());
        }
        for (int i = 0; i < data300.Count; i++)
        {

            Code50List.Add(data300[i]["Code"].ToString());
        }

        // bonus1Bought = PlayerPrefsIntToBool("bonus1Bought");
        // bonus2Bought = PlayerPrefsIntToBool("bonus2Bought");
        // bonus3Bought = PlayerPrefsIntToBool("bonus3Bought");
        //  bonus4Bought = PlayerPrefsIntToBool("bonus4Bought");
        //  bonus5Bought = PlayerPrefsIntToBool("bonus5Bought");
        //   bonus6Bought = PlayerPrefsIntToBool("bonus6Bought");

        //  BonusList[0].bonusButton.gameObject.SetActive(!bonus1Bought);
        //  BonusList[1].bonusButton.gameObject.SetActive(!bonus2Bought);
        //  BonusList[2].bonusButton.gameObject.SetActive(!bonus3Bought);
        //  BonusList[3].bonusButton.gameObject.SetActive(!bonus4Bought);
        //  BonusList[4].bonusButton.gameObject.SetActive(!bonus5Bought);
        //  BonusList[5].bonusButton.gameObject.SetActive(!bonus6Bought);

        hpPotionQuantity = PlayerPrefs.GetInt("hpPotionQuantity", 0);
        resistancePotionQuantity = PlayerPrefs.GetInt("resistancePotionQuantity", 0);

        keyQuantity = PlayerPrefs.GetInt("keyQuantity", 0);

        currentCoin = PlayerPrefs.GetInt("CoinQuantity", 0);
        CoinText.text = currentCoin.ToString();

        HomeButton.onClick.AddListener(OnClickHome);
        BackButton.onClick.AddListener(OnClickBack);
        RedeemButton.onClick.AddListener(OnClickRedeem);
        OkButton.onClick.AddListener(OnClickOkRedeemCode);

        foreach (var closeButton in CloseButtonList)
        {
            closeButton.onClick.AddListener(ClosePopup);
        }


        foreach (var bonusButton in BonusList)
        {
            bonusButton.bonusButton.onClick.AddListener(delegate { OnclickBonus(bonusButton); });
        }
    }

    private void OnClickOkRedeemCode()
    {
        if (UsedCodeList.Contains(redeemCodeText.text))
        {
            AlreadyBuyPopup.SetActive(true);
            redeemCodeText.text = "";
            return;

        }

        string codeWithoutLastLetter = redeemCodeText.text.Substring(0, redeemCodeText.text.Length - 1);

        if (Code50List.Contains(codeWithoutLastLetter))
        {
            currentCoin += 50;


        }
        else if (Code100List.Contains(codeWithoutLastLetter))
        {
            currentCoin += 100;
        }
        else if (Code300List.Contains(codeWithoutLastLetter))
        {
            currentCoin += 300;
        }
        else
        {
            WrongCodePopup.SetActive(true);
            return;
        }
        UsedCodeList.Add(redeemCodeText.text);
        SaveList(UsedCodeList, "UsedCodeList");
        RedeemPopup.SetActive(false);
        redeemCodeText.text = "";
        PlayerPrefs.SetInt("CoinQuantity", currentCoin);
        PlayerPrefs.Save();
        CoinText.text = currentCoin.ToString();
      
    }

    private void OnClickRedeem()
    {

        RedeemPopup.SetActive(true);
    }

    private void ClosePopup()
    {
        RedeemPopup.SetActive(false);
        AlreadyBuyPopup.SetActive(false);
        WrongCodePopup.SetActive(false);
        NotEnoughCoinPopup.SetActive(false);
        BuyCompletePopup.SetActive(false);
        BuyCompletePopup.SetActive(false);
    }

    private void OnclickBonus(BonusButton bonusButton)
    {
        // hpPotionQuantity = PlayerPrefs.GetInt("hpPotionQuantity");
        // resistancePotionQuantity = PlayerPrefs.GetInt("resistancePotionQuantity");

        if (currentCoin >= bonusButton.coinUsed)
        {
            currentCoin -= bonusButton.coinUsed;
            CoinText.text = currentCoin.ToString();
            PlayerPrefs.SetInt("CoinQuantity", currentCoin);
            PlayerPrefs.Save();

            BuyCompletePopup.SetActive(true);

            switch (bonusButton.index)
            {
                case 0:
                    hpPotionQuantity += 2;
                    PlayerPrefs.SetInt("hpPotionQuantity", hpPotionQuantity);
                    break;
                case 1:
                    hpPotionQuantity += 2;
                    resistancePotionQuantity += 2;
                    PlayerPrefs.SetInt("hpPotionQuantity", hpPotionQuantity);
                    PlayerPrefs.SetInt("resistancePotionQuantity", resistancePotionQuantity);
                    break;
                case 2:
                    hpPotionQuantity += 5;
                    resistancePotionQuantity += 5;
                    PlayerPrefs.SetInt("hpPotionQuantity", hpPotionQuantity);
                    PlayerPrefs.SetInt("resistancePotionQuantity", resistancePotionQuantity);
                    break;
                case 3:
                    hpPotionQuantity += 2;
                    resistancePotionQuantity += 2;
                    keyQuantity += 2;
                    PlayerPrefs.SetInt("hpPotionQuantity", hpPotionQuantity);
                    PlayerPrefs.SetInt("resistancePotionQuantity", resistancePotionQuantity);
                    PlayerPrefs.SetInt("keyQuantity", keyQuantity);
                    break;
                case 4:
                    hpPotionQuantity += 4;
                    resistancePotionQuantity += 2;
                    PlayerPrefs.SetInt("hpPotionQuantity", hpPotionQuantity);
                    PlayerPrefs.SetInt("resistancePotionQuantity", resistancePotionQuantity);
                    break;
                case 5:
                    hpPotionQuantity += 15;
                    resistancePotionQuantity += 5;
                    PlayerPrefs.SetInt("hpPotionQuantity", hpPotionQuantity);
                    PlayerPrefs.SetInt("resistancePotionQuantity", resistancePotionQuantity);
                    break;
            }
            PlayerPrefs.Save();
        }
        else
        {
            NotEnoughCoinPopup.SetActive(true);
        }
        /*
        CoinText.text = currentCoin.ToString();*/

        Debug.Log(bonusButton.coinUsed);
    }

    private void OnClickBack()
    {
        SceneManager.LoadScene("MapScene");
    }

    private void OnClickHome()
    {
        SceneManager.LoadScene("MainMenuScene");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool PlayerPrefsIntToBool(string key)
    {
        int intValue = PlayerPrefs.GetInt(key);
        return intValue != 0;
    }

    public void SaveList(List<string> saveList,string SaveName)
    {
        string jsonData = JsonUtility.ToJson(new StringListWrapper(saveList));
        Debug.Log(saveList[0]);
        Debug.Log(jsonData);
        PlayerPrefs.SetString(SaveName, jsonData);
        PlayerPrefs.Save();
    }
    public List<string> LoadList(string SavedName)
    {
        string jsonData = PlayerPrefs.GetString(SavedName, "{}");
        Debug.Log(jsonData);
        StringListWrapper data = JsonUtility.FromJson<StringListWrapper>(jsonData);
        return data.stringList;
    }

    [System.Serializable]
    private class StringListWrapper
    {
        public List<string> stringList;

        public StringListWrapper(List<string> list)
        {
            stringList = list;
        }
    }
    private void OnDestroy()
    {
        HomeButton.onClick.RemoveListener(OnClickHome);
        BackButton.onClick.RemoveListener(OnClickBack);
        RedeemButton.onClick.RemoveListener(OnClickRedeem);
        OkButton.onClick.RemoveListener(OnClickOkRedeemCode);

        foreach (var closeButton in CloseButtonList)
        {
            closeButton.onClick.RemoveListener(ClosePopup);
        }


        foreach (var bonusButton in BonusList)
        {
            bonusButton.bonusButton.onClick.RemoveListener(delegate { OnclickBonus(bonusButton); });
        }
    }
}
