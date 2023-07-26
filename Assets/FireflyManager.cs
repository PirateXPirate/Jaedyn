using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyManager : MonoBehaviour
{
    public Firefly[] fireflySet;
    int fireflyNum;
    public GameObject particle;
    public ActivateObject[] activatedObjects;
    public AudioClip[] flySound;
    public AudioClip completeSound;
    int index = 0;
    bool complete = false;
    void Start()
    {

    }
    public void Perform()
    {
        ActivateFirefly();
    }
    public void AddFirefly()
    {
        fireflyNum += 1;
    }
    public void ActivateFirefly()
    {
        if (complete) return;
        if (fireflyNum == fireflySet.Length)
        {
            complete = true;
            foreach (var a in fireflySet)
            {
                a.transform.parent = transform;

                a.transform.DOMove(transform.position, 1).SetSpeedBased().OnComplete(onComplete);
            }
            Utils.soundManager.PlayFX(completeSound);
        }
            

        
    }

    private void onComplete()
    {
        if (fireflyNum == fireflySet.Length)
        {
            particle.SetActive(true);
            foreach (var a in activatedObjects)
            {

                a.Activate();
            }
            foreach (var a in fireflySet)
            {
                a.gameObject.SetActive(false);
            }
           
        }
    }

    public void PlayFlySound()
    {
        Utils.soundManager.PlayFX(flySound[index]);
        index += 1;
    }
    // Update is called once per frame
    void Update()
    {

    }
}
