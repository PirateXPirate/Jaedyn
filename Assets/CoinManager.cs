using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public Text CoinText;
    int currentCoin;

    public TextMeshProUGUI CoinPopupText;
    void Start()
    {
        currentCoin = PlayerPrefs.GetInt("CoinQuantity", 0);
        CoinText.text = currentCoin.ToString();
    }

    public void AddCoin(int num)
    {
        currentCoin += num;
        CoinText.text = currentCoin.ToString();
        PlayerPrefs.SetInt("CoinQuantity", currentCoin);
        PlayerPrefs.Save();
        CoinPopupText.text = "ได้รับเหรียญจำนวน " + num.ToString() + " เหรียญสำหรับแลกไอเท็มต่างๆในหน้าห้องไอเทมได้ ";
    }
}
