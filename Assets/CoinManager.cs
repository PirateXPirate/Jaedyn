using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    public Text CoinText;
    int currentCoin;
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
    }
}
