using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyManager : MonoBehaviour
{
    [SerializeField] private KeyUIManager keyUIManager;
    int keyQuantity = 0;

    [SerializeField] private GameObject endGateParticle;

    bool gotKey = false;
    bool gotLock = false;

    public Text ketText;

    public void AddKey(int quantity)
    {
        keyQuantity += quantity;
        keyUIManager.SetkeyQuantity(keyQuantity);
        gotKey = true;
        CheckKey();
        ketText.text = keyQuantity.ToString();

    }
    public void GetLock()
    {
        gotLock = true;
        CheckKey();

    }

    private void CheckKey()
    {
        if (gotKey && gotLock)
        {
            endGateParticle.SetActive(true);
        }
    }
}
