using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCoin : MonoBehaviour
{
    [SerializeField] CoinManager coinManager;
    public int minCoin;
    public int maxCoin;

    private void OnEnable()
    {
        coinManager.AddCoin(Random.Range(minCoin, maxCoin + 1));
    }
  
}
