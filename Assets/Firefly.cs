using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firefly : MonoBehaviour
{
    public FireflyManager manager;
    public GameObject Rotator;
    public AudioClip collectedSound;
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponent<Collider>().enabled = false;
            transform.parent = Rotator.transform;
         
            transform.DOMove(Rotator.transform.position, 3).SetSpeedBased().OnComplete(onComplete);
            manager.PlayFlySound();
          //  Utils.soundManager.PlayFX(collectedSound);
            /*
            transform.DOMove(manager.transform.position, 3).SetSpeedBased().OnComplete(onComplete) ;
           
          */

        }
    }

    private void onComplete()
    {
        Vector3 currentPosition = transform.position;

        currentPosition.z += 2f;
        transform.position = currentPosition;
        manager.AddFirefly();
        // 
        // gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
