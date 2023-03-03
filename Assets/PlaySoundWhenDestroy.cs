using MoreMountains.TopDownEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundWhenDestroy : MonoBehaviour
{
    Health health;
    bool canPlay = false;
    [SerializeField] GameObject[] revelantObjs;
    [SerializeField] AudioClip soundClip;

    [SerializeField] ActivateObject activateObj;
    void Start()
    {
        health = GetComponent<Health>();
        health.OnDeath += OnObjDead;
    }

    private void OnObjDead()
    {
        canPlay = true;
        foreach (var obj in revelantObjs)
        {
            if (obj.activeSelf)
            {
                canPlay = false;
            }
        }
        if (canPlay)
        {
            if(soundClip)
            Utils.soundManager.PlayFX(soundClip);
            if (activateObj)
                activateObj.Activate();
        }
         
    }

}
