using MoreMountains.TopDownEngine;
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

    public bool gotKey = false;
    public bool gotLock = false;

    public Text ketText;

    [SerializeField] private GameObject noKeyPopUp;
    [SerializeField] private GameObject LockPopUp;

    public bool isBoos = false;
    public Health bossHealth;
    private void Start()
    {
        if (isBoos)
        {
            bossHealth.OnDeath += OnObjDead;
        }
    }
    private void OnObjDead()
    {
        CheckKey();

    }
    public void AddKey(int quantity)
    {
        keyQuantity += quantity;
        keyUIManager.SetkeyQuantity(keyQuantity);
        gotKey = true;
        CheckKey();
        ketText.text = keyQuantity.ToString() + "/1";

    }
    public void GetLock()
    {
        if (gotKey == false)
        {
            noKeyPopUp.SetActive(true);
        }
        else
        {
            LockPopUp.SetActive(true);
            gotLock = true;
            CheckKey();
        }
      

    }

    public void CheckKey()
    {
        if (!isBoos)
        {
            if (gotKey && gotLock && endGateParticle)
            {
                endGateParticle.SetActive(true);
            }
        }
        else
        {
            if (gotKey && gotLock && endGateParticle && bossHealth.CurrentHealth <= 0)
            {
                endGateParticle.SetActive(true);
            }
           
        }
        
    }
}
