using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firefly : MonoBehaviour
{
    public FireflyManager manager;

    public AudioClip collectedSound;
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //.
            GetComponent<Collider>().enabled = false;
            transform.DOMove(manager.transform.position, 3).SetSpeedBased().OnComplete(onComplete) ;
            Utils.soundManager.PlayFX(collectedSound);
            manager.PlayFlySound();

        }
    }

    private void onComplete()
    {
        manager.ActivateFirefly();
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
