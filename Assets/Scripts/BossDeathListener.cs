using MoreMountains.TopDownEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathListener : MonoBehaviour
{
    [SerializeField] Health bossHealth;

    [SerializeField] GameObject completePopup;
    void Start()
    {
        bossHealth.OnDeath += BossDeath;
    }

    private void BossDeath()
    {
        completePopup.SetActive(true);
    }

    private void OnDisable()
    {
        bossHealth.OnDeath -= BossDeath;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
