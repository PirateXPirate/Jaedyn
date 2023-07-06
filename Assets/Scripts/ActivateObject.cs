using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObject : MonoBehaviour
{
    public Transform targetTransform;
    public float time;

    public int targetPuzzleValue = 1;
    int currentValue = 0;
    public GameObject activateParticleObject;
    public AudioClip activateSound;
    virtual public void Activate()
    {
        currentValue += 1;
        if (currentValue == targetPuzzleValue)
        {
            if (GetComponent<Collider>())
            GetComponent<Collider>().enabled = false;
            activateParticleObject.SetActive(true);
            if(activateSound)
            Utils.soundManager.PlayFX(activateSound);
            transform.DOMove(targetTransform.position, time);
        }
    }


}
